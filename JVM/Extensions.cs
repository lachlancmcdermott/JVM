using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    public static class Extensions
    {
        public static byte U1(this ref Span<byte> span)
        {
            byte ret = span[0];
            span = span.Slice(1);
            return ret;
        }

        public static ushort U2(this ref Span<byte> span)
            => (ushort)(U1(ref span) << 8 | U1(ref span));

        public static uint U4(this ref Span<byte> span)
            => (uint)(U2(ref span) << 16 | U2(ref span));

    }
}
