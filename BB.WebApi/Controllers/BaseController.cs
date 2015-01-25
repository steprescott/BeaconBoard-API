using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BB.WebApi.Services;

namespace BB.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        private BeaconBoardService _beaconBoardService;

        public BeaconBoardService BeaconBoardService
        {
            get { return _beaconBoardService ?? (_beaconBoardService = new BeaconBoardService()); }
        }
    }
}
