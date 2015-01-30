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
    /// Any requests made to the API for Lecturer entities will be handled by the LecturersController
    /// </summary>
    public class LecturersController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Lecturer with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="Lecturer">The details of the new Lecturer.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Post([FromBody] Lecturer Lecturer)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.LecturerBusinessLogic.Create(Lecturer);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Lecturer.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Lecturer created");
        }

        /// <summary>
        /// Updates the Lecturer with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="Lecturer">These are the details that the Lecturer should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Put([FromBody] Lecturer Lecturer)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.LecturerBusinessLogic.Update(Lecturer);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Lecturer with ID '" + Lecturer.UserID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Lecturer with ID of '" + Lecturer.UserID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Lecturer updated");
        }

        /// <summary>
        /// Gets all Lecturers that are in the system.
        /// </summary>
        /// <returns>An array of Lecturer DTOs that holds the details for the Lecturers.</returns>
        [HttpGet]
        [ResponseType(typeof(List<LecturerDTOModel>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.LecturerBusinessLogic.GetAll();

            var Lecturers = new List<LecturerDTOModel>();

            foreach (Lecturer lecturer in items)
            {
                Lecturers.Add(new LecturerDTOModel
                {
                    UserID = lecturer.UserID,
                    Username = lecturer.Username,
                    FirstName = lecturer.FirstName,
                    OtherNames = lecturer.OtherNames,
                    LastName = lecturer.LastName,
                    EmailAddress = lecturer.EmailAddress,
                    RoleID = lecturer.RoleID,
                    CourseIDs = lecturer.CourseIDs,
                    SessionIDs = lecturer.SessionIDs
                });
            }

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, Lecturers);
        }

        /// <summary>
        /// Gets the Lecturer with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Lecturer that the request is asking for.</param>
        /// <returns>Lecturer DTO that holds the details for the Lecturer.</returns>
        [HttpGet]
        [ResponseType(typeof(LecturerDTOModel))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.LecturerBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Lecturer found");
            }

            var Lecturer = new LecturerDTOModel
            {
                UserID = obj.UserID,
                Username = obj.Username,
                FirstName = obj.FirstName,
                OtherNames = obj.OtherNames,
                LastName = obj.LastName,
                EmailAddress = obj.EmailAddress,
                RoleID = obj.RoleID,
                CourseIDs = obj.CourseIDs,
                SessionIDs = obj.SessionIDs
            };

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, Lecturer);
        }

        /// <summary>
        /// Deletes the Lecturer from the database with the given ID.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="id">The ID of the Lecturer to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.LecturerBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Lecturer with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Lecturer with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Lecturer deleted");
        }
    }
}