using System;
using Transferify.DataTypes.Interfaces;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class Unsigned8 : Numeric<byte>, IUnsigned8
    {
        public Unsigned8(byte value = 0, Endianess endianess = Endianess.Little)
            : base(value, endianess, 1)
        {
        }

        #region Protected methods

        protected override byte[] ConvertToBytes()
        {
            return new[] {Value};
        }

        protected override byte ConvertFromBytes(byte[] value)
        {
            return value[0];
        }

        protected override byte OnConvertFromString(string value)
        {
            return Convert.ToByte(value);
        }

        #endregion
    }
}