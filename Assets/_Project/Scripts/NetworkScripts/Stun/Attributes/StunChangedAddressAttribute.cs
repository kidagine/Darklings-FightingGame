using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN.Attributes
{
    public class STUNChangedAddressAttribute : STUNEndPointAttribute
    {
        public override string ToString()
        {
            return string.Format("CHANGE(D)-ADDRESS {0}", EndPoint);
        }
    }
}
