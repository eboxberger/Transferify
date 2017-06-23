using System;

namespace Transferify
{
    public class DynamicSizeBuffer : DataBuffer
    {
        public DynamicSizeBuffer()
            : base(INITIAL_CAPACITY)
        {
        }

        public DynamicSizeBuffer(int initialCapacity)
            : base(initialCapacity)
        {
        }

        #region Protected methods

        protected override void OnBeforeWrite(byte[] values)
        {
            
            var numberOfRemainingBytes = InternalBuffer.Length - Position;
            var numberOfBytesToWrite = values.Length;

            if (numberOfRemainingBytes >= numberOfBytesToWrite)
                return;

            var numberOfMissingBytes = numberOfBytesToWrite - numberOfRemainingBytes;

            var numberOfChunks = numberOfMissingBytes / INITIAL_CAPACITY;
            if (numberOfMissingBytes % INITIAL_CAPACITY > 0)
                numberOfChunks++;

            var newCapacity = Capacity + numberOfChunks * INITIAL_CAPACITY;
            var newBuffer = new byte[newCapacity];
            Array.Copy(InternalBuffer, newBuffer, InternalBuffer.Length);
            InternalBuffer = newBuffer;
        }

        #endregion

        private const int INITIAL_CAPACITY = 500;
    }
}