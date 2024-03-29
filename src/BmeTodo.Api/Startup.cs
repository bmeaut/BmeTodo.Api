﻿using System;
using System.IO;
using System.Reflection;

using BmeTodo.Api.Exceptions;
using BmeTodo.Api.Services;
using BmeTodo.Api.Swagger;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Converters;

namespace BmeTodo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(c => c.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSingleton<TodoService>();

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BME Todo Service",
                    Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()
                });
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                // workaroundok az AutoRest generátor miatt UWP projektben
                o.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");
                o.SchemaFilter<AutoRestSchemaFilter>();
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddProblemDetails(o => o.Map<EntityNotFoundException>(ex => new StatusCodeProblemDetails(StatusCodes.Status404NotFound)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseProblemDetails();
            app.UseRouting();

            app.UseSwagger(c =>
            {
                // workaround az AutoRest generátor miatt UWP projektben
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BME Todo Service");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
