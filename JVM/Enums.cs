using System;
using System.Collections.Generic;
using System.Text;

namespace JVM
{
    //CONSTANT POOL
    public enum CONSTANTS : byte
    {
        CONSTANT_Class = 7,
        CONSTANT_Fieldref = 9,
        CONSTANT_Methodref = 10,
        CONSTANT_InterfaceMethodref = 11,
        CONSTANT_String = 8,
        CONSTANT_Integer = 3,
        CONSTANT_Float = 4,
        CONSTANT_Long = 5,
        CONSTANT_Double = 6,
        CONSTANT_NameAndType = 12,
        CONSTANT_Utf8 = 1,
        CONSTANT_MethodHandle = 15,
        CONSTANT_MethodType = 16,
        CONSTANT_InvokeDynamic = 18
    }

    //ACCESS FLAGS
    public enum ClassAccessFlags : ushort
    {
        ACC_PUBLIC = 0x0001,
        ACC_FINAL = 0x0010,
        ACC_SUPER = 0x0020,
        ACC_INTERFACE = 0x0200,
        ACC_ABSTRACT = 0x0400,
        ACC_SYNTHETIC = 0x1000,
        ACC_ANNOTATION = 0x2000,
        ACC_ENUM = 0x4000,
    }

    public enum FieldAccessFlags : ushort
    {
        ACC_PUBLIC = 0x0001,
        ACC_PRIVATE = 0x0002,
        ACC_PROTECTED = 0x0004,
        ACC_STATIC = 0x0008,
        ACC_FINAL = 0x0010,
        ACC_VOLATILE = 0x0040,
        ACC_TRANSIENT = 0x0080,
        ACC_SYNTHETIC = 0x1000,
        ACC_ENUM = 0x4000,
    }

    public enum MethodAccessFlags : ushort
    {
        ACC_PUBLIC = 0x0001,
        ACC_PRIVATE = 0x0002,
        ACC_PROTECTED = 0x0004,
        ACC_STATIC = 0x0008,
        ACC_FINAL = 0x0010,
        ACC_SYNCHRONIZED = 0x0020,
        ACC_BRIDGE = 0x0040,
        ACC_VARARGS = 0x0080,
        ACC_NATIVE = 0x0100,
        ACC_ABSTRACT = 0x0400,
        ACC_STRICT = 0x0800,
        ACC_SYNTHETIC = 0x1000,
    }

    //INSTRUCTIONS
    public enum INSTRUCTIONS : ushort
    {
        bipush = 0x10,
        iconst_0 = 0x03,
        iconst_1 = 0x04,
        iconst_2 = 0x05,
        iconst_3 = 0x06,
        iconst_4 = 0x07,
        iconst_5 = 0x08,
        istore_1 = 0x3c,
        istore_2 = 0x3d,
        istore_3 = 0x3e,
        iload_1 = 0x1b,
        iload_2 = 0x1c,
        iadd = 0x60,
        ireturn = 0xac,
        @return = 0xb1,
        invokestatic = 0xb8,
        aload_0 = 0x2a,
        invokespecial = 0xb7,

    }
}
