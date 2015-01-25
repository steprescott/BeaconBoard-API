using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class ResourcesController : BaseController
    {
        public HttpResponseMessage Get()
        {
            var items = BeaconBoardService.ResourceBusinessLogic.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = BeaconBoardService.ResourceBusinessLogic.GetByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No resource found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}