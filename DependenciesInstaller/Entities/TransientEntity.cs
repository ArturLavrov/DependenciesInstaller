using Microsoft.Extensions.DependencyInjection;

namespace DependenciesInstaller.Entities
{
    public class TransientEntity : RegisteredEntity
    {
        public override void SetLifeTime(IServiceCollection service)
        {
            service.AddTransient(Interface, Class);
        }
    }
}
