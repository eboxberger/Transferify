using Transferify.DataTypes.Interfaces;

namespace Transferify.DataTypes
{
    public class OctetString : String, IOctetString
    {
        public OctetString(string value = "") 
            : base(value)
        {
        }
    }
}