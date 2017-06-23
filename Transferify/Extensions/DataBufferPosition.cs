using Transferify.Interfaces;

namespace Transferify.Extensions
{
    public static class DataBufferPosition
    {
        public static void AlignTo(this IDataBuffer dataBuffer, int number)
        {
            var remainder = dataBuffer.Position % number;
            if (remainder > 0)
                dataBuffer.Position += number - remainder;
        }

        public static void AlignTo2(this IDataBuffer dataBuffer)
        {
            AlignTo(dataBuffer, 2);
        }

        public static void AlignTo4(this IDataBuffer dataBuffer)
        {
            AlignTo(dataBuffer, 4);
        }

        public static void DecrementPosition(this IDataBuffer dataBuffer)
        {
            dataBuffer.Position--;
        }

        public static void DecrementPosition(this IDataBuffer dataBuffer, int decrement)
        {
            dataBuffer.Position -= decrement;
        }

        public static void IncrementPosition(this IDataBuffer dataBuffer)
        {
            dataBuffer.Position++;
        }

        public static void IncrementPosition(this IDataBuffer dataBuffer, int increment)
        {
            dataBuffer.Position += increment;
        }

        public static bool IsEnd(this IDataBuffer dataBuffer)
        {
            return dataBuffer.Position == dataBuffer.Capacity;
        }

        public static void Skip(this IDataBuffer dataBuffer, int bytesToSkip)
        {
            dataBuffer.Position += bytesToSkip;
        }

        public static void Skip2(this IDataBuffer dataBuffer)
        {
            Skip(dataBuffer, 2);
        }

        public static void Skip4(this IDataBuffer dataBuffer)
        {
            Skip(dataBuffer, 4);
        }

        public static void ResetPosition(this IDataBuffer dataBuffer)
        {
            dataBuffer.Position = 0;
        }
    }
}