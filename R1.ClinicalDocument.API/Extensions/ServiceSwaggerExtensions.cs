using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace R1.ClinicalDocument.API.Extensions
{
    /// <summary>
    /// Swagger Service extension class
    /// </summary>
    public static class ServiceSwaggerExtensions
    {
        /// <summary>
        /// Swagger configurations
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services, IWebHostEnvironment env)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "R1 Clinical Document API",
                    Description = "Clinical Document Service",
                    TermsOfService = null
                });

                if (!IsTestHost(env))
                {
                    IncludeXmlDocument(c);
                }
            });
        }

        /// <summary>
        /// Swagger documentation XML document
        /// </summary>
        /// <param name="c"></param>
        private static void IncludeXmlDocument(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions c)
        {
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{GetAssemblyName()}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        }

        /// <summary>
        /// Creation of test host for swagger extension
        /// </summary>
        /// <returns></returns>
        private static bool IsTestHost(IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                return true;
            }

            return false;
            //return GetAssemblyName().ToLower() == _testHostName;
        }

        /// <summary>
        /// To get assembly name
        /// </summary>
        /// <returns></returns>
        private static string GetAssemblyName()
        {
            return Assembly.GetEntryAssembly().GetName().Name;
        }
    }
}
