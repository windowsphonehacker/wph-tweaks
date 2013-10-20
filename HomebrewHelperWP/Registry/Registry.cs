using Registry;
using RPCComponent;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomebrewHelperWP
{
    public static class Registry
    {
        public static string ReadString(RegistryHive hive, string path, string value)
        {
            string ret = string.Empty;
            if (!NativeRegistry.ReadString(hive, path, value, out ret))
            {
                uint error = NativeRegistry.GetError();
                SammyReadString(hive, path, value, out error);
                //if (error != 0)
                //{
                //throw new Exception("Read failed: " + error);
                //}
            }
            return ret;
        }
        public static void WriteString(RegistryHive hive, string path, string value, string data)
        {
            if (!NativeRegistry.WriteString(hive, path, value, data))
            {
                uint error = NativeRegistry.GetError();
                SammyWriteString(hive, path, value, data, out error);
                //if (error != 0)
                //{
                //    throw new Exception("Write failed: " + error);
                //}
            }
        }
        internal static uint SammyWriteString(RegistryHive hive, string path, string value, string data, out uint error)
        {
            InitSammy();
            return CRPCComponent.Registry_SetString((uint)hive, path, value, data, out error);
        }

        internal static string SammyReadString(RegistryHive hive, string path, string value, out uint error)
        {
            InitSammy();
            return CRPCComponent.Registry_GetString((uint)hive, path, value, out error);
        }

        internal static bool SammyInited = false;
        internal static void InitSammy()
        {
            if (!SammyInited)
            {
                CRPCComponent.Initialize();
                SammyInited = true;
            }
        }
    }
}
