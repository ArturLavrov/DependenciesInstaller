using System;
using System.Linq;
using SoftGene.DependencyInstaller.Attributes;
using SoftGene.DependencyInstaller.Exceptions;

namespace SoftGene.DependencyInstaller.Extensions
{
    internal static class TypeExtension
    {
        public static Attribute GetEntityLifeTimeAttribute(this Type type)
        {
            var attributes = Attribute.GetCustomAttributes(type).Where(a => ((Guid)a.TypeId) == Core.EntityLifetimeAttributeTypeId);
            if (attributes.Count() > 1)
            {
                throw new MultipleLifetimeAttributeException("Can't resolve Dependencies. Class contains more that two LifeTime attributes.");
            }
            else
            {
                return attributes.DefaultIfEmpty(new ScopedAttribute()).First();
            }
        }
    }
}
