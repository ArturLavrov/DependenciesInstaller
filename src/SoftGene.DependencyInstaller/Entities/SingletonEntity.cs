using Microsoft.Extensions.DependencyInjection;

namespace SoftGene.DependencyInstaller.Entities
{
    internal class SingletonEntity : RegisteredEntity
    {
        public override void SetLifeTime(IServiceCollection service)
        {
            service.AddSingleton(Interface, Class);
        }
    }
}
