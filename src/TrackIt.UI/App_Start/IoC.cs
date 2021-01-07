using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TrackIt.UI.Infrastructure;
using TrackIt.UI.Models;

namespace TrackIt.UI.App_Start
{
    public class IoC
    {
        public static void Config()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            //builder.RegisterType<RoleStore<ApplicationRole>>().As<IRoleStore<ApplicationRole, string>>().InstancePerRequest(); ;
            //builder.RegisterType<ApplicationDbContext>().AsSelf();
            builder.RegisterType<ProjectContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerRequest();

            //builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            //builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            //builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>())).AsImplementedInterfaces().InstancePerRequest();
            //builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            //builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>
            //{
            //    DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider(nameof(TrackIt.UI))
            //});

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}