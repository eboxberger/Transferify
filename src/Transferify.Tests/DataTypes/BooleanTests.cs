using FluentAssertions;
using NUnit.Framework;
using Boolean = Transferify.DataTypes.Boolean;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    class BooleanTests
    {
        [TestCase(new byte[] {0x00}, 0, false)]
        [TestCase(new byte[] {0x01}, 0, true)]
        [TestCase(new byte[] {0x00, 0x00}, 1, false)]
        [TestCase(new byte[] {0x00, 0xFF}, 1, true)]
        [TestCase(new byte[] {0x00, 0x00, 0x00}, 1, false)]
        [TestCase(new byte[] {0x00, 0xFF, 0x00}, 1, true)]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, bool expectedResult)
        {
            var transferItem = new Boolean();
            var mockBuffer = new FixedSizeBuffer(data: buffer, currentIndex: currentIndex);

            transferItem.Read(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 1);
            transferItem.Value.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldReturnTheAppropriateByteCount()
        {
            var transferItem = new Boolean();

            transferItem.SizeInBytes.Should().Be(1);
        }


        [TestCase(new byte[] {0x00}, 0, false, new byte[] {0x00})]
        [TestCase(new byte[] {0x00}, 0, true, new byte[] {0x01})]
        [TestCase(new byte[] {0x00, 0x00}, 1, true, new byte[] {0x00, 0x01})]
        [TestCase(new byte[] {0x00, 0x01, 0x01}, 1, false, new byte[] {0x00, 0x00, 0x01})]
        public void ShouldWriteToBufferAndIncrementTheCurrentIndex(byte[] bufferBefore, int currentIndex, bool value, byte[] bufferAfter)
        {
            var transferItem = new Boolean(value);
            var mockBuffer = new FixedSizeBuffer(data: bufferBefore, currentIndex: currentIndex);

            transferItem.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 1);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}