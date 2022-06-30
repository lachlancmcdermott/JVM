using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class Attribute_Info
    {
        public virtual ushort Attribute_Name_Index { get; set; }
        public uint Attribute_Length { get; set; }
        public byte[] Info { get; private set; }

        public Attribute_Info(ref ReadOnlySpan<byte> input)
        {
            Attribute_Name_Index = input.U2();
            Attribute_Length = input.U4();

            Info = new byte[Attribute_Length];
            for (int i = 0; i < Info.Length; i++)
            {
                Info[i] = input.U1();
            }
        }

        public virtual void Parse(ref ReadOnlySpan<byte> input)
        {

            input.U2();
        }
    }
}
