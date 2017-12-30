using System;

namespace SoftGene.DependencyInstaller.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BindToAttribute : Attribute
    {
        public BindToAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }

        public Type InterfaceType { get; set; }

        public override object TypeId
        {
            get { return Core.BindToAttributeTypeId; }
        }
    }
}
