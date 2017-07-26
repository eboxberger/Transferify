using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    class IpAddressTests
    {
        [Test]
        public void ShouldAcceptAStringWithinTheConstructor()
        {
            var address = "1.2.3.4";

            var ipAddress = new IpAddress(address);

            ipAddress.Value.ToString().Should().Be(address);
        }

        [Test]
        public void ShouldHaveDefaultIpAddress()
        {
            var ipAddress = new IpAddress();

            ipAddress.Value.ToString().Should().Be("0.0.0.0");
        }

        [TestCase(new byte[] {0x01, 0x02, 0x03, 0x04}, 0, "1.2.3.4")]
        [TestCase(new byte[] {0x00, 0x01, 0x02, 0x03, 0x04}, 1, "1.2.3.4")]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, string expectedResult)
        {
            var transferItem = new IpAddress();
            var mockBuffer = new FixedSizeBuffer(buffer, currentIndex);

            transferItem.Read(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 4);
            transferItem.Value.ToString().Should().Be(expectedResult);
        }


        [Test]
        public void ShouldReturnTheAppropriateByteCount()
        {
            var transferItem = new IpAddress();

            transferItem.SizeInBytes.Should().Be(4);
        }


        [TestCase(new byte[] {0x00, 0x00, 0x00, 0x00}, 0, "1.2.3.4", new byte[] {0x01, 0x02, 0x03, 0x04})]
        [TestCase(new byte[] {0x00, 0x00, 0x00, 0x00, 0x00}, 1, "1.2.3.4", new byte[] {0x00, 0x01, 0x02, 0x03, 0x04})]
        public void ShouldWriteToBufferAndIncrementTheCurrentIndex(byte[] bufferBefore, int currentIndex, string value, byte[] bufferAfter)
        {
            var transferItem = new IpAddress(value);
            var mockBuffer = new FixedSizeBuffer(bufferBefore, currentIndex);

            transferItem.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 4);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}