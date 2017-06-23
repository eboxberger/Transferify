namespace Transferify.Interfaces
{
    public interface ITransferValue : IReadableWritable, IKnowMySize
    {
        object Value { get; set; }
        void ConvertFromString(string value);
    }
}