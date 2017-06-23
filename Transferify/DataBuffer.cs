using System;
using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify
{
    public abstract class DataBuffer : IDataBuffer
    {
        protected DataBuffer(int capacity)
            : this(new byte[capacity])
        {
        }

        protected DataBuffer(byte[] data, int currentIndex = 0)
        {
            InternalBuffer = data;
            Position = currentIndex;
        }

        #region IDataBuffer members

        public virtual byte[] Read(int count)
        {
            var result = new byte[count];

            if (count == 1)
                result[0] = InternalBuffer[Position];
            else
                Array.Copy(InternalBuffer, Position, result, 0, count);

            MovePosition(count);
            return result;
        }

        public virtual void Write(byte[] values)
        {
            OnBeforeWrite(values);

            if (values.Length == 1)
                InternalBuffer[Position] = values[0];
            else
                Array.Copy(values, 0, InternalBuffer, Position, values.Length);

            MovePosition(values.Length);
        }

        public void Clear()
        {
            var capacity = Capacity;
            InternalBuffer = new byte[capacity];
            this.ResetPosition();
        }

        protected abstract void OnBeforeWrite(byte[] values);


        public byte[] BufferCopy
        {
            get
            {
                var dataCopy = new byte[Capacity];
                Array.Copy(InternalBuffer, dataCopy, dataCopy.Length);
                return dataCopy;
            }
        }

        public int Position { get; set; }

        public int Capacity => InternalBuffer.Length;

        #endregion

        #region Private methods

        private void MovePosition(int offset)
        {
            this.IncrementPosition(offset);
        }

        #endregion

        #region Fields

        protected byte[] InternalBuffer;

        #endregion
    }
}