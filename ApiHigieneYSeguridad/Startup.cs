using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiHigieneYSeguridad.Constants;
using AutoMapper;
using Data.EF;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Contracts;
using Encriptacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Services;
using Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HealthChecks.UI.Client;

namespace ApiHigieneYSeguridad
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        //public object JwtBearerDefaults { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry("4b7b1c8f-621c-4093-bf14-16e84f4af73f");
            services.AddCors();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            #region Injeccion De Dependencia
            //DI Repositorios
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IGenericRepository<Empresa>, EmpresaRepository>();
            services.AddScoped<IGenericRepository<Empleado>, EmpleadoRepository>();
            services.AddScoped<IGenericRepository<Visita>, VisitaRepository>();
            services.AddScoped<IGenericRepository<Cliente>, ClienteRepository>();
            services.AddScoped<IGenericRepository<Plan>, PlanRepository>();
            services.AddScoped<IGenericRepository<EquipoMedicion>, EquipoMedicionRepository>();
            services.AddScoped<IGenericRepository<Establecimiento>, EstablecimientoRepository>();
            services.AddScoped<IGenericRepository<Ubicacion>, UbicacionRepository>();
            services.AddScoped<IGenericRepository<Irregularidad>, IrregularidadRepository>();
            services.AddScoped<IGenericRepository<TipoPlan>, TipoPlanRepository>();
            services.AddScoped<ISeguridadRepository, SeguridadRepository>();
            services.AddScoped<IGenericRepository<TipoEquipoMedicion>, TipoEquipoMedicionRepository>();
            services.AddScoped<IGenericRepository<TipoVisita>, TipoVisitaRepository>();
            services.AddScoped<IGenericRepository<Direccion>, DireccionRepository>();
            services.AddScoped<IGenericRepository<ResponsableEmpresa>, ResponsableEmpresaRepository>();
            services.AddScoped<IGenericRepository<Partido>, PartidoRepository>();
            services.AddScoped<IGenericRepository<EstadoPlan>, EstadoPlanRepository>();
            services.AddScoped<IGenericRepository<EstadoVisita>, EstadoVisitaRepository>();
            services.AddScoped<IGenericRepository<TipoIrregularidad>, TipoIrregularidadRepository>();
            services.AddScoped<IGenericRepository<TipoLicencia>, TipoLicenciaRepository>();
            services.AddScoped<IGenericRepository<Licencia>, LicenciaRepository>();
            services.AddScoped<IGenericRepository<Pago>, PagoRepository>();
            services.AddScoped<IGenericRepository<TasaConversionTipoLicencia>, TasaConversionTipoLicenciaRepository>();
            services.AddScoped<IGenericRepository<PrecioLicencia>, PrecioLicenciaRepository>();

            //DI Services
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IGenericService<Empleado>, EmpleadoService>();
            services.AddScoped<IGenericService<EquipoMedicion>, EquipoMedicionService>();
            services.AddScoped<IGenericService<Plan>, PlanService>();
            services.AddScoped<IGenericService<Visita>, VisitaService>();
            services.AddScoped<IGenericService<TipoVisita>, TipoVisitaService>();
            services.AddScoped<IGenericService<Establecimiento>, EstablecimientoService>();
            services.AddScoped<IGenericService<Direccion>, DireccionService>();
            services.AddScoped<IGenericService<Partido>, PartidoService>();
            services.AddScoped<IGenericService<Irregularidad>, IrregularidadService>();
            services.AddScoped<IGenericService<Licencia>, LicenciaService>();
            services.AddScoped<IGenericService<Empresa>, EmpresaService>();
            services.AddScoped<IGenericService<Cliente>, ClienteService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHashingService, HashingService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticateService, AuthentificateService>();
            #endregion

            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString(ConfiguracionConstants.NOMBRE_BASE_DATOS));

            services.AddHealthChecksUI(s =>
            {
                s.AddHealthCheckEndpoint("endpoint1", "https://localhost:44318/health");
            }).AddInMemoryStorage();

            services.AddDbContext<AplicationDbContext>(

                options => options.UseSqlServer(Configuration.GetConnectionString(ConfiguracionConstants.NOMBRE_BASE_DATOS),
                x => x.UseNetTopologySuite())
            );

            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Higiene y Seguridad", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                                    "Se Utiliza Esquema de Bearer JWT. \r\n\r\n Ingrese 'Bearer' [espacio] y el token.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "HigSegApi.xml");
                c.IncludeXmlComments(filePath);

            });
            #endregion

            #region Autentificacion JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = "ApiHigieneYSeguridad",
                    ValidIssuer = "ApiHigieneYSeguridad",
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("43729FD696AABBCC1A541BB1AB399FAD"))
                };
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksUI();

                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                //endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                //{
                //    ResultStatusCodes =
                //    {
                //        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                //        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                //        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                //    },
                //    ResponseWriter = WriteHealthCheckResponse
                //});
                //endpoints.MapHealthChecks("/healthui", new HealthCheckOptions()
                //{
                //    Predicate = _ => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Higiene y Seguridad V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHealthChecksUI();
        }

        private Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(new JProperty("OverallStatus", result.Status.ToString()),
                    new JProperty("TotalChecksDuration", result.TotalDuration.TotalSeconds.ToString("0:0.00")),
                    new JProperty("DependencyHealthChecks", new JObject(result.Entries.Select(dicItem =>
                        new JProperty(dicItem.Key, new JObject(
                                new JProperty("Status", dicItem.Value.Status.ToString()),
                                new JProperty("Duration", dicItem.Value.Duration.TotalSeconds.ToString("0:0.00")),
                                new JProperty("Exception", dicItem.Value.Exception?.Message),
                                new JProperty("Data", new JObject(dicItem.Value.Data.Select(dicData =>
                                    new JProperty(dicData.Key, dicData.Value))))
                            ))
                    ))));

            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}
