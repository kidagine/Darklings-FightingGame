namespace STUN.Attributes
{
    public class STUNXorMappedAddressAttribute : STUNEndPointAttribute
    {
        public override string ToString()
        {
            return string.Format("XOR-MAPPED-ADDRESS {0}", EndPoint);
        }
    }
}