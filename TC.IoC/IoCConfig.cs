using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core;
using TC.DataAccess;
using TinyIoC;

namespace TC.IoC
{
    public class IoCConfig
    {
        public static void Register()
        {
            var container = TinyIoCContainer.Current;

            container.Register<IRepository, Repository>();

            System.Web.Mvc.DependencyResolver.SetResolver(new TinyIoCDependencyResolver(container));
        }
    }
}
