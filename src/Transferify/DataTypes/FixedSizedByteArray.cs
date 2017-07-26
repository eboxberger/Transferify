using System;
using Transferify.DataTypes.Interfaces;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class FixedSizedByteArray : IByteArray
    {
        public int SizeInBytes => Value.Length;

        public Endianess Endianess { get; set; }

        public byte[] Value
        {
            get => _value;
            set
            {
                if (value == null)
                    throw new InvalidOperationException("The passed value may not be null.");

                if (value.Length != _byteCount)
                    throw new InvalidOperationException("The passed value has wrong size.");

                _value = value;
            }
        }

        public FixedSizedByteArray(byte[] bytes)
        {
            _value = bytes ?? throw new ArgumentNullException(nameof(bytes));
            _byteCount = bytes.Length;
        }

        public FixedSizedByteArray(int length)
            : this(new byte[length])
        {
        }

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            _value = dataBuffer.Read(_byteCount);
        }

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            dataBuffer.Write(Value);
        }

        #region Fields

        private readonly int _byteCount;
        private byte[] _value;

        #endregion
    }
}