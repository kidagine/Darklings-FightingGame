namespace STUN.Attributes
{
    public class STUNOtherAddressAttribute : STUNEndPointAttribute
    {
        public override string ToString()
        {
            return string.Format("OTHER-ADDRESS {0}", EndPoint);
        }
    }
}