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
    /// Any requests made to the API for Lesson entities will be handled by the lessonsController
    /// </summary>
    public class LessonsController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Lesson with the given details.
        /// </summary>
        /// <param name="Lesson">The details of the new Lesson.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Lesson Lesson)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.LessonBusinessLogic.Create(Lesson);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Lesson.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Lesson created");
        }

        /// <summary>
        /// Updates the Lesson with the given details.
        /// </summary>
        /// <param name="Lesson">These are the details that the Lesson should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Lesson Lesson)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.LessonBusinessLogic.Update(Lesson);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Lesson with ID '" + Lesson.LessonID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Lesson with ID of '" + Lesson.LessonID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Lesson updated");
        }

        /// <summary>
        /// Gets all Lessons that are in the system.
        /// </summary>
        /// <returns>An array of Lesson DTOs that holds the details for the Lessons.</returns>
        [HttpGet]
        [ResponseType(typeof(List<Lesson>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.LessonBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the Lesson with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Lesson that the request is asking for.</param>
        /// <returns>Lesson DTO that holds the details for the Lesson.</returns>
        [HttpGet]
        [ResponseType(typeof(Lesson))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.LessonBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Lesson found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Deletes the Lesson from the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Lesson to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.LessonBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Lesson with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Lesson with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Lesson deleted");
        }
    }
}