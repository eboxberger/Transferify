using Transferify.Interfaces;

namespace Transferify.Extensions
{
    public static class TransferValueExtensions
    {
        public static DelayedLengthWriter WriteLengthDelayed(this ITransferValue transferValue, IDataBuffer dataBuffer)
        {
            return new DelayedLengthWriter(dataBuffer, transferValue);
        }
    }
}