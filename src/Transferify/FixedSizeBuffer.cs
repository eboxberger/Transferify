namespace Transferify
{
    public class FixedSizeBuffer : DataBuffer
    {
        public FixedSizeBuffer(int capacity)
            : base(capacity)
        {
        }

        public FixedSizeBuffer(byte[] data, int currentIndex = 0)
            : base(data, currentIndex)
        {
        }

        #region Protected methods

        protected override void OnBeforeWrite(byte[] values)
        {
            var remainder = Capacity - Position;
            if (remainder < values.Length)
                throw new BufferOverflowException();
        }

        #endregion
    }
}