using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN.Attributes
{
    public class STUNAsciiTextAttribute : STUNAttribute
    {
        public string Text { get; set; }

        public override void Parse(STUNBinaryReader binary, int length)
        {
            Text = Encoding.ASCII.GetString(binary.ReadBytes(length));
        }

        public override void WriteBody(STUNBinaryWriter binary)
        {
            binary.Write(Encoding.ASCII.GetBytes(Text));
        }
    }
}
