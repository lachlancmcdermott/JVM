using JVM.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class ClassFile
    {
        public uint Magic { get; set; }
        public ushort Minor_Version;
        public ushort Major_Version;
        public ushort Constant_Pool_Count;
        Cp_Info[] Constant_Pool;
        public ushort Access_Flags;
        public ushort This_Class;
        public ushort Super_Class;
        public ushort Interfaces_Count;
        public ushort Interfaces;
        public ushort Fields_Count;
        Field_Info Fields;
        public ushort Methods_Count;
        Method_Info Methods;
        public ushort Attributes_Count;
        Attribute_Info Attributes;

        public ClassFile(byte[] ins)
        {
            Span<byte> input = ins.AsSpan();
            Magic = input.U4();
            Minor_Version = input.U2();
            Major_Version = input.U2();
            Constant_Pool_Count = input.U2();
            Constant_Pool = new Cp_Info[Constant_Pool_Count - 1];
        }

        public void ParseConstVal(Span<byte> input)
        {
            int index = 0;
            byte tag = input.U1();
            //based on tag make the correct class
            switch (tag)
            {
                case (byte)CONSTANTS.CONSTANT_Methodref:
                    CONSTANT_MethodRef method = new CONSTANT_MethodRef();
                    Constant_Pool[index] = method;
                    index++;
                    break; 
            }
        }
    }
}
