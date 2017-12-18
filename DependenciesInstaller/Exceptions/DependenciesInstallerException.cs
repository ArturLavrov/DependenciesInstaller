using System;
using System.Collections.Generic;
using System.Text;

namespace DependenciesInstaller.Exceptions
{
    [Serializable()]
    public class DependenciesInstallerException : Exception
    {
        public DependenciesInstallerException()
        {

        }
        public DependenciesInstallerException(string message) : base(message)
        {

        }
        public DependenciesInstallerException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
