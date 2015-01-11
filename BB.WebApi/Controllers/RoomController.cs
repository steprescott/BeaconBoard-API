using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BB.WebApi.Controllers
{
    public class RoomController : BaseController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.RoomRepository.GetAllRooms());
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = UnitOfWork.RoomRepository.GetRoomByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No room found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}