using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN.Attributes
{
    public class STUNReflectedFromAttribute : STUNAttribute
    {
        public override void Parse(STUNBinaryReader binary, int length)
        {
            throw new NotImplementedException();
        }

        public override void WriteBody(STUNBinaryWriter binary)
        {
            throw new NotImplementedException();
        }
    }
}
