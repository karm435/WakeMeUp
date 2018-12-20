using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Acr.UserDialogs;
using Autofac;
using WakeMeUp.Services;
using Xamarin.Forms;

namespace WakeMeUp.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static IContainer container;

        static ViewModelLocator()
        {
            try
            {
                BuildContainer();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool GetAutoWireViewModel(BindableObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWiredViewModelChanged);

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        private static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            RegisterAppTypes(builder);

            RegisterViewModel(builder);

            container = builder.Build();
        }

        private static void RegisterViewModel(ContainerBuilder builder)
        {
            builder.RegisterType<MainPageViewModel>();
        }

        private static void OnAutoWiredViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel,{1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            var viewModel = container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }

        private static void RegisterAppTypes(ContainerBuilder builder)
        {
            builder.RegisterType<LocationService>().As<ILocationService>();

            var userDialogInstance = UserDialogs.Instance;
            builder.RegisterInstance<IUserDialogs>(userDialogInstance);
        }
    }
}
