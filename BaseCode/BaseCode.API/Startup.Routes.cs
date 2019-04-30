using Microsoft.AspNetCore.Builder;

namespace BaseCode.API
{
    public partial class Startup
    {
        private void ConfigureRoutes(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapWebApiRoute(
                    name: "token",
                    template: "api/{token}");
            });
        }
    }
}
