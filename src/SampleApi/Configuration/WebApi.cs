using System.Net.Mime;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SampleApi.API.V1.Controllers;

namespace SampleApi.Configuration
{
    internal static class WebApi
    {
        internal static readonly Assembly[] ControllersAssemblies = new[]
        {
            typeof(InfosController).Assembly
        };

        public static IServiceCollection AddWebApis(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                    options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                })
                .AddApplicationParts(ControllersAssemblies);

            return services;
        }

        private static IMvcBuilder AddApplicationParts(this IMvcBuilder builder, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
                builder.AddApplicationPart(assembly);
            return builder;
        }

        private class SlugifyParameterTransformer : IOutboundParameterTransformer
        {
            public string TransformOutbound(object value)
            {
                if (value == null) { return null; }

                return Regex.Replace(value.ToString(), "([^A-Z])([A-Z])", "$1-$2").ToLower();
            }
        }
    }
}
