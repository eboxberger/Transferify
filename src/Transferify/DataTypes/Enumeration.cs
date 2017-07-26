using System;
using Transferify.DataTypes.Interfaces;
using Transferify.Interfaces;

namespace Transferify.DataTypes
{
    public class Enumeration<TEnum> : IEnumeration<TEnum>
        where TEnum : struct
    {
        public Endianess Endianess
        {
            get => _transferValue.Endianess;
            set => _transferValue.Endianess = value;
        }

        public TEnum Value
        {
            get => (TEnum) Enum.Parse(typeof(TEnum), _transferValue.Value.ToString());
            set => _transferValue.Value = value;
        }

        public Enumeration(ITransferValue transferValue)
        {
            _transferValue = transferValue;
        }

        public void Read(IDataBuffer dataBuffer, int count = -1)
        {
            _transferValue.Read(dataBuffer, count);
        }

        public void Write(IDataBuffer dataBuffer, int count = -1)
        {
            _transferValue.Write(dataBuffer, count);
        }

        #region Fields

        private readonly ITransferValue _transferValue;

        #endregion
    }
}