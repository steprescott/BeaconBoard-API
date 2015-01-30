using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MS.WebApi.Classes.Http
{
    public static class HttpCodes
    {
        public static HttpStatusCode  HttpCodeInvalidToken
        {
            get
            {
                return (HttpStatusCode)498;
            }
        }
        public static HttpStatusCode HttpCodeTokenRequired
        {
            get
            {
                return (HttpStatusCode)499;
            }
        }
    }
}