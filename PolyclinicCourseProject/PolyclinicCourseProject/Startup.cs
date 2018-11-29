using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PolyclinicCourseProject.Startup))]
namespace PolyclinicCourseProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
