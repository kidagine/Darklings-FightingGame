using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityGGPO;

namespace SharedGame
{

    public abstract class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public bool useNewUpdate = true;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        }

        public event Action<StatusInfo> OnStatus;

        public event Action<bool> OnRunningChanged;

        public event Action OnInit;

        public event Action OnStateChanged;

        public Stopwatch updateWatch = new Stopwatch();

        public bool IsRunning { get; private set; }

        public IGameRunner Runner { get; private set; }

        private int start;
        private int next;

        public void DisconnectPlayer(int player)
        {
            if (Runner != null)
            {
                Runner.DisconnectPlayer(player);
            }
        }

        public void Shutdown()
        {
            if (Runner != null)
            {
                Runner.Shutdown();
                Runner = null;
            }
        }

        private void OnDestroy()
        {
            Shutdown();
            _instance = null;
        }

        protected virtual void OnPreRunFrame()
        {
        }

        private void Start()
        {
            var t = Time.realtimeSinceStartup;
            var t2 = t + 1f / 60f;
            System.Threading.Thread.Sleep(SToMs(t2 - t));
        }

        private int SToMs(float time)
        {
            return (int)(time * 1000f);
        }

        private void Update()
        {
            if (IsRunning != (Runner != null))
            {
                IsRunning = Runner != null;
                OnRunningChanged?.Invoke(IsRunning);
                if (IsRunning)
                {
                    OnInit?.Invoke();
                    start = next = Utils.TimeGetTime();
                }
            }
            if (IsRunning)
            {
                updateWatch.Start();
                if (useNewUpdate)
                {
                    NewUpdate();
                }
                else
                {
                    OriginalUpdate();
                }
                updateWatch.Stop();

                OnStatus?.Invoke(Runner.GetStatus(updateWatch));
            }
        }

        private void OriginalUpdate()
        {
            var now = Utils.TimeGetTime();
            var extraMs = Mathf.Max(0, next - now - 1);
            Runner.Idle(extraMs);
            if (now >= next)
            {
                OnPreRunFrame();
                Runner.RunFrame();
                next = now + (int)(1000f / 60f);
                OnStateChanged?.Invoke();
            }
        }

        private void NewUpdate()
        {
            var now = Utils.TimeGetTime();
            var extraMs = Mathf.Max(0, next - now - 1);
            Runner.Idle(extraMs);
            OnPreRunFrame();
            Runner.RunFrame();
            next = next + (int)(1000f / 60f);
            OnStateChanged?.Invoke();
        }

        public void StartGame(IGameRunner runner)
        {
            Runner = runner;
        }

        public abstract void StartLocalGame();

        public abstract void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex);
    }
}