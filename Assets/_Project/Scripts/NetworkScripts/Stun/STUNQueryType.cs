namespace STUN
{
    public enum STUNQueryType
    {
        /// <summary>
        /// Indicates to client to just query IP address and return
        /// </summary>
        PublicIP,

        /// <summary>
        /// Indicates to client to stop the querying if NAT type is strict.
        /// If the NAT is strict the NAT type will set too <see cref="STUNNATType.Unspecified"/> 
        /// Else the NAT type will set to one of following types
        /// <see cref="STUNNATType.OpenInternet"/>
        /// <see cref="STUNNATType.SymmetricUDPFirewall"/>
        /// <see cref="STUNNATType.FullCone"/>
        /// </summary>
        OpenNAT,

        /// <summary>
        /// Indicates to client to find the exact NAT type.
        /// </summary>
        ExactNAT,
    }
}
