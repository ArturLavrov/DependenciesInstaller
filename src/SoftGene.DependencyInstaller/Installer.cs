using Microsoft.Extensions.DependencyInjection;

namespace SoftGene.DependencyInstaller
{
    public static class Installer
    {
        public static void RunDependencyInstaller(this IServiceCollection serviceCollection)
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
