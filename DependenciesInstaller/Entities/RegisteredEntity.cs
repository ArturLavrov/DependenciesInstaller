using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependenciesInstaller.Entities
{
    public abstract class RegisteredEntity
    {
        public Type Class { get; set; }
        public Type Interface { get; set; }
        public Attribute LifeTime { get; set; }

        public abstract void SetLifeTime(IServiceCollection serviceCollection);
    }
}
