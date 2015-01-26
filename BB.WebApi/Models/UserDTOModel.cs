using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BB.WebApi.Models
{
    /// <summary>
    /// To avoid the password being set over the API a new User DTO is needed without the Password or token property.
    /// </summary>
    public class UserDTOModel
    {
        /// <summary>
        /// Primary key that is unique for the object.
        /// </summary>
        [Required]
        public Guid UserID { get; set; }
        /// <summary>
        /// The username of the user.
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// The first name of the user.
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Any other names of the user.
        /// </summary>
        public string OtherNames { get; set; }
        /// <summary>
        /// The last name of the user.
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// The email address of the user.
        /// </summary>
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}