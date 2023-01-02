using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace STUN
{
    public class STUNBinaryWriter : BinaryWriter
    {
        public STUNBinaryWriter(Stream stream) : base(stream)
        {

        }

        public override void Write(short value)
        {
            WriteNetworkBytes(BitConverter.GetBytes(value));
        }

        public override void Write(ushort value)
        {
            WriteNetworkBytes(BitConverter.GetBytes(value));
        }

        public override void Write(int value)
        {
            WriteNetworkBytes(BitConverter.GetBytes(value));
        }

        public override void Write(uint value)
        {
            WriteNetworkBytes(BitConverter.GetBytes(value));
        }

        public override void Write(long value)
        {
            WriteNetworkBytes(BitConverter.GetBytes(value));
        }

        public override void Write(ulong value)
        {
            WriteNetworkBytes(BitConverter.GetBytes(value));
        }

        private void WriteNetworkBytes(byte[] buffer)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(buffer);
            }

            base.Write(buffer);
        }
    }
}
