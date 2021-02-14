using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _singletons;
        private readonly Dictionary<Type, object> _transients;

        public ServiceProvider(Dictionary<Type, object> singletons, Dictionary<Type, object> transients)
        {
            _singletons = singletons;
            _transients = transients;
        }

        public T GetService<T>()
        {
            var type = typeof(T);

            if (_singletons.ContainsKey(type))
            {
                _singletons[type] ??= type.GetConstructor(Type.EmptyTypes)?.Invoke(Type.EmptyTypes);

                return (T) _singletons[type];
            }

            if (!_transients.ContainsKey(type)) return default;

            return _transients[type] switch
            {
                null => (T) type.GetConstructor(Type.EmptyTypes)?.Invoke(Type.EmptyTypes),
                Func<T> factory => factory.Invoke(),
                Func<IServiceProvider, T> factory => factory.Invoke(this),
                _ => default
            };
        }
    }
}