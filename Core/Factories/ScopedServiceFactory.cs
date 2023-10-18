using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Factories
{
    public class ScopedServiceFactory
    {
        public Dictionary<Type, IServiceScope> Scopes { get; private set; }
        public IServiceProvider Provider { get; }
        public ScopedServiceFactory(IServiceProvider provider)
        {
            Provider = provider;
            Scopes = new();
        }

        public IExpanseService GetScopedService<TExpanseService>() where TExpanseService : IExpanseService
        {
            if (Scopes.TryGetValue(typeof(TExpanseService), out IServiceScope? activeScope))
            {
                return activeScope.ServiceProvider.GetRequiredService<TExpanseService>();
            }
            IServiceScope newScope = Provider.CreateScope();
            IExpanseService scopedService = newScope.ServiceProvider.GetRequiredService<TExpanseService>();
            Scopes.Add(scopedService.GetType(), newScope);
            return scopedService;
        }
        public void DisposeScope<TExpanseService>() where TExpanseService : IExpanseService
        {
            if (Scopes.TryGetValue(typeof(TExpanseService), out IServiceScope? activeScope))
            {
                activeScope.Dispose();
                Scopes.Remove(typeof(TExpanseService));
            }
        }
    }
}
