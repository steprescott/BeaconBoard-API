using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BB.WebApi.Controllers
{
    public class LessonController : BaseController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.LessonRepository.GetAllLessons());
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = UnitOfWork.LessonRepository.GetLessonByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No lesson found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}