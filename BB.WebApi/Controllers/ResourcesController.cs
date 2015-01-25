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
    /// Any requests made to the API for Resource entities will be handled by the ResourcesController
    /// </summary>
    public class ResourcesController : BaseController
    {
        /// <summary>
        /// Creates a new Resource with the given details.
        /// </summary>
        /// <param name="Resource">The details of the new Resource.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Resource Resource)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.ResourceBusinessLogic.Create(Resource);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Resource.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Resource created");
        }

        /// <summary>
        /// Updates the Resource with the given details.
        /// </summary>
        /// <param name="Resource">These are the details that the Resource should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Resource Resource)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.ResourceBusinessLogic.Update(Resource);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Resource with ID '" + Resource.ResourceID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Resource with ID of '" + Resource.ResourceID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Resource updated");
        }

        /// <summary>
        /// Gets all Resources that are in the system.
        /// </summary>
        /// <returns>An array of Resource DTOs that holds the details for the Resources.</returns>
        [HttpGet]
        [ResponseType(typeof(List<Resource>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.ResourceBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the Resource with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Resource that the request is asking for.</param>
        /// <returns>Resource DTO that holds the details for the Resource.</returns>
        [HttpGet]
        [ResponseType(typeof(Resource))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.ResourceBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Resource found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Deletes the Resource from the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Resource to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.ResourceBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Resource with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Resource with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Resource deleted");
        }
    }
}