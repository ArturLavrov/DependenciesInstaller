using Microsoft.Extensions.DependencyInjection;

namespace DependenciesInstaller.Entities
{
    internal class SingletoneEntity : RegisteredEntity
    {
        public override void SetLifeTime(IServiceCollection service)
        {
            service.AddSingleton(Interface, Class);
        }
    }
}
