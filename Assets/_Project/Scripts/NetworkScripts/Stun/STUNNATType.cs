namespace STUN
{
    public enum STUNNATType
    {
        /// <summary>
        /// Unspecified NAT Type
        /// </summary>
        Unspecified,

        /// <summary>
        /// Open internet. for example Virtual Private Servers.
        /// </summary>
        OpenInternet,

        /// <summary>
        /// Full Cone NAT. Good to go.
        /// </summary>
        FullCone,

        /// <summary>
        /// Restricted Cone NAT.
        /// It mean's client can only receive data only IP addresses that it sent a data before.
        /// </summary>
        Restricted,

        /// <summary>
        /// Port-Restricted Cone NAT.
        /// Same as <see cref="Restricted"/> but port is included too.
        /// </summary>
        PortRestricted,

        /// <summary>
        /// Symmetric NAT.
        /// It's means the client pick's a different port for every connection it made.
        /// </summary>
        Symmetric,

        /// <summary>
        /// Same as <see cref="OpenInternet"/> but only received data from addresses that it sent a data before.
        /// </summary>
        SymmetricUDPFirewall,
    }
}
