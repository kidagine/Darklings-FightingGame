using System.Diagnostics;
using Unity.Collections;

namespace SharedGame {

    public class Connections {
        public ushort port;
        public string ip;
        public bool spectator;
    }

    public interface IGame {
        int Framenumber { get; }
        int Checksum { get; }

        void Update(long[] inputs, int disconnectFlags);

        void FromBytes(NativeArray<byte> data);

        NativeArray<byte> ToBytes();

        long ReadInputs(int controllerId);

        void LogInfo(string filename);

        void FreeBytes(NativeArray<byte> data);
    }

    public struct StatusInfo {
        public float idlePerc;
        public float updatePerc;
        public ChecksumInfo now;
        public ChecksumInfo periodic;

        public string TimePercString() {
            var otherPerc = 1f - (idlePerc + updatePerc);
            return string.Format("idle:{0:.00} update{1:.00} other{2:.00}", idlePerc, updatePerc, otherPerc);
        }

        public string ChecksumString() {
            return "periodic: " + RenderChecksum(periodic) + " now:" + RenderChecksum(now);
        }

        private string RenderChecksum(ChecksumInfo info) {
            return string.Format("f:{0} c:{1}", info.framenumber, info.checksum); // %04d  %08x
        }
    }

    public interface IGameRunner {
        IGame Game { get; }
        GameInfo GameInfo { get; }

        void Idle(int ms);

        void RunFrame();

        StatusInfo GetStatus(Stopwatch updateWatch);

        void DisconnectPlayer(int player);

        void Shutdown();
    }

    public interface IGameView {

        void UpdateGameView(IGameRunner runner);
    }
}