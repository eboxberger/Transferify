using System.Collections.Generic;
using Transferify.Interfaces;

namespace Transferify
{
    public class DataBufferPositionTagger
    {
        public DataBufferPositionTagger(IDataBuffer dataBuffer)
        {
            _dataBuffer = dataBuffer;
            _positionTags = new Dictionary<string, int>();
        }

        public void CreatePositionTag(string tag)
        {
            _positionTags.Add(tag, _dataBuffer.Position);
        }

        public void GoToPosition(string tag)
        {
            _dataBuffer.Position = _positionTags[tag];
        }

        public int GetPosition(string tag)
        {
            return _positionTags[tag];
        }


        public int GetDistance(string tag1, string tag2)
        {
            return _positionTags[tag2] - _positionTags[tag1];
        }

        #region Fields

        private readonly IDataBuffer _dataBuffer;
        private readonly Dictionary<string, int> _positionTags;

        #endregion
    }
}