using AutoMapper;
using Microsoft.Owin;
using Owin;
using WarmCallApplication.Models;

[assembly: OwinStartupAttribute(typeof(WarmCallApplication.Startup))]
namespace WarmCallApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Mapper.Initialize(cfg => { cfg.CreateMap<Requirement, RequirementViewModel>(); });
        }
    }
}
