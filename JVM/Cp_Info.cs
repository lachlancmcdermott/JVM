using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public abstract class Cp_Info
    {
        public byte Tag { get; }

        public Cp_Info(byte tag)
        {
            Tag = tag;
        }

        public abstract void Parse(ref Span<byte> span); 
    }
}
