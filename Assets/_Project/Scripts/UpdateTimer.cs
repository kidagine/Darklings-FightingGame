using System.Threading.Tasks;
using UnityEngine;

namespace Demonics.Utility
{
    public static class UpdateTimer
    {
        public static async Task WaitFor(float waitTime)
        {
            if (waitTime <= 0f) return;
            float timer = 0;
            while (timer < waitTime)
            {
                timer += Time.deltaTime;
                await Task.Yield();
            }
        }
    }
}
