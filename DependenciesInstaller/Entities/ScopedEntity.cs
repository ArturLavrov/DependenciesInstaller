using Microsoft.Extensions.DependencyInjection;

namespace DependenciesInstaller.Entities
{
    internal class ScopedEntity : RegisteredEntity
    {
        public override void SetLifeTime(IServiceCollection service)
        {
            service.AddScoped(Interface, Class);
        }
    }
}
