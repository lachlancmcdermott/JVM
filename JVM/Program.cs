using JVM.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] file = System.IO.File.ReadAllBytes(@"\\GMRDC1\Folder Redirection\Lachlan.McDermott\Documents\Visual Studio Code\Java\CompArchPractice.class");
            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(file);

            ClassFile ins = new ClassFile(file);
            Method_Info mainInfo = ins.FindMethod("main");
            Attribute_Info code = ins.FindCode(ins);

            //get code attribute
            //parse descriptor

            #region debug
            if (ins.Magic != 3405691582)
            {
                throw new Exception("Not valid Java!");
            }

            Console.WriteLine("Magic: " + ins.Magic);
            Console.WriteLine("Minor Version: " + ins.Major_Version);
            Console.WriteLine("Minor Version: " + ins.Minor_Version);
            Console.WriteLine("Constant Pool:");
            for (int i = 0; i < ins.Constant_Pool_Count - 1; i++)
            {
                Console.WriteLine(ins.Constant_Pool[i]);
            }
            Console.WriteLine("Fields:");
            for (int i = 0; i < ins.Fields_Count; i++)
            {
                Console.WriteLine(ins.Fields[i]);
            }
            Console.WriteLine("Methods:");
            for (int i = 0; i < ins.Methods_Count; i++)
            {
                Console.WriteLine(ins.Methods[i]);
            }
            Console.WriteLine("Attributes:");
            for (int i = 0; i < ins.Attributes_Count; i++)
            {
                Console.WriteLine(ins.Attributes[i]);
            }
            #endregion
        }
    }
}
