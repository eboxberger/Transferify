using System.Linq;
using System.Text;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public abstract class String : ITransferValueT<string>, IKnowMySize
    {
        protected String(string value = "")
        {
            Value = value;
            Endianess = Endianess.None;
        }

        #region IReadable members

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            if (count == 0)
            {
                Value = string.Empty;
                return;
            }

            var bytes = dataBuffer.Read(count);
            var newCount = bytes.Last() == NULL_CHARACTER ? bytes.Length - 1 : bytes.Length;
            Value = Encoding.ASCII.GetString(bytes, 0, newCount);
        }

        #endregion

        #region IKnowMySize members

        public int SizeInBytes => Value.Length;

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
            var bytes = Encoding.ASCII.GetBytes(Value);
            dataBuffer.Write(bytes);
        }

        #endregion

        private const int NULL_CHARACTER = 0x00;
    }
}