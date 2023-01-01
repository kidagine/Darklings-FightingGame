using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace STUN.Attributes
{
    public class STUNUtils
    {
        public static byte[] Receive(Socket socket, int timeout)
        {
            if (!socket.Poll(timeout * 1000, SelectMode.SelectRead))
            {
                return null;
            }

            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] buffer = new byte[1024 * 2];
            int bytesRead = 0;

            bytesRead = socket.ReceiveFrom(buffer, ref endPoint);

            return buffer.Take(bytesRead).ToArray();
        }

        public static bool TryParseHostAndPort(string hostAndPort, out IPEndPoint endPoint)
        {
            if (string.IsNullOrWhiteSpace(hostAndPort))
            {
                endPoint = null;
                return false;
            }

            var split = hostAndPort.Split(':');

            if (split.Length != 2)
            {
                endPoint = null;
                return false;
            }

            if (!ushort.TryParse(split[1], out ushort port))
            {
                endPoint = null;
                return false;
            }

            if (!IPAddress.TryParse(split[0], out IPAddress address))
            {
                try
                {
#if NETSTANDARD1_3
                    address = Dns.GetHostEntryAsync(split[0]).GetAwaiter().GetResult().AddressList.First();
#else
                    address = Dns.GetHostEntry(split[0]).AddressList.First();
#endif
                }
                catch
                {
                    endPoint = null;
                    return false;
                }
            }

            endPoint = new IPEndPoint(address, port);
            return true;
        }

        public static bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            if (b1 == b2)
                return true;

            if (b1.Length != b2.Length)
                return false;

            return b1.SequenceEqual(b2);
        }
    }
}