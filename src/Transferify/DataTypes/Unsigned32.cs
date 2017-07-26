using System;
using Transferify.DataTypes.Interfaces;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class Unsigned32 : Numeric<uint>, IUnsigned32
    {
        public Unsigned32(uint value = 0, Endianess endianess = Endianess.Little)
            : base(value, endianess, 4)
        {
        }

        #region Protected methods

        protected override byte[] ConvertToBytes()
        {
            return BitConverter.GetBytes(Value);
        }

        protected override uint ConvertFromBytes(byte[] value)
        {
            return BitConverter.ToUInt32(value, 0);
        }

        protected override uint OnConvertFromString(string value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion
    }
}