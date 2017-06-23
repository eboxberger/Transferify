using System.Collections.Generic;
using Transferify.Interfaces;

namespace Transferify
{
    public class DataBufferSeeker
    {
        public DataBufferSeeker(IDataBuffer dataBuffer)
        {
            _dataBuffer = dataBuffer;
            _indexLookupDict = new Dictionary<string, int>();
            _isSeekInProgress = false;
        }

        public int GetDifferenceTo(string positionKey)
        {
            var index = _indexLookupDict[positionKey];

            if (_isSeekInProgress)
                return _lastReachedPosition - index;
            return _dataBuffer.Position - index;
        }

        public void SavePosition(string positionKey)
        {
            _indexLookupDict.Add(positionKey, _dataBuffer.Position);
        }

        public void SeekReset()
        {
            _dataBuffer.Position = _lastReachedPosition;
        }


        public void SeekToPosition(string positionKey)
        {
            if (!_isSeekInProgress)
            {
                _lastReachedPosition = _dataBuffer.Position;
                _isSeekInProgress = true;
            }

            _dataBuffer.Position = _indexLookupDict[positionKey];
        }

        #region Fields

        private readonly IDataBuffer _dataBuffer;
        private readonly Dictionary<string, int> _indexLookupDict;
        private int _lastReachedPosition;
        private bool _isSeekInProgress;

        #endregion
    }
}