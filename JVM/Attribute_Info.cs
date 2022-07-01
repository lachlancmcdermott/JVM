using JVM.Constants;
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

        public Attribute_Info(ref ReadOnlySpan<byte> input, ushort Name_Index)
        {
            Attribute_Name_Index = Name_Index;
            Attribute_Length = input.U4(); 
        }

        public static Attribute_Info Parse(ref ReadOnlySpan<byte> input, Cp_Info[] Constant_Pool)
        {
            ushort Name_Index = input.U2();
            CONSTANT_Utf8 Constant = (CONSTANT_Utf8)Constant_Pool[Name_Index - 1];
            string ret = Constant.bytes.ByteToString();
            if (ret == "Code")
            {
                return new Code_Attribute_Info(ref input, Name_Index, Constant_Pool);
                
            }
            else
            {
                Attribute_Info attributeInfo = new Attribute_Info(ref input, Name_Index);
                attributeInfo.Info = new byte[attributeInfo.Attribute_Length];
                for (int i = 0; i < attributeInfo.Attribute_Length; i++)
                {
                    attributeInfo.Info[i] = input.U1();
                }
                return attributeInfo;
            }
        }
    }
}
