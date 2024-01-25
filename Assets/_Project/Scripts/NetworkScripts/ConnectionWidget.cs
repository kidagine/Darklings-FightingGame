using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using System;
using System.Net.Sockets;

namespace SharedGame
{

    public class ConnectionWidget : MonoBehaviour
    {
        public TMP_InputField[] inpIps;
        public Toggle[] tglSpectators;
        public TMP_InputField inpPlayerIndex;
        public Toggle tgLocal;
        public Button btnConnect;

        private GameManager gameManager => GameManager.Instance;

        private void Awake()
        {
            gameManager.OnRunningChanged += OnRunningChanged;
            btnConnect.onClick.AddListener(OnConnect);
            var connections = new List<Connections>();
            connections.Add(new Connections()
            {
                ip = PrivateIP(),
                port = 7000,
                spectator = false,
            });
            connections.Add(new Connections()
            {
                ip = PrivateIP(),
                port = 7001,
                spectator = false,
            });
            inpPlayerIndex.text = "0";
            LoadConnectionInfo(connections);
        }
        private string PrivateIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                tgLocal.isOn = !tgLocal.isOn;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (inpPlayerIndex.text == "0")
                {
                    inpPlayerIndex.text = "1";
                }
                else
                {
                    inpPlayerIndex.text = "0";
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                inpIps[0].Select();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                inpIps[1].Select();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                OnConnect();
            }
        }

        private void OnConnect()
        {
            if (tgLocal.isOn)
            {
                NetworkInput.IS_LOCAL = true;
                gameManager.StartLocalGame();
            }
            else
            {
                SceneSettings.IsOnline = true;
                NetworkInput.IS_LOCAL = false;
                var connectionInfo = GetConnectionInfo();
                var perf = FindObjectOfType<GgpoPerformancePanel>();
                perf.Setup();
                var playerIndex = int.Parse(inpPlayerIndex.text);
                SceneSettings.OnlineIndex = playerIndex;
                gameManager.StartGGPOGame(perf, connectionInfo, playerIndex);
            }
        }
        public void RematchConnection()
        {
            NetworkInput.IS_LOCAL = false;
            var connectionInfo = GetConnectionInfo();
            var perf = FindObjectOfType<GgpoPerformancePanel>();
            perf.Setup();
            if (SceneSettings.OnlineIndex == -1)
            {
                SceneSettings.OnlineIndex = int.Parse(inpPlayerIndex.text);
            }
            gameManager.StartGGPOGame(perf, connectionInfo, SceneSettings.OnlineIndex);
        }

        public void StartGGPO(string ipOne, string ipTwo, string privateIpOne, string privateIpTwo, int portOne, int portTwo, int index)
        {
            string ipOneUsed = ipOne;
            string ipTwoUsed = ipTwo;
            if (ipOne == ipTwo)
            {
                ipOneUsed = privateIpOne;
                ipTwoUsed = privateIpTwo;
            }
            var connections = new List<Connections>();
            connections.Add(new Connections()
            {
                ip = ipOneUsed,
                port = (ushort)portOne,
                spectator = false,
            });
            connections.Add(new Connections()
            {
                ip = ipTwoUsed,
                port = (ushort)portTwo,
                spectator = false,
            });
            LoadConnectionInfo(connections);
            NetworkInput.IS_LOCAL = false;
            var connectionInfo = GetConnectionInfo();
            var perf = FindObjectOfType<GgpoPerformancePanel>();
            perf.Setup();
            var playerIndex = index;
            Printer.Log($"P1:" + ipOneUsed + "," + portOne);
            Printer.Log($"P2:" + ipTwoUsed + "," + portTwo);
            gameManager.StartGGPOGame(perf, connectionInfo, playerIndex);
        }

        private void OnDestroy()
        {
            if (gameManager != null)
            {
                gameManager.OnRunningChanged -= OnRunningChanged;
            }
            btnConnect.onClick.RemoveListener(OnConnect);
        }

        private void OnRunningChanged(bool obj)
        {
            gameObject.SetActive(!obj);
        }

        public void LoadConnectionInfo(IList<Connections> connections)
        {
            for (int i = 0; i < connections.Count; ++i)
            {
                inpIps[i].text = connections[i].ip + ":" + connections[i].port;
                tglSpectators[i].isOn = connections[i].spectator;
            }
        }

        public IList<Connections> GetConnectionInfo()
        {
            var connections = new List<Connections>(inpIps.Length);
            for (int i = 0; i < inpIps.Length; ++i)
            {
                var split = inpIps[i].text.Split(':');
                connections.Add(new Connections()
                {
                    ip = split[0],
                    port = ushort.Parse(split[1]),
                    spectator = false,
                });
            }
            return connections;
        }
    }
}