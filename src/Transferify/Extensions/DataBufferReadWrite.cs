using System;
using Transferify.Interfaces;

namespace Transferify.Extensions
{
    public static class DataBufferReadWrite
    {
        public static byte ReadByte(this IDataBuffer dataBuffer, int index)
        {
            var currentIndex = dataBuffer.Position;
            dataBuffer.Position = index;
            var result = dataBuffer.ReadByte();
            dataBuffer.Position = currentIndex;
            return result;
        }

        public static byte[] ReadBytes(this IDataBuffer dataBuffer)
        {
            var result = new byte[dataBuffer.Position];
            Array.Copy(dataBuffer.BufferCopy, 0, result, 0, result.Length);
            return result;
        }

        public static byte ReadCurrentByte(this IDataBuffer dataBuffer)
        {
            return ReadByte(dataBuffer, dataBuffer.Position);
        }


        public static byte ReadHighNibble(this IDataBuffer dataBuffer)
        {
            var result = dataBuffer.BufferCopy[dataBuffer.Position].HighNibble();
            dataBuffer.IncrementPosition();
            return result;
        }

        public static byte ReadLowNibble(this IDataBuffer dataBuffer)
        {
            var result = dataBuffer.BufferCopy[dataBuffer.Position].LowNibble();
            dataBuffer.IncrementPosition();
            return result;
        }

        public static void WriteHighNibble(this IDataBuffer dataBuffer, byte value)
        {
            var bufferValue = dataBuffer.ReadCurrentByte() & 0x0F;
            dataBuffer.WriteByte((byte) (bufferValue | value << 4));
        }

        public static void WriteLowNibble(this IDataBuffer dataBuffer, byte value)
        {
            var bufferValue = dataBuffer.ReadCurrentByte() & 0xF0;
            dataBuffer.WriteByte((byte) (bufferValue | value));
        }

        public static byte ReadByte(this IDataBuffer dataBuffer)
        {
            return dataBuffer.Read(1)[0];
        }

        public static void WriteByte(this IDataBuffer dataBuffer, byte value)
        {
            dataBuffer.Write(new[]{value});
        }
    }
}