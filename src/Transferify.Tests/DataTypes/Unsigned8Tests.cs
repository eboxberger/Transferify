using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;
using Transferify.Interfaces;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    public class Unsigned8Tests
    {
        [TestCase(new byte[] {0x00}, 0, Endianess.Little, (byte) 0)]
        [TestCase(new byte[] {0x01}, 0, Endianess.Little, (byte) 1)]
        [TestCase(new byte[] {0x64}, 0, Endianess.Little, (byte) 100)]
        [TestCase(new byte[] {0xFF}, 0, Endianess.Little, (byte) 255)]
        [TestCase(new byte[] {0x00, 0xAB}, 1, Endianess.Little, (byte) 171)]
        [TestCase(new byte[] {0xAB}, 0, Endianess.Big, (byte) 171)]
        [TestCase(new byte[] {0x00, 0xAB}, 1, Endianess.Big, (byte) 171)]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, Endianess endianess, byte expectedResult)
        {
            var transferItem = new Unsigned8(0, endianess);
            var mockBuffer = new FixedSizeBuffer(buffer, currentIndex);

            transferItem.Read(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 1);
            transferItem.Value.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldReturnTheAppropriateByteCount()
        {
            var transferItem = new Unsigned8();

            transferItem.SizeInBytes.Should().Be(1);
        }


        [TestCase(new byte[] {0x00}, 0, (byte) 0x00, Endianess.Little, new byte[] {0x00})]
        [TestCase(new byte[] {0x00}, 0, (byte) 0x01, Endianess.Little, new byte[] {0x01})]
        [TestCase(new byte[] {0x00}, 0, (byte) 0x64, Endianess.Little, new byte[] {0x64})]
        [TestCase(new byte[] {0x00}, 0, (byte) 0xFF, Endianess.Little, new byte[] {0xFF})]
        [TestCase(new byte[] {0x00}, 0, (byte) 0xAB, Endianess.Big, new byte[] {0xAB})]
        [TestCase(new byte[] {0x00, 0x00}, 1, (byte) 0xAB, Endianess.Little, new byte[] {0x00, 0xAB})]
        [TestCase(new byte[] {0x00, 0x00}, 1, (byte) 0xAB, Endianess.Big, new byte[] {0x00, 0xAB})]
        public void ShouldWriteToBufferAndIncrementTheCurrentIndex(byte[] bufferBefore, int currentIndex, byte value, Endianess endianess, byte[] bufferAfter)
        {
            var transferItem = new Unsigned8(value, endianess);
            var mockBuffer = new FixedSizeBuffer(bufferBefore, currentIndex);


            transferItem.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 1);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}