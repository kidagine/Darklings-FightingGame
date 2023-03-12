using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN.Attributes
{
    public class STUNErrorCodeAttribute : STUNAttribute
    {
        public STUNErrorCodes Error { get; set; }
        public string Phrase { get; set; }

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
