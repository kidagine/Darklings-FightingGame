using UnityEngine;

namespace Demonics.Utility
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static DontDestroyOnLoad instance;
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                this.transform.SetParent(null);
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }
}