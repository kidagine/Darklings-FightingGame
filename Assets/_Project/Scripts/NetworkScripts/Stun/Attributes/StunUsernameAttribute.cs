using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN.Attributes
{
    public class STUNUsernameAttribute : STUNAsciiTextAttribute
    {
        public override string ToString()
        {
            return string.Format("USERNAME \"{0}\"", Text);
        }
    }
}
