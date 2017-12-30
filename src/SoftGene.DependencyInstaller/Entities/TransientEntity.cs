using Microsoft.Extensions.DependencyInjection;

namespace SoftGene.DependencyInstaller.Entities
{
    internal class TransientEntity : RegisteredEntity
    {
        public override void SetLifeTime(IServiceCollection service)
        {
            service.AddTransient(Interface, Class);
        }
    }
}
