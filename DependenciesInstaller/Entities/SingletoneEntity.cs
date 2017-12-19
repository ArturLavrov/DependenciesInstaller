using Microsoft.Extensions.DependencyInjection;

namespace DependenciesInstaller.Entities
{
    public class SingletoneEntity : RegisteredEntity
    {
        public override void SetLifeTime(IServiceCollection service)
        {
            service.AddSingleton(Interface, Class);
        }
    }
}
