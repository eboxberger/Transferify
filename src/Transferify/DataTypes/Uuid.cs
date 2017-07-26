using System;
using Transferify.DataTypes.Interfaces;
using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class Uuid : IUuid
    {
        public Uuid()
            : this(Guid.Empty, Endianess.Little)
        {
        }

        public Uuid(Endianess endianess)
            : this(Guid.Empty, endianess)
        {
        }

        public Uuid(string value = "00000000-0000-0000-0000-000000000000", Endianess endianess = Endianess.Little)
            : this(Guid.Parse(value), endianess)
        {
        }

        public Uuid(Guid value, Endianess endianess)
        {
            Value = value;
            Endianess = endianess;
        }

        #region IKnowMySize members

        public int SizeInBytes => 16;

        #endregion

        #region IReadable members

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            var bytes = dataBuffer.Read(SizeInBytes);
            if (NeedToSwap)
                SwapBytes(bytes);

            Value = new Guid(bytes);
        }

        #endregion

        #region ITransferItem members

        public Endianess Endianess { get; set; }

        #endregion

        #region ITransferValueT<Guid> members

        public Guid Value { get; set; }

        #endregion

        #region IWritable members

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            var bytes = Value.ToByteArray();
            if (NeedToSwap)
                SwapBytes(bytes);

            dataBuffer.Write(bytes);
        }

        #endregion

        #region Private methods

        private void SwapBytes(byte[] bytes)
        {
            bytes.Swap(0, 4);
            bytes.Swap(4, 2);
            bytes.Swap(6, 2);
        }

        #endregion

        private bool NeedToSwap => Endianess != ThisMachine.Endianess;
    }
}