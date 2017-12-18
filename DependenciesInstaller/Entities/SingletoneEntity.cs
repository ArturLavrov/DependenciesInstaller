using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
