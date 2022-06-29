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
        public Cp_Info[] Constant_Pool;
        public ushort Access_Flags;
        public ushort This_Class;
        public ushort Super_Class;
        public ushort Interfaces_Count;
        public ushort Interfaces;
        public ushort Fields_Count;
        public Field_Info[] Fields;
        public ushort Methods_Count;
        public Method_Info[] Methods;
        public ushort Attributes_Count;
        public Attribute_Info[] Attributes;

        public ClassFile(byte[] ins)
        {
            ReadOnlySpan<byte> input = ins.AsSpan();
            Magic = input.U4();
            Minor_Version = input.U2();
            Major_Version = input.U2();
            Constant_Pool_Count = input.U2();
            Constant_Pool = new Cp_Info[Constant_Pool_Count - 1];
            for (int i = 0; i < Constant_Pool_Count - 1; i++)
            {
                ParseConstantPoolValue(ref input, i);
            }
            Access_Flags = input.U2();
            This_Class = input.U2();
            Super_Class = input.U2();
            Interfaces_Count = input.U2();
            Interfaces = input.U2();
            Fields_Count = input.U2();
            for (int i = 0; i < Fields_Count; i++)
            {
                ParseFields(ref input, i);
            }
        }

        public void ParseFields(ref ReadOnlySpan<byte> input, int index)
        {
            Field_Info field = new Field_Info(ref input);
            
            Fields[index] = field;
        }

        public void ParseConstantPoolValue(ref ReadOnlySpan<byte> input, int index)
        {
            CONSTANTS tag = (CONSTANTS)input.U1();

            switch (tag)
            {
                case CONSTANTS.CONSTANT_Methodref:
                    CONSTANT_MethodRef methodRef = new CONSTANT_MethodRef(tag);
                    methodRef.Parse(ref input);
                    Constant_Pool[index] = methodRef;
                    break;

                case CONSTANTS.CONSTANT_Class:
                    CONSTANT_Class Class = new CONSTANT_Class(tag);
                    Class.Parse(ref input);
                    Constant_Pool[index] = Class;
                    break;

                case CONSTANTS.CONSTANT_Utf8:
                    CONSTANT_Utf8 utf8 = new CONSTANT_Utf8(tag);
                    utf8.Parse(ref input);
                    Constant_Pool[index] = utf8;
                    break;

                case CONSTANTS.CONSTANT_NameAndType:
                    CONSTANT_NameAndType nameAndType = new CONSTANT_NameAndType(tag);
                    nameAndType.Parse(ref input);
                    Constant_Pool[index] = nameAndType;
                    break;
            }
        }
    }
}
