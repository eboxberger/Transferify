using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;
using Transferify.Interfaces;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    public class Unsigned16Tests
    {
        [TestCase(new byte[] {0x12, 0x34}, 0, Endianess.Little, (ushort) 0x3412)]
        [TestCase(new byte[] {0x12, 0x34}, 0, Endianess.Big, (ushort) 0x1234)]
        [TestCase(new byte[] {0x00, 0x12, 0x34}, 1, Endianess.Little, (ushort) 0x3412)]
        [TestCase(new byte[] {0x00, 0x12, 0x34}, 1, Endianess.Big, (ushort) 0x1234)]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, Endianess endianess, ushort expectedResult)
        {
            var transferItem = new Unsigned16(0, endianess);
            var mockBuffer = new FixedSizeBuffer(buffer, currentIndex);

            transferItem.Read(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 2);
            transferItem.Value.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldReturnTheAppropriateByteCount()
        {
            var transferItem = new Unsigned16();

            transferItem.SizeInBytes.Should().Be(2);
        }


        [TestCase(new byte[] {0x00, 0x00}, 0, (ushort) 0x3412, Endianess.Little, new byte[] {0x12, 0x34})]
        [TestCase(new byte[] {0x00, 0x00}, 0, (ushort) 0x3412, Endianess.Big, new byte[] {0x34, 0x12})]
        [TestCase(new byte[] {0x00, 0x00, 0x00}, 1, (ushort) 0x3412, Endianess.Little, new byte[] {0x00, 0x12, 0x34})]
        [TestCase(new byte[] {0x00, 0x00, 0x00}, 1, (ushort) 0x3412, Endianess.Big, new byte[] {0x00, 0x34, 0x12})]
        public void ShouldWriteToTheBuffer(byte[] bufferBefore, int currentIndex, ushort value, Endianess endianess, byte[] bufferAfter)
        {
            var transferItem = new Unsigned16(value, endianess);
            var mockBuffer = new FixedSizeBuffer(bufferBefore, currentIndex);

            transferItem.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 2);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}