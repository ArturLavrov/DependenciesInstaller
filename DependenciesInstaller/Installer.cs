using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependenciesInstaller
{
    public static class Installer
    {
        public static readonly Guid AttributeTypeId = new Guid("C3730612-B077-4B0D-A7DD-A478D6DEE990");

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
