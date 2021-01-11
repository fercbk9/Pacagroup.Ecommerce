using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Pacagroup.Ecommerce.Transversal.Mapper;
using Newtonsoft.Json.Serialization;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Infraestructure.Data;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Infraestructure.Repository;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.IO;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Transversal.Logging;

namespace Pacagroup.Ecommerce.Services.WebApi
{
    /// <summary>
    /// StartUp class.
    /// </summary>
    public class Startup
    {
        readonly string _myPolicy = "policyApiEcommerce";
        /// <summary>
        /// Ctor of startup class.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// IConfiguration  Instance.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));
            //Configuracion de CORS.
            services.AddCors(op => op.AddPolicy(_myPolicy, o => 
            {
                o.WithOrigins(Configuration["Config:OriginCors"]);
                o.AllowAnyHeader();
                o.AllowAnyMethod();
            }));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            var appSettingsSection = Configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();


            services.AddSingleton(Configuration);
            services.AddSingleton<IConnectionFactory,ConnectionFactory>();
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<ICustomersDomain, CustomersDomain>();
            services.AddScoped<ICustomersRepository, CustomerRepository>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var issuer = appSettings.Issuer;
            var audience = appSettings.Audience;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = int.Parse(context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //Registro de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Pacagroup Tech Services API Market",
                    Description = "Aplicacion de prueba para pacagroup",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Fernando Martinez Pellicer",
                        Email = "fer-m.p@hotmail.com",
                        Url = new Uri("https://pacagroup.com")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Descripcion de la API key",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Name = "Authorization"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    { new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }                
                });
            });
        }

        /// <summary>
        /// Configures the API.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPIEcommerce v1");
            });
            app.UseCors(_myPolicy);
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
