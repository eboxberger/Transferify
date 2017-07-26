using System.Net;
using Transferify.Interfaces;

namespace Transferify.DataTypes.Interfaces
{
    internal interface IIpAddress : ITransferValueT<IPAddress>, IKnowMySize
    {
    }
}