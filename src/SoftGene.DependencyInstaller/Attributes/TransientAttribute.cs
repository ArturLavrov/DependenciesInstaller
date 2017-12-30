using System;

namespace SoftGene.DependencyInstaller.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TransientAttribute : Attribute
    {
        public override object TypeId
        {
            get { return Core.EntityLifetimeAttributeTypeId; }
        }

        public override string ToString()
        {
            return "Transient";
        }
    }
}
