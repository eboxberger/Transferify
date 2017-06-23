using System;
using Transferify.Interfaces;

namespace Transferify
{
    public static class ThisMachine
    {
        public static Endianess Endianess => BitConverter.IsLittleEndian ? Endianess.Little : Endianess.Big;
    }
}