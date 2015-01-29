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
    /// Any requests made to the API for Course entities will be handled by the CoursesController
    /// </summary>
    public class CoursesController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Course with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="Course">The details of the new Course.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Post([FromBody] Course Course)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.CourseBusinessLogic.Create(Course);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Course.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Course created");
        }

        /// <summary>
        /// Updates the Course with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="Course">These are the details that the Course should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Put([FromBody] Course Course)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.CourseBusinessLogic.Update(Course);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Course with ID '" + Course.CourseID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Course with ID of '" + Course.CourseID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Course updated");
        }

        /// <summary>
        /// Gets all Courses that are in the system.
        /// </summary>
        /// <returns>An array of Course DTOs that holds the details for the Courses.</returns>
        [HttpGet]
        [ResponseType(typeof(List<Course>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.CourseBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the Course with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Course that the request is asking for.</param>
        /// <returns>Course DTO that holds the details for the Course.</returns>
        [HttpGet]
        [ResponseType(typeof(Course))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.CourseBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Course found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Deletes the Course from the database with the given ID.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="id">The ID of the Course to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.CourseBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Course with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Course with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Course deleted");
        }
    }
}