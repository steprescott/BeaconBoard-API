using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class ResourceTypeController : BaseController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.ResourceTypeRepository.GetAllResourceTypes());
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = UnitOfWork.ResourceTypeRepository.GetResourceTypeByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No resource type found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}