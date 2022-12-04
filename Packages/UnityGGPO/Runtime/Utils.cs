using System;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityGGPO {

    public static class Utils {

        public static int CalcFletcher32(NativeArray<byte> data) {
            uint sum1 = 0;
            uint sum2 = 0;

            int index;
            for (index = 0; index < data.Length; ++index) {
                sum1 = (sum1 + data[index]) % 0xffff;
                sum2 = (sum2 + sum1) % 0xffff;
            }
            return unchecked((int)((sum2 << 16) | sum1));
        }

        public static string GetString(IntPtr ptrStr) {
            return ptrStr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptrStr) : "";
        }

        unsafe public static void* ToPtr(NativeArray<byte> data) {
            unsafe {
                return NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(data);
            }
        }

        unsafe public static NativeArray<byte> ToArray(void* dataPointer, int length) {
            unsafe {
                var array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, length, Allocator.Persistent);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
                NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, AtomicSafetyHandle.Create());
#endif
                return array;
            }
        }

        public static int TimeGetTime() {
            return (int)(UnityEngine.Time.realtimeSinceStartup * 1000);
        }

        public static void Sleep(int ms) {
            Thread.Sleep(ms);
        }
    }
}