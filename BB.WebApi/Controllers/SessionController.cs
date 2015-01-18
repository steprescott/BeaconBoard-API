using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class SessionController : BaseController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.SessionRepository.GetAllSessions());
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = UnitOfWork.SessionRepository.GetSessionByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No session found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}