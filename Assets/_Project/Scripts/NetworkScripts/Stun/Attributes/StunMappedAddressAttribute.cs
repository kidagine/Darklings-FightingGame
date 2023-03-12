using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN.Attributes
{
    public class STUNMappedAddressAttribute : STUNEndPointAttribute
    {
        public override string ToString()
        {
            return string.Format("MAPPED-ADDRESS {0}", EndPoint);
        }
    }
}
