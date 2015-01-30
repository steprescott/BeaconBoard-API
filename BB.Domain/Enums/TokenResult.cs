using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain.Enums
{
    /// <summary>
    /// A user token can be in several states.
    /// This Enum is used to determine what state a token is in.
    /// </summary>
    public enum TokenResult
    {
        /// <summary>
        /// This is for it the token is in a valid state.
        /// </summary>
        Valid,
        /// <summary>
        /// This is for it the token is in a expired state.
        /// This could be due to account inactivity or session timeout
        /// </summary>
        Expired,
        /// <summary>
        /// This is for it the token is in a revoked state.
        /// This is due to the token manually being set to revoked in result of loss of device / account or miss-use of account.
        /// </summary>
        Revoked,
        /// <summary>
        /// This is for it the token is not found.
        /// This is due to the user not having an current issued token.
        /// </summary>
        NotFound
    }
}
