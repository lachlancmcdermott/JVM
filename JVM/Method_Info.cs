﻿using JVM.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public class Method_Info
    {
        int[] Locals { get; set; }
        public MethodAccessFlags Access_Flags { get; private set; }
        public ushort Name_Index { get; private set; }
        public ushort Descriptor_Index { get; private set; }
        public ushort Attributes_Count { get; private set; }
        Attribute_Info[] Attributes { get; set; }

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

        public void Execute(Stack<int> stack, Cp_Info[] Constant_Pool)
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
                    case INSTRUCTIONS.iload_0:
                        stack.Push(Locals[0]);
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

                    case INSTRUCTIONS.invokestatic:
                        //go to constant pool at line #, and get the method ref, find and run method
                        Cp_Info methRef = Constant_Pool[attr.Code[k + 1]];
                        methRef = 
                        break;

                    case INSTRUCTIONS.ireturn:
                        break;
                }
            }
        }
    }
}