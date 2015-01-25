using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class SessionsController : BaseController
    {
        public HttpResponseMessage Get()
        {
            var items = BeaconBoardService.SessionBusinessLogic.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = BeaconBoardService.SessionBusinessLogic.GetByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Session found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}