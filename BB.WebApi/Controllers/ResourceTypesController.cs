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
    /// Any requests made to the API for ResourceType entities will be handled by the ResourceTypesController
    /// </summary>
    public class ResourceTypeTypesController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Resource Type with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="ResourceType">The details of the new Resource Type.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Post([FromBody] ResourceType ResourceType)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.ResourceTypeBusinessLogic.Create(ResourceType);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating a new Resource Type.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Resource Type created");
        }

        /// <summary>
        /// Updates the Resource Type with the given details.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="ResourceType">These are the details that the Resource Type should be update with.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPut]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Put([FromBody] ResourceType ResourceType)
        {
            //Update the item that is in the database with the given details
            var result = BeaconBoardService.ResourceTypeBusinessLogic.Update(ResourceType);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when updating the Resource Type with ID '" + ResourceType.ResourceTypeID + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Resource Type with ID of '" + ResourceType.ResourceTypeID + "' to update.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Resource Type updated");
        }

        /// <summary>
        /// Gets all Resource Types that are in the system.
        /// </summary>
        /// <returns>An array of Resource Type DTOs that holds the details for the Resource Types.</returns>
        [HttpGet]
        [ResponseType(typeof(List<ResourceType>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.ResourceTypeBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the ResourceType with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Resource Type that the request is asking for.</param>
        /// <returns>Resource Type DTO that holds the details for the Resource Type.</returns>
        [HttpGet]
        [ResponseType(typeof(ResourceType))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.ResourceTypeBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Resource Type found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        /// <summary>
        /// Deletes the Resource Type from the database with the given ID.
        /// Only Lecturers can call this action.
        /// </summary>
        /// <param name="id">The ID of the Resource Type to delete.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpDelete]
        [Authorize(Roles = "Lecturer")]
        public HttpResponseMessage Delete(Guid id)
        {
            //Delete the item from the database with the given ID
            var result = BeaconBoardService.ResourceTypeBusinessLogic.DeleteByID(id);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when deleting the Resource Type with ID '" + id + "'");
            }
            //If there isn't an item with the ID of the given item ID
            else if (result == CRUDResult.NotFound)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find a Resource Type with ID of '" + id + "' to delete.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Resource Type deleted");
        }
    }
}