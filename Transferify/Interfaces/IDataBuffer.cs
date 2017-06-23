namespace Transferify.Interfaces
{
    public interface IDataBuffer
    {
        byte[] BufferCopy { get; }
        int Position { get; set; }
        byte[] Read(int count);
        void Write(byte[] values);
        void Clear();
        int Capacity { get; }
    }
}