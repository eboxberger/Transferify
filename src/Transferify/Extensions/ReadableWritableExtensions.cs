using System.Collections.Generic;
using Transferify.Interfaces;

namespace Transferify.Extensions
{
    public static class ReadableWritableExtensions
    {
        public static byte[] Data(this IWritable writable)
        {
            var dataBuffer = new FixedSizeBuffer(BUFFER_SIZE);
            writable.Write(dataBuffer);
            return dataBuffer.ReadBytes();
        }

        public static byte[] Data(this IEnumerable<IWritable> writables)
        {
            var dataBuffer = new FixedSizeBuffer(BUFFER_SIZE);
            foreach (var writable in writables)
                writable.Write(dataBuffer);

            return dataBuffer.ReadBytes();
        }

        public static IDataBuffer CreateDataBuffer(this IWritable writable)
        {
            var writableData = writable.Data();
            return new FixedSizeBuffer(writableData, 0);

        }

        public static int SizeInBytes(this IWritable writable)
        {
            var item = writable as IKnowMySize;
            if (item != null)
                return item.SizeInBytes;

            return writable.Data().Length;
        }

        private const int BUFFER_SIZE = 1500;
    }
}