using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BB.WebApi.Models
{
    /// <summary>
    /// In order to send the authenticate request via HTTP POST this model is used to encapsulate the two Strings in one object.
    /// </summary>
    public class TokenRequestModel
    {
        /// <summary>
        /// The username of the account to be authenticated
        /// </summary>
        public String Username { get; set; }
        /// <summary>
        /// The password of the account to be authenticated
        /// </summary>
        public String Password { get; set; }
    }
}