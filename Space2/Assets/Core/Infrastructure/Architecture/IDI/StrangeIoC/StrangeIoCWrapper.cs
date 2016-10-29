
using strange.extensions.injector.api;
using strange.extensions.injector.impl;
using strange.framework.api;

/// <summary>
/// Wrapper for StrangeIoC binder
/// </summary>
namespace Assets.Infrastructure.Architecture.IDI.StrangeIoC
{
	public class StrangeIoCWrapper : Architecture.IDI.IDI
    {
        private readonly InjectionBinder _binder = new InjectionBinder();

        public void Bind<TFrom, TTo>(bool isSingleton = true, string filter = null)
        {
            IInjectionBinding binding = _binder.Bind<TFrom>().To<TTo>();

            if (isSingleton) binding = binding.ToSingleton();
            if (!string.IsNullOrEmpty(filter)) binding.ToName(filter);
        }

        public void Bind(object from, object to, string filter = null)
        {
            IBinding binding = _binder.Bind(from).To(to);
            if (!string.IsNullOrEmpty(filter)) binding.ToName(filter);
        }

        public T Get<T>(string filter = null)
        {
            return _binder.GetInstance<T>(filter);
        }
    }
}