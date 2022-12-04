using System.Diagnostics;
using Unity.Collections;
using UnityGGPO;

namespace SharedGame {

    public class RollbackRunner : IGameRunner {
        private NativeArray<byte> buffer = new NativeArray<byte>();

        private NativeArray<byte>[] buffers = new NativeArray<byte>[8];
        private long[][] inputss = new long[8][];

        private int targetFrame = 0;

        public IGame Game { get; private set; }

        public GameInfo GameInfo { get; private set; }

        private Stopwatch frameWatch = new Stopwatch();
        private Stopwatch idleWatch = new Stopwatch();

        public void Idle(int ms) {
            idleWatch.Start();
            Utils.Sleep(ms);
            idleWatch.Stop();
        }

        public void RunFrame() {
            ++targetFrame;
            frameWatch.Start();
            var inputs = new long[GameInfo.players.Length];
            for (int i = 0; i < inputs.Length; ++i) {
                inputs[i] = Game.ReadInputs(GameInfo.players[i].controllerId);
            }
            Game.Update(inputs, 0);
            frameWatch.Stop();
        }

        public void OnTestSave() {
            if (buffer.IsCreated) {
                buffer.Dispose();
            }
            buffer = Game.ToBytes();
        }

        public void OnTestLoad() {
            Game.FromBytes(buffer);
        }

        private void Rotate() {
            buffers[0].Dispose();
            for (int i = 1; i < buffers.Length; ++i) {
                buffers[i - 1] = buffers[i];
            }
            buffers[buffers.Length - 1] = default;
        }

        private void Rollback(int frames) {
            int cf = targetFrame;
            int f = targetFrame - frames;

            for (int i = 0; i < frames; ++i) {
                var inputs = this.inputss[i];
                Game.FromBytes(buffers[i]);
                Game.Update(inputs, 0);
            }
        }

        public RollbackRunner(IGame game) {
            Game = game;
            GameInfo = new GameInfo();
            int handle = 1;
            int controllerId = 0;
            GameInfo.players = new PlayerConnectionInfo[2];
            GameInfo.players[0] = new PlayerConnectionInfo {
                handle = handle,
                type = GGPOPlayerType.GGPO_PLAYERTYPE_LOCAL,
                connect_progress = 100,
                controllerId = controllerId
            };
            GameInfo.SetConnectState(handle, PlayerConnectState.Connecting);
            ++handle;
            ++controllerId;
            GameInfo.players[1] = new PlayerConnectionInfo {
                handle = handle,
                type = GGPOPlayerType.GGPO_PLAYERTYPE_LOCAL,
                connect_progress = 100,
                controllerId = controllerId++
            };
            GameInfo.SetConnectState(handle, PlayerConnectState.Connecting);
        }

        public StatusInfo GetStatus(Stopwatch updateWatch) {
            var status = new StatusInfo();
            status.idlePerc = (float)idleWatch.ElapsedMilliseconds / (float)updateWatch.ElapsedMilliseconds;
            status.updatePerc = (float)frameWatch.ElapsedMilliseconds / (float)updateWatch.ElapsedMilliseconds;
            status.periodic = GameInfo.periodic;
            status.now = GameInfo.now;
            return status;
        }

        public void DisconnectPlayer(int player) {
        }

        public void Shutdown() {
            if (buffer.IsCreated) {
                buffer.Dispose();
            }
        }
    }

    public class LocalRunner : IGameRunner {
        private NativeArray<byte> buffer;

        public IGame Game { get; private set; }

        public GameInfo GameInfo { get; private set; }

        private Stopwatch frameWatch = new Stopwatch();
        private Stopwatch idleWatch = new Stopwatch();

        public void Idle(int ms) {
            idleWatch.Start();
            Utils.Sleep(ms);
            idleWatch.Stop();
        }

        public void RunFrame() {
            frameWatch.Start();
            var inputs = new long[GameInfo.players.Length];
            for (int i = 0; i < inputs.Length; ++i) {
                inputs[i] = Game.ReadInputs(GameInfo.players[i].controllerId);
            }
            Game.Update(inputs, 0);
            frameWatch.Stop();
        }

        public void OnTestSave() {
            if (buffer.IsCreated) {
                buffer.Dispose();
            }
            buffer = Game.ToBytes();
        }

        public void OnTestLoad() {
            Game.FromBytes(buffer);
        }

        public LocalRunner(IGame game) {
            Game = game;
            GameInfo = new GameInfo();
            int handle = 1;
            int controllerId = 0;
            GameInfo.players = new PlayerConnectionInfo[2];
            GameInfo.players[0] = new PlayerConnectionInfo {
                handle = handle,
                type = GGPOPlayerType.GGPO_PLAYERTYPE_LOCAL,
                connect_progress = 100,
                controllerId = controllerId
            };
            GameInfo.SetConnectState(handle, PlayerConnectState.Connecting);
            ++handle;
            ++controllerId;
            GameInfo.players[1] = new PlayerConnectionInfo {
                handle = handle,
                type = GGPOPlayerType.GGPO_PLAYERTYPE_LOCAL,
                connect_progress = 100,
                controllerId = controllerId++
            };
            GameInfo.SetConnectState(handle, PlayerConnectState.Connecting);
        }

        public StatusInfo GetStatus(Stopwatch updateWatch) {
            var status = new StatusInfo();
            status.idlePerc = (float)idleWatch.ElapsedMilliseconds / (float)updateWatch.ElapsedMilliseconds;
            status.updatePerc = (float)frameWatch.ElapsedMilliseconds / (float)updateWatch.ElapsedMilliseconds;
            status.periodic = GameInfo.periodic;
            status.now = GameInfo.now;
            return status;
        }

        public void DisconnectPlayer(int player) {
        }

        public void Shutdown() {
            if (buffer.IsCreated) {
                buffer.Dispose();
            }
        }
    }
}