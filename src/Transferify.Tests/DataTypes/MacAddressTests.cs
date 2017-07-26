using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    class MacAddressTests
    {
        [Test]
        public void ShouldHaveDefaultMacAddress()
        {
            var macAddress = new MacAddress();

            macAddress.Value.Should().Be("00:00:00:00:00:00");
        }

        [TestCase(new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06}, 0, "01:02:03:04:05:06")]
        [TestCase(new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06}, 1, "01:02:03:04:05:06")]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, string expectedResult)
        {
            var macAddress = new MacAddress();
            var mockBuffer = new FixedSizeBuffer(buffer, currentIndex);

            macAddress.Read(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 6);
            macAddress.Value.Should().Be(expectedResult);
        }

        [Test]
        public void ShouldReturnTheAppropriateByteCount()
        {
            var transferItem = new MacAddress();

            transferItem.SizeInBytes.Should().Be(6);
        }

        [Test]
        public void ShouldSetTheDefaultMacAddress()
        {
            var macAddress = new MacAddress("01:02:03:04:05:06");

            macAddress.Value.Should().Be("01:02:03:04:05:06");
        }

        [TestCase(new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06}, 0, "01:02:03:04:05:06", new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06})]
        [TestCase(new byte[] {0xFF, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06}, 1, "01:02:03:04:05:06", new byte[] {0xFF, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06})]
        public void ShouldWriteToBufferAndIncrementTheCurrentIndex(byte[] bufferBefore, int currentIndex, string value, byte[] bufferAfter)
        {
            var macAddress = new MacAddress(value);
            var mockBuffer = new FixedSizeBuffer(bufferBefore, currentIndex);

            macAddress.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 6);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}