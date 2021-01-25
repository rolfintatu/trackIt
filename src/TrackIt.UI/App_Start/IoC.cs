using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TrackIt.UI.AuthManagers;
using TrackIt.UI.AuthModels;
using TrackIt.UI.Infrastructure;

namespace TrackIt.UI.App_Start
{
    public class IoC
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ProjectContext>().AsSelf().InstancePerRequest();

            builder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerRequest();

            builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>();
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>()))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}