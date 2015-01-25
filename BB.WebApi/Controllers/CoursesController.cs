using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class CoursesController : BaseController
    {
        public HttpResponseMessage Get()
        {
            var items = BeaconBoardService.CourseBusinessLogic.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = BeaconBoardService.CourseBusinessLogic.GetByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No course found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}