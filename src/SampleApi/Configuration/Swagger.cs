using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SampleApi.Configuration
{
    internal static class Swagger
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSwaggerGen()
                .Configure<SwaggerGenOptions>(configuration.GetSection(WellKnownConfigSection.SwaggerGen))
                .Configure<SwaggerGenOptions>(c =>
                {
                    foreach (var assembly in WebApi.ControllersAssemblies)
                    {
                        var path = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
                        c.IncludeXmlComments(path, true);
                    }
                    c.DescribeAllParametersInCamelCase();
                    c.OperationFilter<AddResponseHeadersFilter>();
                    c.ExampleFilters();
                    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                    c.OperationFilter<SecurityRequirementsOperationFilter>();

                    c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");
                })
                .AddSwaggerExamplesFromAssemblies(WebApi.ControllersAssemblies);

            services.Configure<SwaggerOptions>(c =>
            {
                c.RouteTemplate = "swagger/{documentName}.json";
                c.PreSerializeFilters.Add((apiDocument, context) =>
                {
                    apiDocument.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer { Url = $"{context.Scheme}://{context.Host}/" },
                    };
                });
            });

            services
                .Configure<SwaggerUIOptions>(configuration.GetSection(WellKnownConfigSection.SwaggerUI))
                .ConfigureAll<SwaggerUIOptions>(options =>
                {
                    options.SwaggerEndpoint("v1.json", "Sample API V1");
                });

            return services;
        }
    }
}
