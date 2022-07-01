using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    class Code_Attribute_Info : Attribute_Info
    {
        public new ushort Attribute_Name_Index { get; private set; }
        public new uint Attribute_Length { get; private set; }
        public ushort Max_Stack { get; private set; }
        public ushort Max_Locals { get; private set; }
        public uint Code_Length { get; private set; }

        public byte[] Code { get; private set; }
        public ushort Exception_Table_Length { get; private set; }
        Exception_Table[] Exception_Table { get; set; }
        public ushort Attributes_Count { get; private set; }
        Attribute_Info[] Attributes { get; set; }

        public Code_Attribute_Info(ref ReadOnlySpan<byte> input, ushort Name_Index, Cp_Info[] Constant_Pool)
            : base(ref input, Name_Index)
        {
            Max_Stack = input.U2();
            Max_Locals = input.U2();
            Code_Length = input.U4();
            Code = new byte[Code_Length];
            for (int i = 0; i < Code_Length; i++)
            {
                Code[i] = input.U1();
            }
            Exception_Table_Length = input.U2();
            Exception_Table = new Exception_Table[Exception_Table_Length];
            for (int i = 0; i < Exception_Table_Length; i++)
            {
                Exception_Table[i] = new Exception_Table(ref input);
            }
            Attributes_Count = input.U2();
            Attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes_Count; i++)
            {
                Attributes[i] = Attribute_Info.Parse(ref input, Constant_Pool);
            }
        }

    }
}
