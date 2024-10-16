using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Axpo.Challenge.FullStack.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring Swagger.
    /// </summary>
    public static class SwaggerExtensions
    {
        private const string balancingTypeNameDocs = "Axpo.Challenge.Balancing.xml";

        /// <summary>
        /// Adds Swagger configuration to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(IncludeXMLDocs);
        }

        /// <summary>
        /// Includes XML documentation files in the Swagger configuration.
        /// </summary>
        /// <param name="options">The SwaggerGen options.</param>
        private static void IncludeXMLDocs(SwaggerGenOptions options)
        {
            var currentProject = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var currentProjectDocsPath = Path.Combine(AppContext.BaseDirectory, currentProject);
            var balancingTypeNameDocsPath = Path.Combine(AppContext.BaseDirectory, balancingTypeNameDocs);

            if (File.Exists(currentProjectDocsPath))
            {
                options.IncludeXmlComments(currentProjectDocsPath);
            }

            if (File.Exists(balancingTypeNameDocsPath))
            {
                options.IncludeXmlComments(balancingTypeNameDocsPath);
            }
        }
    }
}