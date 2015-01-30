using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Models
{
    /// <summary>
    /// This wraps the response of the authentication request to hold a User Token.
    /// </summary>
    public class TokenResponseModel
    {
        /// <summary>
        /// If successful the users User Token will be populated.
        /// </summary>
        public Guid? UserToken { get; set; }
    }
}