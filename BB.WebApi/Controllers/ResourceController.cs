using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BB.WebApi.Controllers
{
    public class ResourceController : BaseController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.ResourceRepository.GetAllResources());
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = UnitOfWork.ResourceRepository.GetRecourceByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No resource found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}