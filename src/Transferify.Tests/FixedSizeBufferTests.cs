using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Transferify.Extensions;

namespace Transferify.Tests
{
    [TestFixture]
    class FixedSizeBufferTests
    {
        [Test]
        public void ShouldClearTheDataBuffer()
        {
            var fixedSizeBuffer = new FixedSizeBuffer(new byte[] {0x01, 0x02, 0x03});

            fixedSizeBuffer.Clear();

            fixedSizeBuffer.Position.Should().Be(0);
            fixedSizeBuffer.BufferCopy.ShouldBeEquivalentTo(new byte[] {0x00, 0x00, 0x00});
        }

        [TestCase(0)]
        [TestCase(10)]
        [TestCase(1000)]
        public void ShouldCreateTheBufferWithPassedCapacity(int capacity)
        {
            var fixedSizeBuffer = new FixedSizeBuffer(capacity);

            fixedSizeBuffer.Capacity.Should().Be(capacity);
        }

        [Test]
        public void ShouldCreateTheBufferWithPredefinedData()
        {
            var numberOfBytes = 100;
            var initializedByteArray = TestHelper.CreateInitializedByteArray(numberOfBytes);

            var fixedSizeBuffer = new FixedSizeBuffer(initializedByteArray);

            fixedSizeBuffer.Position.Should().Be(0);
            fixedSizeBuffer.Capacity.Should().Be(numberOfBytes);
            fixedSizeBuffer.BufferCopy.ShouldBeEquivalentTo(initializedByteArray);
        }

        [TestCase]
        public void ShouldReadData()
        {
            var totalNumberOfBytes = 100;
            var numberOfBytesToRead = 50;
            var initializedByteArray = TestHelper.CreateInitializedByteArray(totalNumberOfBytes);

            var fixedSizeBuffer = new FixedSizeBuffer(initializedByteArray);

            var data = fixedSizeBuffer.Read(numberOfBytesToRead);

            data.Should().BeEquivalentTo(initializedByteArray.Take(numberOfBytesToRead));
        }

        [TestCase]
        public void ShouldThrowAnException()
        {
            var initializedByteArray = TestHelper.CreateInitializedByteArray(11);

            var fixedSizeBuffer = new FixedSizeBuffer(10);

            new Action(() => { fixedSizeBuffer.Write(initializedByteArray); }).ShouldThrow<BufferOverflowException>();
        }

        [Test]
        public void ShouldWriteDataToTheBuffer()
        {
            var bufferCapacity = 100;
            var numberOfBytesToWrite = 50;

            var initializedByteArray = TestHelper.CreateInitializedByteArray(numberOfBytesToWrite);

            var fixedSizeBuffer = new FixedSizeBuffer(bufferCapacity);

            fixedSizeBuffer.Write(initializedByteArray);

            fixedSizeBuffer.Position.Should().Be(initializedByteArray.Length);
            fixedSizeBuffer.ReadBytes().ShouldBeEquivalentTo(initializedByteArray);
        }
    }
}