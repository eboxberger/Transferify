using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;
using Transferify.Interfaces;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    public class Unsigned32Tests
    {
        [TestCase(new byte[] {0x12, 0x34, 0x56, 0x78}, 0, Endianess.Little, (uint) 0x78563412)]
        [TestCase(new byte[] {0x12, 0x34, 0x56, 0x78}, 0, Endianess.Big, (uint) 0x12345678)]
        [TestCase(new byte[] {0x00, 0x12, 0x34, 0x56, 0x78}, 1, Endianess.Little, (uint) 0x78563412)]
        [TestCase(new byte[] {0x00, 0x12, 0x34, 0x56, 0x78}, 1, Endianess.Big, (uint) 0x12345678)]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, Endianess endianess, uint expectedResult)
        {
            var transferItem = new Unsigned32(0, endianess);
            var mockBuffer = new FixedSizeBuffer(buffer, currentIndex);

            transferItem.Read(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 4);
            transferItem.Value.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldReturnTheAppropriateByteCount()
        {
            var transferItem = new Unsigned32();

            transferItem.SizeInBytes.Should().Be(4);
        }


        [TestCase(new byte[] {0x00, 0x00, 0x00, 0x00}, 0, (uint) 0x78563412, Endianess.Little, new byte[] {0x12, 0x34, 0x56, 0x78})]
        [TestCase(new byte[] {0x00, 0x00, 0x00, 0x00}, 0, (uint) 0x78563412, Endianess.Big, new byte[] {0x78, 0x56, 0x34, 0x12})]
        [TestCase(new byte[] {0x00, 0x00, 0x00, 0x00, 0x00}, 1, (uint) 0x78563412, Endianess.Little, new byte[] {0x00, 0x12, 0x34, 0x56, 0x78})]
        [TestCase(new byte[] {0x00, 0x00, 0x00, 0x00, 0x00}, 1, (uint) 0x78563412, Endianess.Big, new byte[] {0x00, 0x78, 0x56, 0x34, 0x12})]
        public void ShouldWriteToTheBuffer(byte[] bufferBefore, int currentIndex, uint value, Endianess endianess, byte[] bufferAfter)
        {
            var transferItem = new Unsigned32(value, endianess);
            var mockBuffer = new FixedSizeBuffer(bufferBefore, currentIndex);

            transferItem.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 4);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}