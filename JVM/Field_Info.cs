using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class Field_Info
    {
        public ushort Access_Flags { get; private set; }
        public ushort Name_Index { get; private set; }
        public ushort Descriptor_Index { get; private set; }
        public ushort Attributes_Count { get; private set; }
        Attribute_Info[] Attributes { get; set; }

        public Field_Info(ref ReadOnlySpan<byte> input)
        {
            Access_Flags = input.U2();
            Name_Index = input.U2();
            Descriptor_Index = input.U2();
            Attributes_Count = input.U2();

            Attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes.Length; i++)
            {
                Attributes[i] = new Attribute_Info(ref input, Name_Index);
            }
        }
    }
}