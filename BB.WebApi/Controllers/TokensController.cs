using BB.Domain;
using BB.Domain.Enums;
using BB.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace BB.WebApi.Controllers
{
    /// <summary>
    /// Following BasicAuth tokens are used to validate request access level. This controller deals with the generation of User Tokens.
    /// This is the only controller that can be access without the requirement of a User Token.
    /// </summary>
    [System.Web.Http.AllowAnonymous]
    public class TokensController : BaseWithAuthController
    {
        /// <summary>
        /// Authenticates if a username and password has access to the system.
        /// </summary>
        /// <param name="model">The token model that holds the Username and Password of the User to be authenticated.</param>
        /// <returns>HttpResponseMessage with the UserToken if authenticated.</returns>
        [HttpPost]
        [ResponseType(typeof(TokenResponseModel))]
        public HttpResponseMessage Post(TokenRequestModel model)
        {
            if (model != null)
            {
                if(model.Username != null && model.Password != null)
                {
                    //Authenticate the user
                    var token = BeaconBoardService.TokenBusinessLogic.authenticateUsernameAndPassword(model.Username, model.Password);

                    //If there is no user with the given details
                    if (token == null)
                    {
                        //Return HttpResponseMessage with NotFound status code
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Incorrect username or password.");
                    }

                    //Otherwise return with a status of OK with the User Token
                    return Request.CreateResponse(HttpStatusCode.OK, new TokenResponseModel { UserToken = token });
                }
                else
                {
                    //Return HttpResponseMessage with BadRequest status code
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Missing credentials.");
                }
            }

            //Return HttpResponseMessage with BadRequest status code
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No credentials sent.");
        }
    }
}