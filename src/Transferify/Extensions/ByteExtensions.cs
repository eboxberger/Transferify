using System;
using Transferify.Interfaces;

namespace Transferify.Extensions
{
    public static class ByteExtensions
    {
        public static byte HighNibble(this byte value)
        {
            return (byte) (value >> 4);
        }

        public static byte LowNibble(this byte value)
        {
            return (byte) (value & 0x0F);
        }

        public static void Swap(this byte[] value)
        {
            Array.Reverse(value);
        }

        public static void Swap(this byte[] value, int index, int length)
        {
            Array.Reverse(value, index, length);
        }

        public static IDataBuffer CreateDataBuffer(this byte[] bytes, int currentIndex = 0)
        {
            return new FixedSizeBuffer(bytes, currentIndex);
        }


    }
}