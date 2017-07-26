using System;
using FluentAssertions;
using NUnit.Framework;
using Transferify.DataTypes;
using Transferify.Interfaces;

namespace Transferify.Tests.DataTypes
{
    [TestFixture]
    class UuidTests
    {
        [Test]
        public void GuidShouldBeEmtptyByDefault()
        {
            var objectUnderTest = new Uuid();

            objectUnderTest.Value.Should().Be(Guid.Empty);
        }

        [Test]
        public void ShouldCreateAnInstanceByPassingAString()
        {
            var guid = "0f8fad5b-d9cb-469f-a165-70867728950e";

            var objectUnderTest = new Uuid(guid);

            objectUnderTest.Value.Should().Be(Guid.Parse(guid));
        }


        [TestCase(
            new byte[] {0x0f, 0x8f, 0xad, 0x5b, 0xd9, 0xcb, 0x46, 0x9f, 0xa1, 0x65, 0x70, 0x86, 0x77, 0x28, 0x95, 0x0e},
            0,
            Endianess.Big,
            "0f8fad5b-d9cb-469f-a165-70867728950e")]
        [TestCase(
            new byte[] {0x5b, 0xad, 0x8f, 0x0f, 0xcb, 0xd9, 0x9f, 0x46, 0xa1, 0x65, 0x70, 0x86, 0x77, 0x28, 0x95, 0x0e},
            0,
            Endianess.Little,
            "0f8fad5b-d9cb-469f-a165-70867728950e")]
        [TestCase(
            new byte[] {0x00, 0x0f, 0x8f, 0xad, 0x5b, 0xd9, 0xcb, 0x46, 0x9f, 0xa1, 0x65, 0x70, 0x86, 0x77, 0x28, 0x95, 0x0e},
            1,
            Endianess.Big,
            "0f8fad5b-d9cb-469f-a165-70867728950e")]
        [TestCase(new byte[] {0x00, 0x5b, 0xad, 0x8f, 0x0f, 0xcb, 0xd9, 0x9f, 0x46, 0xa1, 0x65, 0x70, 0x86, 0x77, 0x28, 0x95, 0x0e},
            1,
            Endianess.Little,
            "0f8fad5b-d9cb-469f-a165-70867728950e")]
        public void ShouldReadFromBufferAndIncrementTheCurrentIndex(byte[] buffer, int currentIndex, Endianess endianess, string expectedValue)
        {
            var objectUnderTest = new Uuid(endianess);
            var mockBuffer = new FixedSizeBuffer(buffer, currentIndex);

            objectUnderTest.Read(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 16);
            objectUnderTest.Value.Should().Be(Guid.Parse(expectedValue));
        }


        [Test]
        public void ShouldReturnTheAppropriateByteCount()
        {
            var transferItem = new Uuid();

            transferItem.SizeInBytes.Should().Be(16);
        }


        [TestCase(
            new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            0,
            "0f8fad5b-d9cb-469f-a165-70867728950e",
            Endianess.Big,
            new byte[] {0x0f, 0x8f, 0xad, 0x5b, 0xd9, 0xcb, 0x46, 0x9f, 0xa1, 0x65, 0x70, 0x86, 0x77, 0x28, 0x95, 0x0e})]
        [TestCase(
            new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            0,
            "0f8fad5b-d9cb-469f-a165-70867728950e",
            Endianess.Little,
            new byte[] {0x5b, 0xad, 0x8f, 0x0f, 0xcb, 0xd9, 0x9f, 0x46, 0xa1, 0x65, 0x70, 0x86, 0x77, 0x28, 0x95, 0x0e})]
        [TestCase(
            new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            1,
            "0f8fad5b-d9cb-469f-a165-70867728950e",
            Endianess.Big,
            new byte[] {0x00, 0x0f, 0x8f, 0xad, 0x5b, 0xd9, 0xcb, 0x46, 0x9f, 0xa1, 0x65, 0x70, 0x86, 0x77, 0x28, 0x95, 0x0e})]
        [TestCase(
            new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            1,
            "0f8fad5b-d9cb-469f-a165-70867728950e",
            Endianess.Little,
            new byte[] {0x00, 0x5b, 0xad, 0x8f, 0x0f, 0xcb, 0xd9, 0x9f, 0x46, 0xa1, 0x65, 0x70, 0x86, 0x77, 0x28, 0x95, 0x0e})]
        public void ShouldWriteToBufferAndIncrementTheCurrentIndex(byte[] bufferBefore, int currentIndex, string value, Endianess endianess, byte[] bufferAfter)
        {
            var uuid = new Uuid(value, endianess);
            var mockBuffer = new FixedSizeBuffer(bufferBefore, currentIndex);

            uuid.Write(mockBuffer);

            mockBuffer.Position.Should().Be(currentIndex + 16);
            mockBuffer.BufferCopy.ShouldBeEquivalentTo(bufferAfter);
        }
    }
}