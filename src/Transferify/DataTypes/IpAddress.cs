using System.Net;
using Transferify.DataTypes.Interfaces;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class IpAddress : IIpAddress
    {
        public IpAddress(IPAddress value = null)
        {
            Value = value ?? new IPAddress(new byte[] {0x00, 0x00, 0x00, 0x00});
            Endianess = Endianess.None;
        }

        public IpAddress(string ipAddress)
            : this(IPAddress.Parse(ipAddress))
        {
        }

        #region IReadable members

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            var bytes = dataBuffer.Read(SizeInBytes);
            Value = new IPAddress(bytes);
        }

        #endregion

        #region IKnowMySize members

        public int SizeInBytes => 4;

        #endregion

        #region ITransferValueT<IPAddress> members

        public IPAddress Value { get; set; }
        public Endianess Endianess { get; set; }

        #endregion

        #region IWritable members

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            var bytes = Value.GetAddressBytes();
            dataBuffer.Write(bytes);
        }

        #endregion
    }
}