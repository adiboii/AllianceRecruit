using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.InjectJavascript("token-auth.js");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseCode API V1");
            });
        }

        private void ConfigureSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "V1",
                    Title = "ASP .NET CORE BaseCode API",
                    Description = "ASP .NET CORE BaseCode API",
                    TermsOfService = "None"
                });
            });
        }
    }
}
