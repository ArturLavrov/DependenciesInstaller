using System;

namespace DependenciesInstaller.EntityLifeTimeAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TransientAttribute : Attribute
    {
        public override object TypeId
        {
            get { return Core.AttributeTypeId; }
        }

        public override string ToString()
        {
            return "Transient";
        }
    }
}
