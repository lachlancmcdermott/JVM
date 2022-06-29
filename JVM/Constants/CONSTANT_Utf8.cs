using System;
using System.Collections.Generic;
using System.Text;

namespace JVM.Constants
{
    class CONSTANT_Utf8 : Cp_Info
    {
        public ushort Length { get; set; }
        public byte[] bytes { get; set; }

        public CONSTANT_Utf8(CONSTANTS tag)
            : base(tag)
        {

        }
        public override void Parse(ref ReadOnlySpan<byte> span)
        {
            Length = span.U2();
            bytes = new byte[Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = span.U1();
            }
        }
    }
}
