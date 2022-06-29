﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class Field_Info
    {
        public ushort Access_Flags;
        public ushort Name_Index;
        public ushort Descriptor_Index;
        public ushort Attributes_Count;
        Attribute_Info[] Attributes;

        public Field_Info(ref ReadOnlySpan<byte> input)
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