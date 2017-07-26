using System;
using Transferify.DataTypes.Interfaces;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class Unsigned16 : Numeric<ushort>, IUnsigned16
    {
        public Unsigned16(ushort value = 0, Endianess endianess = Endianess.Little)
            : base(value, endianess, 2)
        {
        }

        #region Protected methods

        protected override byte[] ConvertToBytes()
        {
            return BitConverter.GetBytes(Value);
        }

        protected override ushort ConvertFromBytes(byte[] value)
        {
            return BitConverter.ToUInt16(value, 0);
        }

        protected override ushort OnConvertFromString(string value)
        {
            return Convert.ToUInt16(value);
        }

        #endregion
    }
}