using Autofac;
using Autofac.Integration.WebApi;
using FileManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace WebApi_Angular
{
    public static class DependencyResolver
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<FileCountModel>().As<IFileCountModel>().InstancePerApiRequest();
            builder.RegisterType<FileManager>().As<IFileManager>().InstancePerApiRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}