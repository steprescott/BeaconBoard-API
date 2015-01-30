using BB.Domain.Enums;
using BB.WebApi.Services;
using MS.WebApi.Classes;
using MS.WebApi.Classes.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BB.WebApi.Utilities
{
    /// <summary>
    /// This controller is to validate all requests made to the API are valid.
    /// A valid request is one that has came from an approved App (API Key) and from a valid user (UserToken).
    /// </summary>
    public class HeaderValueHandler : DelegatingHandler
    {
        private readonly BeaconBoardService _beaconBoardService = new BeaconBoardService();

        /// <summary>
        /// Gets the key for the PublicAPIKey from the App.Config.
        /// </summary>
        private static string ApiKeyHeader
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiKeyHeader"];
            }
        }

        /// <summary>
        /// Gets the value for the PublicAPIKey from the App.Config.
        /// </summary>
        private static string PublicApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicApiKey"];
            }
        }

        /// <summary>
        /// Gets the key for the UserTokenHeader from the App.Config.
        /// </summary>
        private static string UserTokenHeader
        {
            get
            {
                return ConfigurationManager.AppSettings["UserTokenHeader"];
            }
        }

        /// <summary>
        /// Overridden function to allow validation of header fields.
        /// </summary>
        /// <param name="request">The HTTP request made.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The Task.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Check to see if the request has a valid API Key
            var result = RequestHasValidApiKey(request);

            //If it has a valid API Key
            if (result.Success)
            {
                //Check to see if the request is from a valid user
                result = ValidateUserRequest(request);

                //If it isn't
                if (!result.Success)
                {
                    //Return the HttpResponseMessage returned from the ValidateUserRequest function
                    return Task.FromResult(result.HttpResponseErrorMessage);
                }
            }
            else
            {
                //Return the HttpResponseMessage returned from the RequestHasValidApiKey function
                return Task.FromResult(result.HttpResponseErrorMessage);
            }

            //Else just process the request as normal
            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Determines if a request contains a valid API Key.
        /// </summary>
        /// <param name="request">The HTTP request made.</param>
        /// <returns>RequestValidation with a success Boolean and a HttpResponseErrorMessage for the correct response.</returns>
        public RequestValidation RequestHasValidApiKey(HttpRequestMessage request)
        {
            //Get back the headers we are checking for
            var apiKeyHeader = request.Headers.SingleOrDefault(h => h.Key == ApiKeyHeader);

            //Check the API Key if there is one
            if (apiKeyHeader.Value != null)
            {
                var apiKeyHeaderValue = apiKeyHeader.Value.FirstOrDefault();
                if (apiKeyHeaderValue == null || apiKeyHeaderValue != PublicApiKey)
                {
                    return new RequestValidation
                    {
                        Success = false,
                        HttpResponseErrorMessage = request.CreateErrorResponse(HttpCodes.HttpCodeInvalidToken, "Invalid API Key")
                    };
                }
            }
            else
            {
                return new RequestValidation
                {
                    Success = false,
                    HttpResponseErrorMessage = request.CreateErrorResponse(HttpCodes.HttpCodeTokenRequired, "Missing API Key")
                };
            }

            return new RequestValidation
            {
                Success = true
            };
        }

        /// <summary>
        /// Determines if a request contains a valid User Token.
        /// </summary>
        /// <param name="request">The HTTP request made.</param>
        /// <returns>RequestValidation with a success Boolean and a HttpResponseErrorMessage for the correct response.</returns>
        public RequestValidation ValidateUserRequest(HttpRequestMessage request)
        {
            //Get back the headers we are checking for
            var userTokenHeader = request.Headers.SingleOrDefault(h => h.Key == UserTokenHeader);

            //Check the User Token if there is one
            if (userTokenHeader.Value != null)
            {
                //Get the User Token value from the header
                var userToken = Guid.Parse(userTokenHeader.Value.First());

                //Check to see if the User Token is valid
                var result = _beaconBoardService.TokenBusinessLogic.IsUserTokenValid(userToken);
                
                //If a User Token is not found
                if(result == TokenResult.NotFound)
                {
                    //Return the correct RequestValidation
                    return new RequestValidation
                    {
                        Success = false,
                        HttpResponseErrorMessage = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid User Token.")
                    };
                }
                //If the User Token has expired
                else if (result == TokenResult.Expired)
                {
                    //Return the correct RequestValidation
                    return new RequestValidation
                    {
                        Success = false,
                        HttpResponseErrorMessage = request.CreateErrorResponse(HttpCodes.HttpCodeInvalidToken, "Users account expired.")
                    };
                }
                //If the User Token has been revoked
                else if (result == TokenResult.Revoked)
                {
                    //Return the correct RequestValidation
                    return new RequestValidation
                    {
                        Success = false,
                        HttpResponseErrorMessage =  request.CreateErrorResponse(HttpCodes.HttpCodeInvalidToken, "Users account revoked.")
                    };
                }

                //If the UserToken is fine then get the user and their role
                var user = _beaconBoardService.UserBusinessLogic.GetUserForToken(userToken);
                var role = _beaconBoardService.RoleBusinessLogic.GetByID(user.RoleID);

                //Set the user and role of the request to that from the UserToken
                //This is used for access control of each endpoint of the API
                var userIdentity = new GenericIdentity(user.Username);
                HttpContext.Current.User = new GenericPrincipal(userIdentity, new[] { role.Name });
            }

            //Everything is OK so return the correct RequestValidation
            return new RequestValidation
            {
                Success = true,
                HttpResponseErrorMessage = request.CreateResponse(HttpStatusCode.OK, "User has access.")
            };
        }
    }
}
