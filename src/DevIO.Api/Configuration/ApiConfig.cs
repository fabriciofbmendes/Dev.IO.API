using DevIO.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddWebApiConfig(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder => builder.WithOrigins("http://desenvolvedor.io")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

                options.AddPolicy("Production",
                    builder => builder
                    .WithMethods("GET")
                    .WithOrigins("http://desenvolvedor.io")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseWebApiConfig(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("Development");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;
        }
    }
}
