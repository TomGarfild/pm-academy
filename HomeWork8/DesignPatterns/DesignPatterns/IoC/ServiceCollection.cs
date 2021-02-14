using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, object> _singletons;
        private readonly Dictionary<Type, object> _transients;

        public ServiceCollection()
        {
            _singletons = new Dictionary<Type, object>();
            _transients = new Dictionary<Type, object>();
        }

        public IServiceCollection AddTransient<T>()
        {
            _transients.Add(typeof(T), null);
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<T> factory)
        {
            _transients.Add(typeof(T), factory);
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<IServiceProvider, T> factory)
        {
            _transients.Add(typeof(T), factory);
            return this;
        }

        public IServiceCollection AddSingleton<T>()
        {
            _singletons.Add(typeof(T), null);
            return this;
        }

        public IServiceCollection AddSingleton<T>(T service)
        {
            _singletons.Add(typeof(T), service);
            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<T> factory)
        {
            _singletons.Add(typeof(T), factory.Invoke());
            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<IServiceProvider, T> factory)
        {
            _singletons.Add(typeof(T), factory.Invoke(BuildServiceProvider()));
            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(_singletons, _transients);
        }
    }
}