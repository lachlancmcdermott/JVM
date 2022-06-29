using System;
using System.Collections.Generic;
using System.Text;

namespace JVM.Constants
{
    public class CONSTANT_Class : Cp_Info
    {
        public ushort Class_Index { get; set; }
        public ushort Name_And_Type_Index { get; set; }

        public CONSTANT_Class(CONSTANTS tag)
            : base(tag)
        {

        }
        public override void Parse(ref ReadOnlySpan<byte> span)
        {
            Class_Index = span.U2();
        }
    }
}
