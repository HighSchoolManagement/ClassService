using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using SchoolService.Contracts.Schools;

namespace ClassService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSchoolServiceClient(
            this IServiceCollection services, IConfiguration config)
        {
            var baseUrl = config["Services:SchoolService:BaseUrl"]
     ?? throw new InvalidOperationException("Missing Services:SchoolService:BaseUrl");

            services.AddRefitClient<ISchoolsApi>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

            return services;
        }
    }
}
