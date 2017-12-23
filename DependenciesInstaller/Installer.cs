using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependenciesInstaller
{
    public static class Installer
    {
        public static void RunDependenciesInstaller(this IServiceCollection serviceCollection)
        {
            var assembliesPaths = Core.GetAssembliesPath();
            foreach (var path in assembliesPaths)
            {
                var types = Core.GetAssemblyTypes(path);
                var entitiesToRegister = Core.GetRegisteredEntity(types);
                foreach (var entity in entitiesToRegister)
                {
                    Core.RegisterDependencies(serviceCollection, entity);
                }
            }
        }
    }
}
