using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    class Exception_Table
    {
        public ushort Start_pc { get; private set; }
        public ushort End_pc { get; private set; }
        public ushort Handler_pc { get; private set; }
        public ushort Catch_Type { get; private set; }

        public Exception_Table(ref ReadOnlySpan<byte> input)
        {
            Start_pc = input.U2();
            End_pc = input.U2();
            Handler_pc = input.U2();
            Catch_Type = input.U2();
        }
    }
}
