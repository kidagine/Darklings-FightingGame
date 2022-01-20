using System;
using Unity.Collections;
using Unity.Netcode;
public struct OnlinePlayerInfo: INetworkSerializable, IEquatable<OnlinePlayerInfo>
{
    public ulong ClientId;
    public FixedString32Bytes PlayerName;
    public FixedString32Bytes IsReady;
    public int Assist;
    public int Portrait;

    public OnlinePlayerInfo(ulong clientId, FixedString32Bytes name, FixedString32Bytes isReady, int assist, int portrait)
    {
        ClientId = clientId;
        PlayerName = name;
        IsReady = isReady;
        Assist = assist;
        Portrait = portrait;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref PlayerName);
        serializer.SerializeValue(ref IsReady);
        serializer.SerializeValue(ref Assist);
        serializer.SerializeValue(ref Portrait);
    }

    public bool Equals(OnlinePlayerInfo other)
    {
        return ClientId == other.ClientId &&
            PlayerName == other.PlayerName &&
            IsReady == other.IsReady && Portrait == other.Portrait && Assist == other.Assist;
    }
}
