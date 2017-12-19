﻿using System;

namespace DependenciesInstaller.EntityLifeTimeAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ScopedAttribute : Attribute
    {
        public override object TypeId
        {
            get { return Installer.AttributeTypeId; }
        }
        public override string ToString()
        {
            return "Scoped";
        }
    }
}
