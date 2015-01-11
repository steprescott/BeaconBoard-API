using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class BeaconController : BaseController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.BeaconRepository.GetAllBeacons());
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = UnitOfWork.BeaconRepository.GetBeaconByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No beacon found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}