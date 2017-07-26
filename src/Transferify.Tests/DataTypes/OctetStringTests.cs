using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    public class OctetStringTests
    {
        [TestCase(new byte[] {0x61}, 0, 1, "a")]
        [TestCase(new byte[] {0x00, 0x61}, 1, 1, "a")]
        [TestCase(new byte[] {0x00, 0x61}, 1, 0, "")]
        [TestCase(new byte[] {0x61, 0x62}, 0, 2, "ab")]
        [TestCase(new byte[] {0x61, 0x62, 0x00}, 0, 3, "ab")]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, int byteCount, string expectedResult)
        {
            var transferItem = new OctetString();
            var mockBuffer = new FixedSizeBuffer(buffer, currentIndex);

            transferItem.Read(mockBuffer, byteCount);

            mockBuffer.Position.Should().Be(currentIndex + byteCount);
            transferItem.Value.Should().Be(expectedResult);
        }


        [TestCase("", 0)]
        [TestCase("A", 1)]
        [TestCase("abc", 3)]
        public void ShouldReturnTheAppropriateByteCount(string value, int exptectedByteCount)
        {
            var transferItem = new OctetString(value);

            transferItem.SizeInBytes.Should().Be(exptectedByteCount);
        }


        [TestCase(new byte[] {0x00}, 0, "a", new byte[] {0x61})]
        [TestCase(new byte[] {0x00, 0x00}, 0, "ab", new byte[] {0x61, 0x62})]
        [TestCase(new byte[] {0x00, 0x00, 0x00}, 1, "ab", new byte[] {0x00, 0x61, 0x62})]
        public void ShouldWriteToBufferAndIncrementTheCurrentIndex(byte[] bufferBefore, int currentIndex, string value, byte[] bufferAfter)
        {
            var transferItem = new OctetString(value);
            var mockBuffer = new FixedSizeBuffer(bufferBefore, currentIndex);

            transferItem.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + transferItem.SizeInBytes);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}