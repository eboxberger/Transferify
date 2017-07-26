using Transferify.DataTypes.Interfaces;

namespace Transferify.DataTypes
{
    public class VisibleString : String, IVisibleString
    {
        public VisibleString(string value = "")
            : base(value)
        {
        }
    }
}