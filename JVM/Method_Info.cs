using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class Method_Info
    {
        int[] Locals;
        public MethodAccessFlags Access_Flags;
        public ushort Name_Index;
        public ushort Descriptor_Index;
        public ushort Attributes_Count;
        Attribute_Info[] Attributes;

        public Method_Info(ref ReadOnlySpan<byte> input, Cp_Info[] Constant_Pool)
        {
            Access_Flags = (MethodAccessFlags)input.U2();
            Name_Index = input.U2();
            Descriptor_Index = input.U2();
            Attributes_Count = input.U2();

            Attributes = new Attribute_Info[Attributes_Count];
            for (int i = 0; i < Attributes.Length; i++)
            {
                Attributes[i] = Attribute_Info.Parse(ref input, Constant_Pool);
            }
        }

        public void Execute(Stack<int> stack)
        {
            Locals = new int[10];
            //Identify code attribute
            Code_Attribute_Info attr = null;
            for (int i = 0; i < Attributes.Length; i++)
            {
                if (Attributes[i] is Code_Attribute_Info a)
                {
                    attr = a;
                    break;
                }
            }

            for (int k = 0; k < attr.Code.Length; k++)
            {
                INSTRUCTIONS tag = (INSTRUCTIONS)attr.Code[k];
                switch (tag)
                {
                    case INSTRUCTIONS.iconst_5:
                        stack.Push(5);
                        break;

                    case INSTRUCTIONS.istore_1:
                        Locals[1] = stack.Peek();
                        stack.Pop();
                        break;

                    case INSTRUCTIONS.bipush:
                        stack.Push(attr.Code[k+1]);
                        k++;
                        break;

                    case INSTRUCTIONS.istore_2:
                        Locals[2] = stack.Peek();
                        stack.Pop();
                        break;

                    case INSTRUCTIONS.iload_1:
                        stack.Push(Locals[1]);
                        break;

                    case INSTRUCTIONS.iload_2:
                        stack.Push(Locals[2]);
                        break;

                    case INSTRUCTIONS.iadd:
                        int temp = stack.Peek();
                        stack.Pop();
                        temp = temp + stack.Peek();
                        stack.Pop();
                        stack.Push(temp);
                        break;

                    case INSTRUCTIONS.istore_3:
                        Locals[3] = stack.Peek();
                        stack.Pop();
                        break;

                    case INSTRUCTIONS.@return:
                        if(stack.Count > 0)
                        {
                            throw new Exception("bad");
                        }
                        break;
                }
            }
        }
    }
}