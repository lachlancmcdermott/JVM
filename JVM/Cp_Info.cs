using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public abstract class Cp_Info
    {
        public CONSTANTS Tag { get; }

        public Cp_Info(CONSTANTS tag)
        {
            Tag = tag;
        }

        public abstract void Parse(ref ReadOnlySpan<byte> span); 
    }
}
