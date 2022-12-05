using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;

namespace UnityGGPO {

    // @TODO Move
    public interface IPerfUpdate {

        void ggpoutil_perfmon_update(GGPONetworkStats[] statss);
    }

    public static partial class GGPO {

        public static class Session {
            // Pass throughs

            public delegate bool OnEventConnectedToPeerDelegate(int connected_player);

            public delegate bool OnEventSynchronizingWithPeerDelegate(int synchronizing_player, int synchronizing_count, int synchronizing_total);

            public delegate bool OnEventSynchronizedWithPeerDelegate(int synchronized_player);

            public delegate bool OnEventRunningDelegate();

            public delegate bool OnEventConnectionInterruptedDelegate(int connection_interrupted_player, int connection_interrupted_disconnect_timeout);

            public delegate bool OnEventConnectionResumedDelegate(int connection_resumed_player);

            public delegate bool OnEventDisconnectedFromPeerDelegate(int disconnected_player);

            public delegate bool OnEventEventcodeTimesyncDelegate(int timesync_frames_ahead);

            public delegate bool SafeLoadGameStateDelegate(NativeArray<byte> data);

            public delegate bool SafeLogGameStateDelegate(string filename, NativeArray<byte> data);

            public delegate bool SafeSaveGameStateDelegate(out NativeArray<byte> data, out int checksum, int frame);

            public delegate void SafeFreeBufferDelegate(NativeArray<byte> data);

            private static IntPtr ggpo;
            private static readonly Dictionary<long, NativeArray<byte>> cache = new Dictionary<long, NativeArray<byte>>();

            private static BeginGameDelegate beginGameCallback;
            private static AdvanceFrameDelegate advanceFrameCallback;

            private static SafeLoadGameStateDelegate loadGameStateCallback;
            private static SafeLogGameStateDelegate logGameStateCallback;
            private static SafeSaveGameStateDelegate saveGameStateCallback;
            private static SafeFreeBufferDelegate freeBufferCallback;

            private static OnEventConnectedToPeerDelegate onEventConnectedToPeer;
            private static OnEventSynchronizingWithPeerDelegate onEventSynchronizingWithPeer;
            private static OnEventSynchronizedWithPeerDelegate onEventSynchronizedWithPeer;
            private static OnEventRunningDelegate onEventRunning;
            private static OnEventConnectionInterruptedDelegate onEventConnectionInterrupted;
            private static OnEventConnectionResumedDelegate onEventConnectionResumed;
            private static OnEventDisconnectedFromPeerDelegate onEventDisconnectedFromPeer;
            private static OnEventEventcodeTimesyncDelegate onEventTimesync;

            private static IntPtr _beginGameCallback;
            private static IntPtr _advanceFrameCallback;
            private static IntPtr _loadGameStateCallback;
            private static IntPtr _logGameStateCallback;
            private static IntPtr _saveGameStateCallback;
            private static IntPtr _freeBufferCallback;
            private static IntPtr _onEventCallback;

            public static void Init(LogDelegate log) {
                GGPO.SetLogDelegate(log);
            }

            public static bool IsStarted() {
                return ggpo != IntPtr.Zero;
            }

            public static int StartSession(
                    BeginGameDelegate beginGame,
                    AdvanceFrameDelegate advanceFrame,
                    SafeLoadGameStateDelegate loadGameState,
                    SafeLogGameStateDelegate logGameState,
                    SafeSaveGameStateDelegate saveGameState,
                    SafeFreeBufferDelegate freeBuffer,
                    OnEventConnectedToPeerDelegate onEventConnectedToPeer,
                    OnEventSynchronizingWithPeerDelegate onEventSynchronizingWithPeer,
                    OnEventSynchronizedWithPeerDelegate onEventSynchronizedWithPeer,
                    OnEventRunningDelegate onEventRunning,
                    OnEventConnectionInterruptedDelegate onEventConnectionInterrupted,
                    OnEventConnectionResumedDelegate onEventConnectionResumed,
                    OnEventDisconnectedFromPeerDelegate onEventDisconnectedFromPeer,
                    OnEventEventcodeTimesyncDelegate onEventTimesync,
                    string gameName, int numPlayers, int localport) {
                beginGameCallback = beginGame;
                advanceFrameCallback = advanceFrame;
                loadGameStateCallback = loadGameState;
                logGameStateCallback = logGameState;
                saveGameStateCallback = saveGameState;
                freeBufferCallback = freeBuffer;

                Session.onEventConnectedToPeer = onEventConnectedToPeer;
                Session.onEventSynchronizingWithPeer = onEventSynchronizingWithPeer;
                Session.onEventSynchronizedWithPeer = onEventSynchronizedWithPeer;
                Session.onEventRunning = onEventRunning;
                Session.onEventConnectionInterrupted = onEventConnectionInterrupted;
                Session.onEventConnectionResumed = onEventConnectionResumed;
                Session.onEventDisconnectedFromPeer = onEventDisconnectedFromPeer;
                Session.onEventTimesync = onEventTimesync;

                unsafe {
                    _beginGameCallback = Marshal.GetFunctionPointerForDelegate<BeginGameDelegate>(OnBeginGame);
                    _advanceFrameCallback = Marshal.GetFunctionPointerForDelegate<AdvanceFrameDelegate>(OnAdvanceFrame);
                    _loadGameStateCallback = Marshal.GetFunctionPointerForDelegate<LoadGameStateDelegate>(LoadGameState);
                    _logGameStateCallback = Marshal.GetFunctionPointerForDelegate<LogGameStateDelegate>(LogGameState);
                    _saveGameStateCallback = Marshal.GetFunctionPointerForDelegate<SaveGameStateDelegate>(SaveGameState);
                    _freeBufferCallback = Marshal.GetFunctionPointerForDelegate<FreeBufferDelegate>(FreeBuffer);
                    _onEventCallback = Marshal.GetFunctionPointerForDelegate<OnEventDelegate>(OnEvent);
                }
                var result = GGPO.StartSession(out ggpo,
                    _beginGameCallback,
                    _advanceFrameCallback,
                    _loadGameStateCallback,
                    _logGameStateCallback,
                    _saveGameStateCallback,
                    _freeBufferCallback,
                    _onEventCallback,
                    gameName, numPlayers, localport);

                return result;
            }

