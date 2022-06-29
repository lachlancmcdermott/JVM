using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class Attribute_Info
    {
        public ushort Attribute_Name_Index;
        public uint Attribute_Length;
        public byte[] Info; 

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
    }
}
