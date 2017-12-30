using System;
using Microsoft.Extensions.DependencyInjection;

namespace SoftGene.DependencyInstaller.Entities
{
    internal abstract class RegisteredEntity
    {
        public Type Class { get; set; }
        public Type Interface { get; set; }
        public Attribute LifeTime { get; set; }

        public abstract void SetLifeTime(IServiceCollection serviceCollection);
    }
}
