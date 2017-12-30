using System;

namespace SoftGene.DependencyInstaller.Exceptions
{
    [Serializable()]
    public class MultipleLifetimeAttributeException : Exception
    {
        public MultipleLifetimeAttributeException() { }
        public MultipleLifetimeAttributeException(string message) : base(message) { }
        public MultipleLifetimeAttributeException(string message, Exception ex) : base(message, ex) { }
    }
}
