using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN.Attributes
{
    public class STUNChangeRequestAttribute : STUNAttribute
    {
        private static readonly byte[] Three0 = new byte[3]; 

        public bool ChangeIP { get; set; }
        public bool ChangePort { get; set; }

        public STUNChangeRequestAttribute()
        {

        }

        public STUNChangeRequestAttribute(bool ip, bool port)
        {
            ChangeIP = ip;
            ChangePort = port;
        }

        public override void Parse(STUNBinaryReader binary, int length)
        {
            binary.BaseStream.Position += 3;
            var b = binary.ReadByte();
            ChangeIP = ((b & 4) != 0);
            ChangePort = ((b & 2) != 0);
        }

        public override void WriteBody(STUNBinaryWriter binary)
        {
            binary.Write(Three0);

            int i = 0;
            if (ChangeIP) i |= 4;
            if (ChangePort) i |= 2;

            binary.Write((byte)i);
        }

        public override string ToString()
        {
            return string.Format("CHANGE-REQUEST IP:{0} PORT:{1}", ChangeIP, ChangePort);
        }
    }
}
