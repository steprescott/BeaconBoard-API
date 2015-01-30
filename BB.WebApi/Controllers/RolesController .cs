using BB.Domain;
using BB.Domain.Enums;
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
    /// Any requests made to the API for Role entities will be handled by the RolesController
    /// </summary>
    public class RolesController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Role with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="Role">The details of the new Role.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Post([FromBody] Role Role)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.RoleBusinessLogic.Create(Role);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Role.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Role created");
        }

        /// <summary>
        /// Updates the Role with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="Role">These are the details that the Role should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Put([FromBody] Role Role)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.RoleBusinessLogic.Update(Role);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Role with ID '" + Role.RoleID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Role with ID of '" + Role.RoleID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Role updated");
        }

        /// <summary>
        /// Gets all Roles that are in the system.
        /// </summary>
        /// <returns>An array of Role DTOs that holds the details for the Roles.</returns>
        [HttpGet]
        [ResponseType(typeof(List<Role>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.RoleBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the Role with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Role that the request is asking for.</param>
        /// <returns>Role DTO that holds the details for the Role.</returns>
        [HttpGet]
        [ResponseType(typeof(Role))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.RoleBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Role found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Deletes the Role from the database with the given ID.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="id">The ID of the Role to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.RoleBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Role with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Role with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Role deleted");
        }
    }
}