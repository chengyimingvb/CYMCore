using System;

namespace CYM
{
    [Unobfus]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class Unobfus : Attribute
    {
    }
}
