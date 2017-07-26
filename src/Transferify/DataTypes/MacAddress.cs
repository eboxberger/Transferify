using System;
using System.Globalization;
using Transferify.DataTypes.Interfaces;
using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class MacAddress : IMacAddress
    {
        public MacAddress(string value = "00:00:00:00:00:00")
        {
            Value = value;
            Endianess = Endianess.None;
        }

        #region IKnowMySize members

        public int SizeInBytes => 6;

        #endregion

        #region IReadable members

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            var bytes = dataBuffer.Read(SizeInBytes);
            Value = BitConverter.ToString(bytes).Replace(DOT_NET_SEPARATOR, SEPARATOR);
        }

        #endregion

        #region ITransferItem members

        public Endianess Endianess { get; set; }

        #endregion

        #region ITransferValueT<string> members

        public string Value { get; set; }

        #endregion

        #region IWritable members

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            var parts = Value.Split(SEPARATOR);
            foreach (var part in parts)
            {
                var value = byte.Parse(part, NumberStyles.HexNumber);
                dataBuffer.WriteByte(value);
            }
        }

        #endregion

        private const char SEPARATOR = ':';
        private const char DOT_NET_SEPARATOR = '-';
    }
}