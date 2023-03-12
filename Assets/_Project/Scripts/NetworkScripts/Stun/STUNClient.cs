using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using STUN.Attributes;
using System.IO;

namespace STUN
{
    /// <summary>
    /// Implements a RFC3489 STUN client.
    /// </summary>
    public static class STUNClient
    {
        /// <summary>
        /// Period of time in miliseconds to wait for server response.
        /// </summary>
        public static int ReceiveTimeout = 5000;

        /// <param name="server">Server address</param>
        /// <param name="queryType">Query type</param>
        /// <param name="closeSocket">
        /// Set to true if created socket should closed after the query
        /// else <see cref="STUNQueryResult.Socket"/> will leave open and can be used.
        /// </param>
        public static Task<STUNQueryResult> QueryAsync(IPEndPoint server, STUNQueryType queryType, bool closeSocket)
        {
            return Task.Run(() => Query(server, queryType, closeSocket));
        }

        /// <param name="socket">A UDP <see cref="Socket"/> that will use for query. You can also use <see cref="UdpClient.Client"/></param>
        /// <param name="server">Server address</param>
        /// <param name="queryType">Query type</param>
        public static Task<STUNQueryResult> QueryAsync(Socket socket, IPEndPoint server, STUNQueryType queryType,
            NATTypeDetectionRFC natTypeDetectionRfc)
        {
            return Task.Run(() => Query(socket, server, queryType, natTypeDetectionRfc));
        }

        /// <param name="server">Server address</param>
        /// <param name="queryType">Query type</param>
        /// <param name="closeSocket">
        /// Set to true if created socket should closed after the query
        /// else <see cref="STUNQueryResult.Socket"/> will leave open and can be used.
        /// </param>
        public static STUNQueryResult Query(IPEndPoint server, STUNQueryType queryType, bool closeSocket,
            NATTypeDetectionRFC natTypeDetectionRfc = NATTypeDetectionRFC.Rfc3489)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint bindEndPoint = new IPEndPoint(IPAddress.Any, 0);
            socket.Bind(bindEndPoint);

            var result = Query(socket, server, queryType, natTypeDetectionRfc);

            if (closeSocket)
            {
                socket.Dispose();
                result.Socket = null;
            }

            return result;
        }

        /// <param name="socket">A UDP <see cref="Socket"/> that will use for query. You can also use <see cref="UdpClient.Client"/></param>
        /// <param name="server">Server address</param>
        /// <param name="queryType">Query type</param>
        /// <param name="natTypeDetectionRfc">Rfc algorithm type</param>
        public static STUNQueryResult Query(Socket socket, IPEndPoint server, STUNQueryType queryType,
            NATTypeDetectionRFC natTypeDetectionRfc)
        {
            if (natTypeDetectionRfc == NATTypeDetectionRFC.Rfc3489)
            {
                return STUNRfc3489.Query(socket, server, queryType, ReceiveTimeout);
            }

            if (natTypeDetectionRfc == NATTypeDetectionRFC.Rfc5780)
            {
                return STUNRfc5780.Query(socket, server, queryType, ReceiveTimeout);
            }

            return new STUNQueryResult();
        }
    }
}