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
        public ushort[] Interfaces;
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

        //code attribute info
        //parse attributes

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

        public Attribute_Info FindCode(string method, ClassFile ins)
        {
            Method_Info work = FindMethod(method);

            for (int i = 0; i < ins.Constant_Pool_Count; i++)
            {
                int attributeNameIndex = ins.Attributes[i].Attribute_Name_Index;
                CONSTANT_Utf8 test = (CONSTANT_Utf8)ins.Constant_Pool[attributeNameIndex - 1];
                string info = Encoding.UTF8.GetString(test.bytes);
            }
            return null;
        }

        public void ParseAttributes(ref ReadOnlySpan<byte> input, int index)
        {
            Attribute_Info attribute = new Attribute_Info(ref input);
            Attributes[index] = attribute;
        }

        public void ParseMethods(ref ReadOnlySpan<byte> input, int index)
        {
            Method_Info method = new Method_Info(ref input);
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
