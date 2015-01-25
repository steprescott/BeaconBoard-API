using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BB.WebApi.Controllers
{
    public class RoomsController : BaseController
    {
        public HttpResponseMessage Get()
        {
            var items = BeaconBoardService.RoomBusinessLogic.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var obj = BeaconBoardService.RoomBusinessLogic.GetByID(id);

            if (obj == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No room found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}