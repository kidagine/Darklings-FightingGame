using UnityGGPO;

namespace SharedGame {

    public enum PlayerConnectState {
        Connecting = 0,
        Synchronizing,
        Running,
        Disconnected,
        Disconnecting,
    };

    public struct PlayerConnectionInfo {
        public GGPOPlayerType type;
        public int handle;
        public PlayerConnectState state;
        public int connect_progress;
        public int disconnect_timeout;
        public int disconnect_start;
        public int controllerId;
    };

    public struct ChecksumInfo {
        public int framenumber;
        public int checksum;

        public override string ToString() => framenumber.ToString() + " " + checksum.ToString("X2");
    };

    public class GameInfo {
        public PlayerConnectionInfo[] players;
        public string status;

        public ChecksumInfo now;
        public ChecksumInfo periodic;

        public void SetConnectState(int handle, PlayerConnectState state) {
            for (int i = 0; i < players.Length; i++) {
                if (players[i].handle == handle) {
                    players[i].connect_progress = 0;
                    players[i].state = state;
                    break;
                }
            }
        }

        public void SetDisconnectTimeout(int handle, int now, int timeout) {
            for (int i = 0; i < players.Length; i++) {
                if (players[i].handle == handle) {
                    players[i].disconnect_start = now;
                    players[i].disconnect_timeout = timeout;
                    players[i].state = PlayerConnectState.Disconnecting;
                    break;
                }
            }
        }

        public void SetConnectState(PlayerConnectState state) {
            for (int i = 0; i < players.Length; i++) {
                players[i].state = state;
            }
        }

        public void UpdateConnectProgress(int handle, int progress) {
            for (int i = 0; i < players.Length; i++) {
                if (players[i].handle == handle) {
                    players[i].connect_progress = progress;
                    break;
                }
            }
        }
    }
}