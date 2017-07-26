using FluentAssertions;
using Moq;
using NUnit.Framework;
using Transferify.DataTypes;
using Transferify.Interfaces;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    class EnumerationTests
    {
        [Test]
        public void ShouldDelegateTheReadOperation()
        {
            var mockBuffer = new FixedSizeBuffer(new byte[] {0x00}, 0);

            var mockTransferValue = new Mock<ITransferValue>();
            mockTransferValue.Setup(mock => mock.Read(mockBuffer, -1)).Verifiable();

            var objectUnderTest = new Enumeration<SomeEnum>(mockTransferValue.Object);

            objectUnderTest.Read(mockBuffer);

            mockTransferValue.Verify();
        }

        [Test]
        public void ShouldDelegateTheWriteOperation()
        {
            var mockBuffer = new FixedSizeBuffer(new byte[] {0x00}, 0);

            var mockTransferValue = new Mock<ITransferValue>();
            mockTransferValue.Setup(mock => mock.Write(mockBuffer, -1)).Verifiable();

            var objectUnderTest = new Enumeration<SomeEnum>(mockTransferValue.Object);

            objectUnderTest.Write(mockBuffer);

            mockTransferValue.Verify();
        }

        [TestCase(Endianess.Little)]
        [TestCase(Endianess.Big)]
        public void ShouldGetTheEndianessOfUnderlyingItem(Endianess endianess)
        {
            var transferValue = new Unsigned8();
            var objectUnderTest = new Enumeration<SomeEnum>(transferValue);

            transferValue.Endianess = endianess;

            objectUnderTest.Endianess.Should().Be(endianess);
        }

        [TestCase(0x00, SomeEnum.En1)]
        [TestCase(0x01, SomeEnum.En2)]
        [TestCase(0xFF, SomeEnum.En3)]
        public void ShouldGetTheValueOfUnderlyingItem(byte value, SomeEnum expectedValue)
        {
            var transferValue = new Unsigned8();
            var objectUnderTest = new Enumeration<SomeEnum>(transferValue);

            transferValue.Value = value;

            objectUnderTest.Value.Should().Be(expectedValue);
        }

        [TestCase(Endianess.Little)]
        [TestCase(Endianess.Big)]
        public void ShouldSetTheEndianessOfUnderlyingItem(Endianess endianess)
        {
            var transferValue = new Unsigned8();
            var objectUnderTest = new Enumeration<SomeEnum>(transferValue)
            {
                Endianess = endianess
            };

            transferValue.Endianess.Should().Be(endianess);
        }

        [TestCase(SomeEnum.En1, 0x00)]
        [TestCase(SomeEnum.En2, 0x01)]
        [TestCase(SomeEnum.En3, 0xFF)]
        public void ShouldSetTheValueOfUnderlyingItem(SomeEnum value, byte expectedValue)
        {
            var transferValue = new Unsigned8();

            var objectUnderTest = new Enumeration<SomeEnum>(transferValue)
            {
                Value = value
            };

            transferValue.Value.Should().Be(expectedValue);
        }
    }

    enum SomeEnum : byte
    {
        En1 = 0x00,
        En2 = 0x01,
        En3 = 0xFF
    }
}