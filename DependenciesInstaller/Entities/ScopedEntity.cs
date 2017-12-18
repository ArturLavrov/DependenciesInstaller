using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesInstaller.Entities
{
    public class ScopedEntity : RegisteredEntity
    {
        public override void SetLifeTime(IServiceCollection service)
        {
            service.AddScoped(Interface, Class);
        }
    }
}
