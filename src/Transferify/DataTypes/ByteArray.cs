using Transferify.DataTypes.Interfaces;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class ByteArray : IByteArray
    {
        public virtual int SizeInBytes => Value.Length;

        public Endianess Endianess { get; set; }

        public byte[] Value { get; set; }

        public ByteArray()
            : this(new byte[0], Endianess.Little)
        {
        }

        public ByteArray(byte[] value)
            : this(value, Endianess.Little)
        {
        }

        public ByteArray(Endianess endianess)
            : this(new byte[0], endianess)
        {
        }

        public ByteArray(byte[] value, Endianess endianess)
        {
            Value = value;
            Endianess = endianess;
        }

        public virtual void Read(IDataBuffer dataBuffer, int count = -1)
        {
            Value = dataBuffer.Read(count);
        }

        public virtual void Write(IDataBuffer dataBuffer, int count = -1)
        {
            dataBuffer.Write(Value);
        }
    }
}