using Microsoft.Extensions.DependencyInjection;
using OpenSeesServer.Core.Services.OpenSeesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSeesServer.Web.Extensions
{
    public static class RegisterServices
    {
        public static void RegisterCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IOpenSeesService, OpenSeesService>();
        }
    }
}
