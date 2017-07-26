using System;

namespace Transferify
{
    public class BufferOverflowException : Exception
    {
        public BufferOverflowException()
        {
        }

        public BufferOverflowException(string message) : base(message)
        {
        }

        public BufferOverflowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}