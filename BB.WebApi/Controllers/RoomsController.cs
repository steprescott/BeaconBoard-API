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
    /// Any requests made to the API for Room entities will be handled by the RoomsController
    /// </summary>
    public class RoomsController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Room with the given details.
        /// </summary>
        /// <param name="Room">The details of the new Room.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Room Room)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.RoomBusinessLogic.Create(Room);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Room.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Room created");
        }

        /// <summary>
        /// Updates the Room with the given details.
        /// </summary>
        /// <param name="Room">These are the details that the Room should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Room Room)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.RoomBusinessLogic.Update(Room);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Room with ID '" + Room.RoomID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Room with ID of '" + Room.RoomID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Room updated");
        }

        /// <summary>
        /// Gets all Rooms that are in the system.
        /// </summary>
        /// <returns>An array of Room DTOs that holds the details for the Rooms.</returns>
        [HttpGet]
        [ResponseType(typeof(List<Room>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.RoomBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the Room with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Room that the request is asking for.</param>
        /// <returns>Room DTO that holds the details for the Room.</returns>
        [HttpGet]
        [ResponseType(typeof(Room))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.RoomBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Room found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Deletes the Room from the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Room to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.RoomBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Room with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Room with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Room deleted");
        }
    }
}