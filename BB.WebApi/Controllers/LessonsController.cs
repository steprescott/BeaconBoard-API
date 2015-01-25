using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class LessonsController : BaseController
    {
        public HttpResponseMessage Get()
        {
            var items = BeaconBoardService.LessonBusinessLogic.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = BeaconBoardService.LessonBusinessLogic.GetByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No lesson found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}