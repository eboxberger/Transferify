namespace Transferify.Interfaces
{
    public interface IWritable : ITransferItem
    {
        void Write(IDataBuffer dataBuffer, int count = -1);
    }
}