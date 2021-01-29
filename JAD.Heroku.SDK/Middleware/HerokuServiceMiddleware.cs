using JAD.Heroku.SDK.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Middleware
{
    public class HerokuHttpContextMiddleware : DelegatingHandler
    {
        private readonly ILogger<HerokuHttpContextMiddleware> logger;

        public HerokuHttpContextMiddleware(ILogger<HerokuHttpContextMiddleware> logger)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await base.SendAsync(request, cancellationToken);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync());

                ErrorDetail error = new ErrorDetail { Message = responseContent.Message, StatusCode = response.StatusCode };

                logger.LogError($"HerokuService Error --> StatusCode: {(int)error.StatusCode} ; ErrorMessage: {error.Message}");

                throw new Exception(error.ToString());
            } 

            return response;
        }
    }
}
