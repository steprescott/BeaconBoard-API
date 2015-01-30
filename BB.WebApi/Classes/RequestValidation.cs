using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MS.WebApi.Classes
{
    public class RequestValidation
    {
        public bool Success { get; set; }

        public HttpResponseMessage HttpResponseErrorMessage { get; set; }
    }
}