            public static int StartSpectating(
                    BeginGameDelegate beginGame,
                    AdvanceFrameDelegate advanceFrame,
                    SafeLoadGameStateDelegate loadGameState,
                    SafeLogGameStateDelegate logGameState,
                    SafeSaveGameStateDelegate saveGameState,
                    SafeFreeBufferDelegate freeBuffer,
                    OnEventConnectedToPeerDelegate onEventConnectedToPeer,
                    OnEventSynchronizingWithPeerDelegate onEventSynchronizingWithPeer,
                    OnEventSynchronizedWithPeerDelegate onEventSynchronizedWithPeer,
                    OnEventRunningDelegate onEventRunning,
                    OnEventConnectionInterruptedDelegate onEventConnectionInterrupted,
                    OnEventConnectionResumedDelegate onEventConnectionResumed,
                    OnEventDisconnectedFromPeerDelegate onEventDisconnectedFromPeer,
                    OnEventEventcodeTimesyncDelegate onEventTimesync,
                    string gameName, int numPlayers, int localport, string hostIp, int hostPort) {
                beginGameCallback = beginGame;
                advanceFrameCallback = advanceFrame;
                loadGameStateCallback = loadGameState;
                logGameStateCallback = logGameState;
                saveGameStateCallback = saveGameState;
                freeBufferCallback = freeBuffer;

                Session.onEventConnectedToPeer = onEventConnectedToPeer;
                Session.onEventSynchronizingWithPeer = onEventSynchronizingWithPeer;
                Session.onEventSynchronizedWithPeer = onEventSynchronizedWithPeer;
                Session.onEventRunning = onEventRunning;
                Session.onEventConnectionInterrupted = onEventConnectionInterrupted;
                Session.onEventConnectionResumed = onEventConnectionResumed;
                Session.onEventDisconnectedFromPeer = onEventDisconnectedFromPeer;
                Session.onEventTimesync = onEventTimesync;

                unsafe {
                    _beginGameCallback = Marshal.GetFunctionPointerForDelegate<BeginGameDelegate>(OnBeginGame);
                    _advanceFrameCallback = Marshal.GetFunctionPointerForDelegate<AdvanceFrameDelegate>(OnAdvanceFrame);
                    _loadGameStateCallback = Marshal.GetFunctionPointerForDelegate<LoadGameStateDelegate>(LoadGameState);
                    _logGameStateCallback = Marshal.GetFunctionPointerForDelegate<LogGameStateDelegate>(LogGameState);
                    _saveGameStateCallback = Marshal.GetFunctionPointerForDelegate<SaveGameStateDelegate>(SaveGameState);
                    _freeBufferCallback = Marshal.GetFunctionPointerForDelegate<FreeBufferDelegate>(FreeBuffer);
                    _onEventCallback = Marshal.GetFunctionPointerForDelegate<OnEventDelegate>(OnEvent);
                }

                var result = GGPO.StartSpectating(out ggpo,
                    _beginGameCallback,
                    _advanceFrameCallback,
                    _loadGameStateCallback,
                    _logGameStateCallback,
                    _saveGameStateCallback,
                    _freeBufferCallback,
                    _onEventCallback,
                    gameName, numPlayers, localport, hostIp, hostPort);
                return result;
            }

