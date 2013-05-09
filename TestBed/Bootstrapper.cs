using Autofac;
using Caliburn.Micro.Autofac;
using TestBed.ViewModels;

namespace TestBed
{
    public class Bootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        protected override void Configure() {
            AutoSubscribeEventAggegatorHandlers = true;
            EnforceNamespaceConvention = false;
            ViewModelBaseType = typeof (object);
            base.Configure();
        }

        protected override void ConfigureContainer(ContainerBuilder builder) {
            base.ConfigureContainer(builder);
        }
    }
}