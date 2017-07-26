using System;
using Transferify.DataTypes.Interfaces;
using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class LowNibble : INibble, ITransferValue
    {
        public LowNibble(byte value = 0)
        {
            Value = value;
        }

        #region IKnowMySize members

        public int SizeInBytes => 1;

        #endregion

        #region IReadable members

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            Value = dataBuffer.ReadLowNibble();
        }

        #endregion

        #region ITransferItem members

        public Endianess Endianess { get; set; }

        #endregion

        #region ITransferValue members

        public void ConvertFromString(string value)
        {
            Value = Convert.ToByte(value);
        }

        object ITransferValue.Value
        {
            get => Value;
            set => Value = (byte) value;
        }

        #endregion

        #region ITransferValueT<byte> members

        public byte Value { get; set; }

        #endregion

        #region IWritable members

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            dataBuffer.WriteLowNibble(Value);
        }

        #endregion
    }
}