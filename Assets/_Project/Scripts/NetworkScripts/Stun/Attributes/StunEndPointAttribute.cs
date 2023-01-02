using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace STUN.Attributes
{
    public class STUNEndPointAttribute : STUNAttribute
    {
        public IPEndPoint EndPoint { get; set; }

        public override void Parse(STUNBinaryReader binary, int length)
        {
            binary.BaseStream.Position++;
            var ipFamily = binary.ReadByte();
            var port = binary.ReadUInt16();
            IPAddress address;

            if (ipFamily == 1)
            {
                address = new IPAddress(binary.ReadBytes(4));
            }
            else if (ipFamily == 2)
            {
                address = new IPAddress(binary.ReadBytes(16));
            }
            else
            {
                throw new Exception("Unsupported IP Family " + ipFamily.ToString());
            }

            EndPoint = new IPEndPoint(address, port);
        }

        public override void WriteBody(STUNBinaryWriter binary)
        {
            binary.Write((byte)0);

            if (EndPoint.AddressFamily == AddressFamily.InterNetwork)
            {
                binary.Write((byte)1);
            }
            else if (EndPoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                binary.Write((byte)2);
            }
            else
            {
                throw new Exception("Unsupported IP Family" + EndPoint.AddressFamily.ToString());
            }

            binary.Write((ushort)EndPoint.Port);

            var addressBytes = EndPoint.Address.GetAddressBytes();
            binary.Write(addressBytes);
        }
    }
}
