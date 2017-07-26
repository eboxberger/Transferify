using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    class LowNibbleTests
    {
        [TestCase(new byte[] {0x00}, 0, 0x00)]
        [TestCase(new byte[] {0xF0}, 0, 0x00)]
        [TestCase(new byte[] {0x0F}, 0, 0x0F)]
        [TestCase(new byte[] {0x00, 0x0A}, 1, 0x0A)]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, byte expectedResult)
        {
            var transferItem = new LowNibble();
            var mockBuffer = new FixedSizeBuffer(buffer, currentIndex);

            transferItem.Read(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 1);
            transferItem.Value.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldReturnTheAppropriateByteCount()
        {
            var transferItem = new LowNibble();

            transferItem.SizeInBytes.Should().Be(1);
        }


        [TestCase(new byte[] {0x00}, 0, 0x0F, new byte[] {0x0F})]
        [TestCase(new byte[] {0xF0}, 0, 0x0F, new byte[] {0xFF})]
        [TestCase(new byte[] {0xFF, 0xFF}, 1, 0x00, new byte[] {0xFF, 0xF0})]
        [TestCase(new byte[] {0xFF, 0xFF}, 1, 0x00, new byte[] {0xFF, 0xF0})]
        [TestCase(new byte[] {0xFF, 0xFF, 0xAB}, 1, 0x0C, new byte[] {0xFF, 0xFC, 0xAB})]
        public void ShouldWriteToBufferAndIncrementTheCurrentIndex(byte[] bufferBefore, int currentIndex, byte value, byte[] bufferAfter)
        {
            var transferItem = new LowNibble(value);
            var mockBuffer = new FixedSizeBuffer(bufferBefore, currentIndex);

            transferItem.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 1);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}