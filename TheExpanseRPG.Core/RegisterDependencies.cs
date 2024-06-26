﻿using Microsoft.Extensions.DependencyInjection;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core;
public static class RegisterDependencies
{
    public static IServiceCollection RegisterCoreDepdendencies(this IServiceCollection serviceProvider)
    {
        serviceProvider.RegisterCoreServices();
        serviceProvider.RegisterCoreModels();
        serviceProvider.RegisterCoreBuilders();
        return serviceProvider;
    }
}
