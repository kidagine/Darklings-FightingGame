using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN.Attributes
{
    public class STUNSourceAddressAttribute : STUNEndPointAttribute
    {
        public override string ToString()
        {
            return string.Format("SOURCE-ADDRESS {0}", EndPoint);
        }
    }
}
