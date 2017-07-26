using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public abstract class Numeric<T> : ITransferValueT<T>, ITransferValue, IKnowMySize
    {
        protected Numeric(T value, Endianess endianess, int byteCount)
        {
            Value = value;
            Endianess = endianess;
            SizeInBytes = byteCount;
        }

        #region IKnowMySize members

        public int SizeInBytes { get; }

        #endregion

        #region IReadable members

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            var bytes = dataBuffer.Read(SizeInBytes);
            if (NeedToSwap)
                bytes.Swap();

            Value = ConvertFromBytes(bytes);
        }

        #endregion

        #region ITransferItem members

        public Endianess Endianess { get; set; }

        #endregion

        #region ITransferValue members

        public void ConvertFromString(string value)
        {
            Value = OnConvertFromString(value);
        }

        object ITransferValue.Value
        {
            get => Value;
            set => Value = (T) value;
        }

        #endregion

        #region ITransferValueT<T> members

        public T Value { get; set; }

        #endregion

        #region IWritable members

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            var bytes = ConvertToBytes();
            if (NeedToSwap)
                bytes.Swap();

            dataBuffer.Write(bytes);
        }

        #endregion

        #region Protected methods

        protected abstract byte[] ConvertToBytes();
        protected abstract T ConvertFromBytes(byte[] value);
        protected abstract T OnConvertFromString(string value);

        #endregion

        private bool NeedToSwap => Endianess != ThisMachine.Endianess;
    }
}