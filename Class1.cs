using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.IoC;
using Xunit;

namespace Test_IoC_and_generics
{
    public interface IPlugin
    {

    }

    public class Plugin : IPlugin
    {

    }

    public class PluginWrapper<TImpl> where TImpl: class, IPlugin
    {
        public PluginWrapper(TImpl _)
        {
        }
    }

    public class Investigation
    {
        public Investigation()
        {
            ServiceLocator.Default.RegisterType(typeof(Plugin),typeof(Plugin), RegistrationType.Transient);
            var wrapperType = typeof(PluginWrapper<>).MakeGenericType(typeof(Plugin));
            ServiceLocator.Default.RegisterType(wrapperType, wrapperType,RegistrationType.Transient);
        }

        [Fact]
        public void CanResolvePlugin()
        {
            Assert.NotNull(ServiceLocator.Default.ResolveType<Plugin>());
        }

        [Fact]
        public void CanResolveWrapper()
        {
            Assert.NotNull(ServiceLocator.Default.ResolveType<PluginWrapper<Plugin>>());
        }
    }
}
