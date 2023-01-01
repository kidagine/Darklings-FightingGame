using System.Net;
using System.Net.Sockets;

namespace STUN
{
    /// <summary>
    /// STUN client query result
    /// </summary>
    public class STUNQueryResult
    {
        /// <summary>
        /// The query type that passed to method
        /// </summary>
        public STUNQueryType QueryType { get; set; }

        /// <summary>
        /// The query result error
        /// </summary>
        public STUNQueryError QueryError { get; set; }

        /// <summary>
        /// Contains the server error code that receive from server.
        /// Presents if <see cref="QueryError"/> set too <see cref="STUNQueryError.ServerError"/>
        /// </summary>
        public STUNErrorCodes ServerError { get; set; }

        /// <summary>
        /// Contains the server error phrase that receive from server.
        /// Presents if <see cref="QueryError"/> set to <see cref="STUNQueryError.ServerError"/>
        /// </summary>
        public string ServerErrorPhrase { get; set; }

        /// <summary>
        /// The socket that used to communicate with STUN server
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// Contains the server address
        /// </summary>
        public IPEndPoint ServerEndPoint { get; set; }

        /// <summary>
        /// Contains the queried NAT Type.
        /// Presents if <see cref="QueryError"/> set to <see cref="STUNQueryError.Success"/>
        /// </summary>
        public STUNNATType NATType { get; set; }

        /// <summary>
        /// Contains the public endpoint that queried from server.
        /// </summary>
        public IPEndPoint PublicEndPoint { get; set; }

        /// <summary>
        /// Contains client's socket local endpoiont.
        /// </summary>
        public IPEndPoint LocalEndPoint { get; set; }
    }
}