            public static int GetNetworkStats(int phandle, out GGPONetworkStats stats) {
                stats = new GGPONetworkStats();
                var result = GGPO.GetNetworkStats(ggpo, phandle,
                    out stats.send_queue_len,
                    out stats.recv_queue_len,
                    out stats.ping,
                    out stats.kbps_sent,
                    out stats.local_frames_behind,
                    out stats.remote_frames_behind
                );
                return result;
            }

            public static int SetDisconnectNotifyStart(int timeout) {
                var result = GGPO.SetDisconnectNotifyStart(ggpo, timeout);
                return result;
            }

            public static int SetDisconnectTimeout(int timeout) {
                var result = GGPO.SetDisconnectTimeout(ggpo, timeout);
                return result;
            }

            public static long[] SynchronizeInput(int length, out int disconnect_flags) {
                return GGPO.SynchronizeInput(ggpo, length, out disconnect_flags);
            }

            public static int AddLocalInput(int local_player_handle, long inputs) {
                var result = GGPO.AddLocalInput(ggpo, local_player_handle, inputs);
                return result;
            }

            public static int CloseSession() {
                foreach (var data in cache.Values) {
                    freeBufferCallback(data);
                }
                cache.Clear();
                var result = GGPO.CloseSession(ggpo);
                ggpo = IntPtr.Zero;
                return result;
            }

            public static int Idle(int time) {
                var result = GGPO.Idle(ggpo, time);
                return result;
            }

            public static int AddPlayer(GGPOPlayer player, out int phandle) {
                var result = GGPO.AddPlayer(ggpo,
                    (int)player.type,
                    player.player_num,
                    player.ip_address,
                    player.port,
                    out phandle);
                return result;
            }

            public static int DisconnectPlayer(int phandle) {
                var result = GGPO.DisconnectPlayer(ggpo, phandle);
                return result;
            }

            public static int SetFrameDelay(int phandle, int frame_delay) {
                var result = GGPO.SetFrameDelay(ggpo, phandle, frame_delay);
                return result;
            }

            public static int AdvanceFrame() {
                var result = GGPO.AdvanceFrame(ggpo);
                return result;
            }

            public static void Log(string v) {
                GGPO.Log(ggpo, v);
            }

            public static void OnDispose() {
                if (ggpo != IntPtr.Zero) {
                    CloseSession();
                }
            }

            // Callbacks

            private static unsafe void FreeBuffer(void* dataPtr) {
                if (cache.TryGetValue((long)dataPtr, out var data)) {
                    freeBufferCallback(data);
                    cache.Remove((long)dataPtr);
                }
            }

            private static unsafe bool SaveGameState(void** buffer, int* outLen, int* outChecksum, int frame) {
                var result = saveGameStateCallback(out var data, out int checksum, frame);
                var ptr = Utils.ToPtr(data);
                cache[(long)ptr] = data;

                *buffer = ptr;
                *outLen = data.Length;
                *outChecksum = checksum;
                return result;
            }

            private static unsafe bool LogGameState(string filename, void* buffer, int length) {
                return logGameStateCallback(filename, Utils.ToArray(buffer, length));
            }

            private static unsafe bool LoadGameState(void* buffer, int length) {
                return loadGameStateCallback(Utils.ToArray(buffer, length));
            }

            private static bool OnAdvanceFrame(int flags) {
                return advanceFrameCallback.Invoke(flags);
            }

            private static bool OnBeginGame(string name) {
                return beginGameCallback.Invoke(name);
            }

            private static bool OnEvent(IntPtr evtPtr) {
                /*
                code = data[0];
                connected.player = data[1];
                synchronizing.player = data[1];
                synchronizing.count = data[2];
                synchronizing.total = data[3];
                synchronized.player = data[1];
                disconnected.player = data[1]
                timesync.frames_ahead = data[1];
                connection_interrupted.player = data[1];
                connection_interrupted.disconnect_timeout = data[2];
                connection_resumed.player = data[1];
                */

                int[] data = new int[4];
                Marshal.Copy(evtPtr, data, 0, 4);
                switch (data[0]) {
                    case EVENTCODE_CONNECTED_TO_PEER:
                        return onEventConnectedToPeer(data[1]);

                    case EVENTCODE_SYNCHRONIZING_WITH_PEER:
                        return onEventSynchronizingWithPeer(data[1], data[2], data[3]);

                    case EVENTCODE_SYNCHRONIZED_WITH_PEER:
                        return onEventSynchronizedWithPeer(data[1]);

                    case EVENTCODE_RUNNING:
                        return onEventRunning();

                    case EVENTCODE_DISCONNECTED_FROM_PEER:
                        return onEventDisconnectedFromPeer(data[1]);

                    case EVENTCODE_TIMESYNC:
                        return onEventTimesync(data[1]);

                    case EVENTCODE_CONNECTION_INTERRUPTED:
                        return onEventConnectionInterrupted(data[1], data[2]);

                    case EVENTCODE_CONNECTION_RESUMED:
                        return onEventConnectionResumed(data[1]);
                }
                return false;
            }
        }
    }
}