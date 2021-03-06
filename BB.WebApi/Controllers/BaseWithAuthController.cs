﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BB.WebApi.Services;
using BB.WebApi.Handlers;

namespace BB.WebApi.Controllers
{
    [ValidationActionFilter]
    [Authorize(Roles = "Lecturer, Student")]
    public class BaseWithAuthController : ApiController
    {
        private BeaconBoardService _beaconBoardService;

        public BeaconBoardService BeaconBoardService
        {
            get { return _beaconBoardService ?? (_beaconBoardService = new BeaconBoardService()); }
        }
    }
}
