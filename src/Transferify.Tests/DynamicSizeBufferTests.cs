using FluentAssertions;
using NUnit.Framework;
using Transferify.Extensions;

namespace Transferify.Tests
{
    [TestFixture]
    class DynamicSizeBufferTests
    {
        [Test]
        public void PositionShouldBeZeroAfterCreation()
        {
            var dynamicBuffer = new DynamicSizeBuffer();

            dynamicBuffer.Position.Should().Be(0);
        }

        [Test]
        public void SholdReturnTheDefaultInitialCapacity()
        {
            var dynamicBuffer = new DynamicSizeBuffer();

            dynamicBuffer.Capacity.Should().Be(500);
        }

        [Test]
        public void ShouldReadFromTheBuffer()
        {
            var dynamicBuffer = new DynamicSizeBuffer();

            var values = TestHelper.CreateInitializedByteArray(100);

            dynamicBuffer.Write(values);

            dynamicBuffer.Position = 0;
            var result = dynamicBuffer.Read(100);

            result.Should().BeEquivalentTo(values);
        }


        [TestCase(0, 500)]
        [TestCase(500, 500)]
        [TestCase(501, 1000)]
        [TestCase(999, 1000)]
        [TestCase(1000, 1000)]
        [TestCase(1001, 1500)]
        public void ShouldWriteToTheBufferAndIncreaseTheCapacityIfNeeded(int numberOfBytesToWrite, int expectedCapacity)
        {
            var dynamicBuffer = new DynamicSizeBuffer();

            var values = TestHelper.CreateInitializedByteArray(numberOfBytesToWrite);

            dynamicBuffer.Write(values);

            dynamicBuffer.Capacity.Should().Be(expectedCapacity);
            dynamicBuffer.Position.Should().Be(values.Length);
            dynamicBuffer.ReadBytes().ShouldBeEquivalentTo(values);
        }
    }
}