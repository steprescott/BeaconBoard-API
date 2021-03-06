﻿using BB.Domain;
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
    /// Any requests made to the API for Session entities will be handled by the SessionsController
    /// </summary>
    public class SessionsController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Session with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="Session">The details of the new Session.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Post([FromBody] Session Session)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.SessionBusinessLogic.Create(Session);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Session.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Session created");
        }

        /// <summary>
        /// Updates the Session with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="Session">These are the details that the Session should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Put([FromBody] Session Session)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.SessionBusinessLogic.Update(Session);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Session with ID '" + Session.SessionID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Session with ID of '" + Session.SessionID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Session updated");
        }

        /// <summary>
        /// Gets all Sessions that are in the system.
        /// </summary>
        /// <returns>An array of Session DTOs that holds the details for the Sessions.</returns>
        [HttpGet]
        [Route("sessions")]
        [ResponseType(typeof(List<Session>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.SessionBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets all the sessions that are upcoming.
        /// That is Sessions that are to be ran.
        /// </summary>
        /// <returns>An array of Session DTOs that holds the details for the Sessions that are upcoming.</returns>
        [HttpGet]
        [Route("sessions/upcoming")]
        [ResponseType(typeof(List<Session>))]
        public HttpResponseMessage GetUpcomingSessions()
        {
            //Get all the sessions that are upcoming. That is Sessions that are to be ran.
            var items = BeaconBoardService.SessionBusinessLogic.GetAllUpcomingSessions();

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets all the sessions that are upcoming for the given Course ID.
        /// </summary>
        /// <param name="courseID">The Course ID that should be used to filter the upcoming Sessions.</param>
        /// <returns>An array of Session DTOs that holds the details for the Sessions that are upcoming for the given Course ID.</returns>
        [HttpGet]
        [Route("sessions/upcoming")]
        [ResponseType(typeof(List<Session>))]
        public HttpResponseMessage GetUpcomingSessionsForCourse(Guid courseID)
        {
            //Get all the sessions that are upcoming for the Course with the given ID. That is Sessions that are to be ran for the Course.
            var obj = BeaconBoardService.CourseBusinessLogic.GetByID(courseID);

            //If there isn't a Course with the given ID
            if(obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course with the ID of '" + courseID + "' was not found.");
            }

            var items = BeaconBoardService.SessionBusinessLogic.GetAllUpcomingSessionsForCourseWithID(courseID);

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets all the sessions that are upcoming for the given Module ID.
        /// </summary>
        /// <param name="moduleID">The Module ID that should be used to filter the upcoming Sessions.</param>
        /// <returns>An array of Session DTOs that holds the details for the Sessions that are upcoming for the given Module ID.</returns>
        [HttpGet]
        [Route("sessions/upcoming")]
        [ResponseType(typeof(List<Session>))]
        public HttpResponseMessage GetUpcomingSessionsForModule(Guid moduleID)
        {
            //Get all the sessions that are upcoming for the Module with the given ID. That is Sessions that are to be ran for the Module.
            var obj = BeaconBoardService.ModuleBusinessLogic.GetByID(moduleID);

            //If there isn't a Module with the given ID
            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Module with the ID of '" + moduleID + "' was not found.");
            }

            var items = BeaconBoardService.SessionBusinessLogic.GetAllUpcomingSessionsForModuleWithID(moduleID);

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the Session with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Session that the request is asking for.</param>
        /// <returns>Session DTO that holds the details for the Session.</returns>
        [HttpGet]
        [Route("sessions/{id}")]
        [ResponseType(typeof(Session))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.SessionBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Session found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Gets the current Session that is ongoing, if there is one, for the Room with the given ID.
        /// </summary>
        /// <param name="roomID">The room ID that is to be checked for a ongoing session.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("sessions/current")]
        [ResponseType(typeof(Session))]
        public HttpResponseMessage GetCurrentSessionByRoomID(Guid roomID)
        {
            //Get back the current Session that is happening in the Room with the given ID
            var obj = BeaconBoardService.SessionBusinessLogic.GetCurrentSessionForRoomWithID(roomID);

            //Return the object with a status of OK
            //If there is no current Session for the Room then it will return an empty array
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Deletes the Session from the database with the given ID.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="id">The ID of the Session to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.SessionBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Session with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Session with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Session deleted");
        }
    }
}
