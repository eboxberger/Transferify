using FluentAssertions;
using NUnit.Framework;

namespace Transferify.Tests
{
    [TestFixture]
    class DataBufferSeekerTests
    {
        [Test]
        public void ShouldReturnTheDifferenceToASpecificPosition()
        {
            var currentPosition = 5;
            var mockBuffer = new FixedSizeBuffer(new byte[20], currentPosition);
            var dataBufferSeeker = new DataBufferSeeker(mockBuffer);

            var positionKey1 = "Key1";
            var positionKey2 = "Key2";

            mockBuffer.Position = 7;
            dataBufferSeeker.SavePosition(positionKey1);
            mockBuffer.Position = 9;
            dataBufferSeeker.SavePosition(positionKey2);
            mockBuffer.Position = 15;

            var byteCount1 = dataBufferSeeker.GetDifferenceTo(positionKey1);
            byteCount1.Should().Be(8);

            var byteCount2 = dataBufferSeeker.GetDifferenceTo(positionKey2);
            byteCount2.Should().Be(6);
        }

        [Test]
        public void ShouldSeekToTheLastReachedPosition()
        {
            var currentPosition = 5;
            var mockBuffer = new FixedSizeBuffer(new byte[20], currentPosition);
            var dataBufferSeeker = new DataBufferSeeker(mockBuffer);

            var positionKey1 = "Key1";
            var positionKey2 = "Key2";

            mockBuffer.Position = 7;
            dataBufferSeeker.SavePosition(positionKey1);
            mockBuffer.Position = 9;
            dataBufferSeeker.SavePosition(positionKey2);
            mockBuffer.Position = 10;

            dataBufferSeeker.SeekToPosition(positionKey1);
            mockBuffer.Position.Should().Be(7);

            dataBufferSeeker.SeekToPosition(positionKey2);
            mockBuffer.Position.Should().Be(9);

            dataBufferSeeker.SeekReset();

            mockBuffer.Position.Should().Be(10);
        }

        [Test]
        public void ShouldSeekToTheLastReachedPosition2()
        {
            var currentPosition = 5;
            var mockBuffer = new FixedSizeBuffer(new byte[20], currentPosition);
            var dataBufferSeeker = new DataBufferSeeker(mockBuffer);

            var positionKey1 = "Key1";

            mockBuffer.Position = 7;
            dataBufferSeeker.SavePosition(positionKey1);
            mockBuffer.Position = 10;

            dataBufferSeeker.SeekToPosition(positionKey1);
            mockBuffer.Position.Should().Be(7);

            dataBufferSeeker.SeekReset();

            mockBuffer.Position.Should().Be(10);
        }
    }
}