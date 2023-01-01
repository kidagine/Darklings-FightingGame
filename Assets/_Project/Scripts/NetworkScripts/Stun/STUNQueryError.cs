namespace STUN
{
    public enum STUNQueryError
    {
        /// <summary>
        /// Indicates querying was successful.
        /// </summary>
        Success,

        /// <summary>
        /// Indicates the server responsed with error.
        /// In this case you have check <see cref="STUNQueryResult.ServerError"/> and <see cref="STUNQueryResult.ServerErrorPhrase"/> in query result.
        /// </summary>
        ServerError,

        /// <summary>
        /// Indicates the server responsed a bad\wrong\.. message. This error will returned in many cases.  
        /// </summary>
        BadResponse,

        /// <summary>
        /// Indicates the server responsed a message that contains a different transcation ID 
        /// </summary>
        BadTransactionID,

        /// <summary>
        /// Indicates the server didn't response a request within a time interval
        /// </summary>
        Timedout,
        /// <summary>
        /// Indicates the server did not support nat detection
        /// </summary>
        NotSupported
    }
}
