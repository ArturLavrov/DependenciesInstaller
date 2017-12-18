using DependenciesInstaller.EntityLifeTimeAttributes;
using DependenciesInstaller.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependenciesInstaller.Extensions
{
    public static class TypeExtension
    {
        public static Attribute GetEntityLifeTimeAttribute(this Type type)
        {
            var attributes = Attribute.GetCustomAttributes(type).Where(a => ((Guid)a.TypeId) == Installer.AttributeTypeId);
            if (attributes.Count() > 1)
            {
                throw new DependenciesInstallerException("Can't resolve Dependencies.Class contains more that two LifeTime attributes.");
            }
            else
            {
                return attributes.DefaultIfEmpty(new ScopedAttribute()).First();
            }
        }
    }
}
