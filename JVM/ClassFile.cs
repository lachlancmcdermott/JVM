using JVM.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class ClassFile
    {
        public uint Magic { get; private set; }
        public ushort Minor_Version { get; private set; }
        public ushort Major_Version { get; private set; }
        public ushort Constant_Pool_Count { get; private set; }
        public Cp_Info[] Constant_Pool { get; private set; }
        public ushort Access_Flags { get; private set; }
        public ushort This_Class { get; private set; }
        public ushort Super_Class { get; private set; }
        public ushort Interfaces_Count { get; private set; }
        public ushort[] Interfaces { get; private set; }
        public ushort Fields_Count { get; private set; }
        public Field_Info[] Fields { get; private set; }
        public ushort Methods_Count { get; private set; }
        public Method_Info[] Methods { get; private set; }
        public ushort Attributes_Count { get; private set; }
        public Attribute_Info[] Attributes { get; private set; }

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
            Interfaces = new ushort[Interfaces_Count];
            for (int i = 0; i < Interfaces_Count; i++)
            {
                Interfaces[i] = input.U2();
            }
            Fields_Count = input.U2();
            for (int i = 0; i < Fields_Count; i++)
            {
                ParseFields(ref input, i);
            }
            Methods_Count = input.U2();
            Methods = new Method_Info[Methods_Count];
            for (int i = 0; i < Methods_Count; i++)
            {
                ParseMethods(ref input, i);
            }
            Attributes_Count = input.U2();
            Attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes_Count; i++)
            {
                ParseAttributes(ref input, i);
            }

            if(input.Length != 0)
            {
                throw new Exception("Bad!");
            }
        }

        public Method_Info FindMethod(string method)
        {
            List<Method_Info> filtered = new List<Method_Info>();
            for (int i = 0; i < this.Methods.Length; i++)
            {
                if (this.Methods[i].Access_Flags == (MethodAccessFlags.ACC_STATIC | MethodAccessFlags.ACC_PUBLIC))
                {
                    filtered.Add(this.Methods[i]);
                }
            }
            for (int i = 0; i < filtered.Count; i++)
            {
                CONSTANT_Utf8 test = (CONSTANT_Utf8)this.Constant_Pool[filtered[i].Name_Index - 1];
                string find = Encoding.UTF8.GetString(test.bytes);
                if (method == "main")
                {
                    return filtered[i];
                }
            }
            return null;
        }

        public void ParseAttributes(ref ReadOnlySpan<byte> input, int index)
        {
            Attributes[index] = Attribute_Info.Parse(ref input, Constant_Pool);
        }

        public void ParseMethods(ref ReadOnlySpan<byte> input, int index)
        {
            Method_Info method = new Method_Info(ref input, Constant_Pool);
            Methods[index] = method;
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
