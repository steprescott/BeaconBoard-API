using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BB.WebApi.Utilities
{
    public class ApiKeyHeaderValueHandler : DelegatingHandler
    {
        private static string PublicApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicApiKey"];
            }
        }

        private static string ApiKeyHeader
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiKeyHeader"];
            }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiKeyHeader = request.Headers.SingleOrDefault(h => h.Key == ApiKeyHeader);

            if (apiKeyHeader.Key != null)
            {
                var apiKeyHeaderValue = apiKeyHeader.Value.FirstOrDefault();
                if (apiKeyHeaderValue == null || apiKeyHeaderValue != PublicApiKey)
                {
                    request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid API token");
                }
            }
            else
            {
                request.CreateErrorResponse(HttpStatusCode.Unauthorized, "API token missing");
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
