using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Feature
{
    /// <summary>
    /// 
    /// </summary>
    public static class FeatureExtensions
    {
        static readonly string _myPolicy = "policyApiEcommerce";
        /// <summary>
        /// Metdo de extension para añadir los CORS.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddFeature(this IServiceCollection services,IConfiguration configuration)
        {
            //Configuracion de CORS.
            services.AddCors(op => op.AddPolicy(_myPolicy, o =>
            {
                o.WithOrigins(configuration["Config:OriginCors"]);
                o.AllowAnyHeader();
                o.AllowAnyMethod();
            }));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            return services;
        }
    }
}
