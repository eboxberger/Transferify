using System.Linq;

namespace Transferify.Tests
{
    static class TestHelper
    {
        public static byte[] CreateInitializedByteArray(int count)
        {
            return new byte[count].Select((valu, index) => (byte)index).ToArray();
        }
    }
}