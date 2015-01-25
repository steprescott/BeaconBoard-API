using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Domain.Enums
{
    /// <summary>
    /// A CRUD operation can end with several states.
    /// This Enum is used to determine what state the CRUD operation finished with.
    /// </summary>
    public enum CRUDResult
    {
        /// <summary>
        /// This state is used on a successful creation of an entity.
        /// </summary>
        Created,
        /// <summary>
        /// This state is used on a successful update of an entity.
        /// </summary>
        Updated,
        /// <summary>
        /// This state is used on a successful deletion of an entity.
        /// </summary>
        Deleted,
        /// <summary>
        /// This state is used on a if the entity is not found.
        /// This should only happen for operations that take an ID.
        /// </summary>
        NotFound,
        /// <summary>
        /// This state is used if any error occurs during the CRUD operation.
        /// </summary>
        Error
    }
}
