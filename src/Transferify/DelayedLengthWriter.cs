using System;
using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify
{
    public class DelayedLengthWriter : IDisposable
    {
        public DelayedLengthWriter(IDataBuffer dataBuffer, ITransferValue transferValue)
        {
            _dataBuffer = dataBuffer;
            _transferValue = transferValue;

            _startPosition = dataBuffer.Position;
            _itemSize = transferValue.SizeInBytes;

            dataBuffer.Skip(_itemSize);
        }

        #region IDisposable members

        public void Dispose()
        {
            var endPosition = _dataBuffer.Position;

            var writtenBytes = endPosition - _startPosition - _itemSize;
            _dataBuffer.Position = _startPosition;
            _transferValue.ConvertFromString(writtenBytes.ToString());
            _transferValue.Write(_dataBuffer);
            _dataBuffer.Position = endPosition;
        }

        #endregion

        #region Fields

        private readonly IDataBuffer _dataBuffer;
        private readonly ITransferValue _transferValue;
        private readonly int _startPosition;
        private readonly int _itemSize;

        #endregion
    }
}