namespace STUN.Attributes
{
    public class STUNResponseOriginAttribute : STUNEndPointAttribute
    {
        public override string ToString()
        {
            return string.Format("RESPONSE-ORIGIN {0}", EndPoint);
        }
    }
}