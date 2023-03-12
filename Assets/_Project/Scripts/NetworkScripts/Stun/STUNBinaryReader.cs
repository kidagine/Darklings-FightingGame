using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace STUN
{
    public class STUNBinaryReader : BinaryReader
    {
        public STUNBinaryReader(Stream stream) : base(stream)
        {

        }

        public override short ReadInt16()
        {
            return BitConverter.ToInt16(ReadNetworkBytes(sizeof(short)), 0);
        }

        public override ushort ReadUInt16()
        {
            return BitConverter.ToUInt16(ReadNetworkBytes(sizeof(ushort)), 0);
        }

        public override int ReadInt32()
        {
            return BitConverter.ToInt32(ReadNetworkBytes(sizeof(int)), 0);
        }

        public override uint ReadUInt32()
        {
            return BitConverter.ToUInt32(ReadNetworkBytes(sizeof(uint)), 0);
        }

        public override long ReadInt64()
        {
            return BitConverter.ToInt64(ReadNetworkBytes(sizeof(long)), 0);
        }

        public override ulong ReadUInt64()
        {
            return BitConverter.ToUInt64(ReadNetworkBytes(sizeof(ulong)), 0);
        }

        public byte[] ReadNetworkBytes(int count)
        {
            var bytes = base.ReadBytes(count);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }
    }
}
