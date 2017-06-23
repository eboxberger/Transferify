namespace Transferify.Interfaces
{
    public interface ITransferValueT<TValue> : IReadableWritable
    {
        TValue Value { get; set; }
    }
}