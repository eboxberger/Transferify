namespace Transferify.Interfaces
{
    public interface IReadable : ITransferItem
    {
        void Read(IDataBuffer dataBuffer, int count = -1);
    }
}