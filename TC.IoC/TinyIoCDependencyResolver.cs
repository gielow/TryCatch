using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using TinyIoC;

namespace TC.IoC
{
    public class TinyIoCDependencyResolver : IDependencyResolver
    {
        //private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly TinyIoCContainer container;

        public TinyIoCDependencyResolver(TinyIoCContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Erro ao resolver {0}. {1}", serviceType, ex);
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Erro ao resolver {0}. {1}", serviceType, ex);
                return Enumerable.Empty<object>();
            }
        }
    }
}