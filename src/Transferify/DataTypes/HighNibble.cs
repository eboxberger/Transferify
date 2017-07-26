using System;
using Transferify.DataTypes.Interfaces;
using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class HighNibble : INibble, ITransferValue
    {
        public HighNibble(byte value = 0)
        {
            Value = value;
        }

        #region IReadable members

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            Value = dataBuffer.ReadHighNibble();
        }

        #endregion

        #region IKnowMySize members

        public int SizeInBytes => 1;

        #endregion

        #region ITransferValue members

        object ITransferValue.Value
        {
            get => Value;
            set => Value = (byte) value;
        }

        public void ConvertFromString(string value)
        {
            Value = Convert.ToByte(value);
        }

        #endregion

        #region ITransferValueT<byte> members

        public Endianess Endianess { get; set; }

        public byte Value { get; set; }

        #endregion

        #region IWritable members

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            dataBuffer.WriteHighNibble(Value);
        }

        #endregion
    }
}