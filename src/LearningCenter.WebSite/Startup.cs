using LearningCenter.WebSite.Models;
using Microsoft.Owin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearningCenter.WebSite.Startup))]
namespace LearningCenter.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
