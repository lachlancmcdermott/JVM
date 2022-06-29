using System;
using System.Collections.Generic;
using System.Text;

namespace JVM.Constants
{
    class CONSTANT_NameAndType :Cp_Info
    {
        public ushort Class_Index { get; set; }
        public ushort Name_And_Type_Index { get; set; }
        public ushort Descriptor_Index { get; set; }


        public CONSTANT_NameAndType(CONSTANTS tag)
            : base(tag)
        {

        }
        public override void Parse(ref ReadOnlySpan<byte> span)
        {
            Name_And_Type_Index = span.U2();
            Descriptor_Index = span.U2();
        }
    }
}
