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
    /// Any requests made to the API for Attendance entities will be handled by the AttendancesController
    /// </summary>
    public class AttendancesController : BaseWithAuthController
    {
        /// <summary>
        /// Creates a new Attendance with the given details.
        /// </summary>
        /// <param name="attendance">The details of the new Attendance.</param>
        /// <returns>HttpResponseMessage with correct status code and content for the result of the call.</returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Attendance attendance)
        {
            //Create a new item with the given details
            var result = BeaconBoardService.AttendanceBusinessLogic.Create(attendance);

            //If there was an error
            if (result == CRUDResult.Error)
            {
                //Return HttpResponseMessage with InternalServerError status code
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred when creating the attendance.");
            }

            //Otherwise return with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, "Attendance created");
        }

        /// <summary>
        /// Gets all Attendances that are in the system.
        /// </summary>
        /// <returns>An array of Attendance DTOs that holds the details for the Attendances.</returns>
        [HttpGet]
        [ResponseType(typeof(List<Attendance>))]
        public HttpResponseMessage Get()
        {
            //Get back all the items
            var items = BeaconBoardService.AttendanceBusinessLogic.GetAll();

            //Return them via a HttpResponseMessage with OK
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Gets the Attendance with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Attendance that the request is asking for.</param>
        /// <returns>Attendance DTO that holds the details for the Attendance.</returns>
        [HttpGet]
        [ResponseType(typeof(Attendance))]
        public HttpResponseMessage Get(Guid id)
        {
            //Get back the item details for the given ID
            var obj = BeaconBoardService.AttendanceBusinessLogic.GetByID(id);

            //If there wasn't an object with the given ID
            if (obj == null)
            {
                //Return HttpResponseMessage with NotFound status code
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Attendance found");
            }

            //Otherwise return the object with a status of OK
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}