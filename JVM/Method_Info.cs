using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class Method_Info
    {
        ushort Access_Flags;
        ushort Name_Index;
        ushort Descriptor_Index;
        ushort Attributes_Count;
        Attribute_Info[] Attributes;

        public Method_Info(ref ReadOnlySpan<byte> input)
        {
            Access_Flags = input.U2();
            Name_Index = input.U2();
            Descriptor_Index = input.U2();
            Attributes_Count = input.U2();

            Attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes.Length; i++)
            {
                Attributes[i] = new Attribute_Info(ref input);
            }
        }
    }
}
