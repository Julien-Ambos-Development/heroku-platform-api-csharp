using JAD.Heroku.SDK.Middleware;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Registration
{
    public static class HerokuServiceRegister
    {
        public static IServiceCollection AddHerokuService(this IServiceCollection services, string token)
        {
            services.AddTransient<HerokuHttpContextMiddleware>();
            services.AddHttpClient<HerokuService>(client =>
            {
                client.BaseAddress = new Uri("https://api.heroku.com/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("Accept", $"application/vnd.heroku+json; version=3");
            })
                .AddHttpMessageHandler<HerokuHttpContextMiddleware>();

            return services;
        }
    }
}
