using Transferify.Interfaces;

namespace Transferify.DataTypes.Interfaces
{
    public interface IByteArray : ITransferValueT<byte[]>, IKnowMySize
    {
    }
}