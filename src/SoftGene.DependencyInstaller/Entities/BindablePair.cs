using System;

namespace SoftGene.DependencyInstaller.Entities
{
    public class BindablePair
    {
        public Type EntityClass { get; set; }
        public Type EntityInterface { get; set; }
    }
}
