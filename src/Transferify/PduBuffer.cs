using Transferify.Extensions;
using Transferify.Interfaces;

namespace Transferify
{
    public class PduBuffer : IPduBuffer
    {
        public PduBuffer(IDataBuffer dataBuffer)
        {
            _dataBuffer = dataBuffer;
        }

        public PduBuffer()
        {
            _dataBuffer = new DynamicSizeBuffer();
        }

        #region IDataBuffer members

        public byte[] Read(int count)
        {
            return _dataBuffer.Read(count);
        }

        public void Write(byte[] values)
        {
            _dataBuffer.Write(values);
        }

        public void Clear()
        {
            _dataBuffer.Clear();
        }

        public byte[] BufferCopy => _dataBuffer.BufferCopy;

        public int Position
        {
            get => _dataBuffer.Position;
            set => _dataBuffer.Position = value;
        }

        public int Capacity => _dataBuffer.Capacity;

        #endregion

        #region IPduBuffer members

        public byte[] PopPdu()
        {
            var currentPisition = _dataBuffer.Position;
            _dataBuffer.ResetPosition();
            var value = _dataBuffer.Read(currentPisition);
            _dataBuffer.Clear();
            return value;
        }

        public void PopPdu(IReadable readable)
        {
            _dataBuffer.ResetPosition();
            readable.Read(this);
            _dataBuffer.Clear();
        }

        public void PushPdu(byte[] data)
        {
            _dataBuffer.ResetPosition();
            _dataBuffer.Write(data);
        }

        public void PushPdu(IWritable writable)
        {
            _dataBuffer.ResetPosition();
            writable.Write(this);
        }

        #endregion

        #region Fields

        private readonly IDataBuffer _dataBuffer;

        #endregion
    }
}