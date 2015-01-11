using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BB.DataLayer;

namespace BB.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        private UnitOfWork _unitOfWork;
        public UnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? (_unitOfWork = new UnitOfWork()); }
        }
    }
}
