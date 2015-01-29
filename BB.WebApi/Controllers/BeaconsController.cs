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
    /// Any requests made to the API for Beacon entities will be handled by the BeaconsController
    /// </summary>
    public class BeaconsController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Beacon with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="beacon">The details of the new beacon.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Post([FromBody] Beacon beacon)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.BeaconBusinessLogic.Create(beacon);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Beacon.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Beacon created");
        }

        /// <summary>
        /// Updates the Beacon with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="beacon">These are the details that the Beacon should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Put([FromBody] Beacon beacon)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.BeaconBusinessLogic.Update(beacon);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Beacon with ID '" + beacon.BeaconID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Beacon with ID of '" + beacon.BeaconID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Beacon updated");
        }

        /// <summary>
        /// Gets all Beacons that are in the system.
        /// </summary>
        /// <returns>An array of Beacon DTOs that holds the details for the Beacons.</returns>
        [HttpGet]
        [ResponseType(typeof(List<Beacon>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.BeaconBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the Beacon with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Beacon that the request is asking for.</param>
        /// <returns>Beacon DTO that holds the details for the Beacon.</returns>
        [HttpGet]
        [ResponseType(typeof(Beacon))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.BeaconBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Beacon found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Deletes the beacon from the database with the given ID.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="id">The ID of the beacon to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.BeaconBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Beacon with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Beacon with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Beacon deleted");
        }
    }
}