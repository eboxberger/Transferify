using FluentAssertions;
using Moq;
using NUnit.Framework;
using Transferify.Interfaces;

namespace Transferify.Tests
{
    [TestFixture]
    class PduBufferTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockDataBuffer = new Mock<IDataBuffer>();
        }

        [Test]
        public void ShouldDelegateTheClearRequest()
        {
            _mockDataBuffer.Setup(mock => mock.Clear()).Verifiable();

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            pduBuffer.Clear();

            _mockDataBuffer.Verify();
        }

        [TestCase(5, new byte[] {0x00, 0x01, 0x02, 0x03, 0x0})]
        public void ShouldDelegateTheReadRequestToTheUnderlyingBuffer(int count, byte[] value)
        {
            _mockDataBuffer.Setup(mock => mock.Read(count)).Returns(value);

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            var result = pduBuffer.Read(count);

            result.ShouldBeEquivalentTo(value);
        }

        [TestCase(new byte[] {0x00, 0x01, 0x02, 0x03, 0x4})]
        public void ShouldDelegateTheWriteRequestToTheUnderlyingBuffer(byte[] value)
        {
            _mockDataBuffer.Setup(mock => mock.Write(value)).Verifiable();

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            pduBuffer.Write(value);

            _mockDataBuffer.Verify();
        }

        [Test]
        public void ShouldPopPdu1()
        {
            var value = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04};

            _mockDataBuffer.SetupGet(mock => mock.Position).Returns(value.Length);
            _mockDataBuffer.Setup(mock => mock.Read(value.Length)).Returns(value);
            _mockDataBuffer.Setup(mock => mock.Clear()).Verifiable();

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            var pduBytes = pduBuffer.PopPdu();

            pduBytes.ShouldBeEquivalentTo(value);
            _mockDataBuffer.Verify();
        }

        [Test]
        public void ShouldPopPdu2()
        {
            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            _mockDataBuffer.SetupSet(mock => mock.Position = 0).Verifiable();
            _mockDataBuffer.Setup(mock => mock.Clear()).Verifiable();

            var mockReadable = new Mock<IReadable>();
            mockReadable.Setup(mock => mock.Read(pduBuffer, -1)).Verifiable();

            pduBuffer.PopPdu(mockReadable.Object);

            _mockDataBuffer.Verify();
            mockReadable.Verify();
        }

        [Test]
        public void ShouldPushPdu1()
        {
            var value = new byte[] {0x00, 0x01, 0x02, 0x03, 0x4};
            _mockDataBuffer.Setup(mock => mock.Write(value)).Verifiable();
            _mockDataBuffer.SetupSet(mock => mock.Position = 0).Verifiable();

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            pduBuffer.PushPdu(value);

            _mockDataBuffer.Verify();
        }

        [Test]
        public void ShouldPushPdu2()
        {
            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            _mockDataBuffer.SetupSet(mock => mock.Position = 0).Verifiable();

            var mockWritable = new Mock<IWritable>();
            mockWritable.Setup(mock => mock.Write(pduBuffer, -1)).Verifiable();

            pduBuffer.PushPdu(mockWritable.Object);

            _mockDataBuffer.Verify();
            mockWritable.Verify();
        }


        [TestCase(new byte[] {0x00, 0x01, 0x02, 0x03, 004})]
        public void ShouldReturnTheBufferCopyOfTherUnderlyingBuffer(byte[] value)
        {
            _mockDataBuffer.SetupGet(mock => mock.BufferCopy).Returns(value);

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            pduBuffer.BufferCopy.ShouldBeEquivalentTo(value);
        }

        [TestCase(1)]
        [TestCase(10)]
        public void ShouldReturnTheCapacityOfUnderlyingBuffer(int capacity)
        {
            _mockDataBuffer.SetupGet(mock => mock.Capacity).Returns(capacity);

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            var bufferCapacity = pduBuffer.Capacity;

            bufferCapacity.Should().Be(capacity);
        }

        [TestCase(0)]
        [TestCase(10)]
        [TestCase(100)]
        public void ShouldReturnThePositionOfTheUnderlyingBuffer(int position)
        {
            _mockDataBuffer.SetupGet(mock => mock.Position).Returns(position);

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object);

            pduBuffer.Position.Should().Be(position);
        }


        [TestCase(0)]
        [TestCase(10)]
        [TestCase(100)]
        public void ShouldSetThePositionOfTheUnderlyingBuffer(int position)
        {
            _mockDataBuffer.SetupSet(mock => mock.Position = position).Verifiable();

            var pduBuffer = new PduBuffer(_mockDataBuffer.Object)
            {
                Position = position
            };

            _mockDataBuffer.Verify();
        }


        private Mock<IDataBuffer> _mockDataBuffer;
    }
}