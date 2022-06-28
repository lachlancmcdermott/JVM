using System;
using System.Collections.Generic;
using System.Text;

namespace JVM.Constants
{
    public class CONSTANT_MethodRef : Cp_Info 
    {
        public ushort Class_Index { get, private set; }

        public override void Parse(ref Span<byte> span)
        {
            throw new NotImplementedException();)
        }
    }

}
