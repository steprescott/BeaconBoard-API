using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class CourseController : BaseController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.CourseRepository.GetAllCourses());
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = UnitOfWork.CourseRepository.GetCourseByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No course found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}