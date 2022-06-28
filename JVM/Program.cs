using System;

namespace JVM
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] file = System.IO.File.ReadAllBytes(@"\\GMRDC1\Folder Redirection\Lachlan.McDermott\Documents\Visual Studio Code\Java\CompArchPractice.class");

            ClassFile ins = new ClassFile(file);

            Console.WriteLine(ins.Magic);
            Console.WriteLine(ins.Major_Version);
            Console.WriteLine(ins.Minor_Version);
            Console.WriteLine(ins.Constant_Pool_Count);
        }
    }
}
