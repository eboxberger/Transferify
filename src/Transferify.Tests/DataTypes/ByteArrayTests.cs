using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    public class ByteArrayTests
    {
        [TestCase(new byte[] {0x00, 0xFE, 0xFF}, 1, 2, new byte[] {0xFE, 0xFF})]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, int byteCount, byte[] expectedResult)
        {
            var transferItem = new ByteArray();
            var mockBuffer = new FixedSizeBuffer(data: buffer, currentIndex: currentIndex);

            transferItem.Read(mockBuffer, byteCount);

            mockBuffer.Position.Should().Be(currentIndex + byteCount);
            transferItem.Value.ShouldBeEquivalentTo(expectedResult);
        }

        [TestCase(new byte[] { }, 0)]
        [TestCase(new byte[] {0x00}, 1)]
        [TestCase(new byte[] {0x00, 0x00}, 2)]
        public void ShouldReturnTheAppropriateByteCount(byte[] value, int expectedByteCount)
        {
            var transferItem = new ByteArray(value);

            transferItem.SizeInBytes.Should().Be(expectedByteCount);
        }


        [TestCase(new byte[] {0x00, 0x00, 0x00}, 1, new byte[] {0xAB, 0xCD}, new byte[] {0x00, 0xAB, 0xCD})]
        public void ShouldWriteToBufferAndIncrementTheCurrentIndex(byte[] bufferBefore, int currentIndex, byte[] value, byte[] bufferAfter)
        {
            var transferItem = new ByteArray(value);
            var mockBuffer = new FixedSizeBuffer(data: bufferBefore, currentIndex: currentIndex);

            transferItem.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + value.Length);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}