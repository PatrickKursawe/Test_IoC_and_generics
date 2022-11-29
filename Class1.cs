using Catel.IoC;
using Xunit;
// ReSharper disable UnusedParameter.Local

namespace Test_IoC_and_generics
{
    public interface IPlugin
    {

    }

    public interface IInfrastructure
    {

    }

    public class Infrastructure : IInfrastructure
    {

    }

    public class Plugin : IPlugin
    {
        public Plugin(IInfrastructure _)
        {

        }
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
            ServiceLocator.Default.RegisterType<IInfrastructure, Infrastructure>(RegistrationType.Transient);
            ServiceLocator.Default.RegisterType(typeof(Plugin),typeof(Plugin),registrationType:RegistrationType.Transient);
            var wrapperType = typeof(PluginWrapper<>).MakeGenericType(typeof(Plugin));
            ServiceLocator.Default.RegisterType(wrapperType, wrapperType, registrationType:RegistrationType.Transient);
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
