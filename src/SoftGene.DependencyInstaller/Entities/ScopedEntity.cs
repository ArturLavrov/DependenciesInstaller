using Microsoft.Extensions.DependencyInjection;

namespace SoftGene.DependencyInstaller.Entities
{
    internal class ScopedEntity : RegisteredEntity
    {
        public override void SetLifeTime(IServiceCollection service)
        {
            service.AddScoped(Interface, Class);
        }
    }
}
