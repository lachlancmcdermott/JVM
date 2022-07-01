using JVM.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    class Program
    {
        //static public Stack<int> stack = new Stack<int>();

        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            byte[] file = System.IO.File.ReadAllBytes(@"\\GMRDC1\Folder Redirection\Lachlan.McDermott\Documents\Visual Studio Code\Java\CompArchPractice.class");
            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(file);
            ClassFile ins = new ClassFile(file);

            if (ins.Magic != 3405691582)
            {
                throw new Exception("Not valid Java!");
            }

            Method_Info mainInfo = ins.FindMethod("main");
            mainInfo.Execute(stack);
        }
    }
}
