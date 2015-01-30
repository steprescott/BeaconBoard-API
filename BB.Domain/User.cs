using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BB.Domain
{
    /// <summary>
    /// DTO to allow the Entity to be passed over the API.
    /// This entity is used to hold any data that a Lecturer and a Student shares.
    /// </summary>
    public class User
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
        /// The password of the user. This is never returned via the API.
        /// </summary>
        public string Password { get; set; }
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
        /// <summary>
        /// A user has a Role. This allows access level of the API.
        /// This is so Students won't be able to create a new Course only view it.
        /// </summary>
        [Required]
        public Guid RoleID { get; set; }
        /// <summary>
        /// To avoid the passing of passwords to the API a user token system is used.
        /// This follows the BasicAuth idea and allows for other security advantages such of revoking of the token and not transferring of passwords.
        /// </summary>
        public Guid Token { get; set; }
    }
}
