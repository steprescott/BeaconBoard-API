using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class ResourceTypesController : BaseController
    {
        public HttpResponseMessage Get()
        {
            var items = BeaconBoardService.ResourceTypeBusinessLogic.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = BeaconBoardService.ResourceTypeBusinessLogic.GetByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No resource type found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}