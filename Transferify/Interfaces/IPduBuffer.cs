namespace Transferify.Interfaces
{
    public interface IPduBuffer : IDataBuffer
    {
        byte[] PopPdu();
        void PopPdu(IReadable readable);
        void PushPdu(byte[] data);
        void PushPdu(IWritable writable);
    }
}