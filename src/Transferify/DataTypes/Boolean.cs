using Transferify.DataTypes.Interfaces;
using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class Boolean : IBoolean
    {
        public int SizeInBytes => 1;

        public bool Value { get; set; }
        public Endianess Endianess { get; set; }

        public Boolean()
            : this(false, Endianess.Little)
        {
        }

        public Boolean(bool value)
            : this(value, Endianess.Little)
        {
            Value = value;
        }

        public Boolean(Endianess endianess)
            : this(false, endianess)
        {
            Endianess = endianess;
        }


        public Boolean(bool value, Endianess endianess)
        {
            Value = value;
            Endianess = endianess;
        }

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            Value = dataBuffer.ReadByte() != 0;
        }

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            var value = Value ? (byte) 0x01 : (byte) 0x00;
            dataBuffer.WriteByte(value);
        }
    }
}