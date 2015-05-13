using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;
using System.Data.Entity;
using Ninject;
using DependencyResolver;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new NinjectDependencyResolver();
            var k = t.GetService(IProfileRepository);

        }
    }

    public class NinjectDependencyResolver 
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel(new ResolverModule());
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}
