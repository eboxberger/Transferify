using System;
using Transferify.Interfaces;

namespace Transferify.DataTypes.Interfaces
{
    public interface IUuid : ITransferValueT<Guid>, IKnowMySize
    {
    }
}