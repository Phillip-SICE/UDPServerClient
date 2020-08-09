using Caliburn.Micro;
using Sice.PoC.UDPServer;
using System;
using System.Collections.Generic;
using System.Windows;
using UDPServer.ViewModels;

namespace UDPServer
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();
        
        public Bootstrapper()
        {
            Initialize();
        }


        protected override void Configure()
        {
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.PerRequest<ShellViewModel>();
            _container.RegisterPerRequest(typeof(SiceUDPServer), null, typeof(SiceUDPServer));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
