using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Newtonsoft.Json;

using Medley.Common.Logging;
using System.Linq.Expressions;

namespace Medley.Common.Utils
{
    public static class GenUtils
    {
        public static object LoadAssembly(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            try
            {
                Type type = Type.GetType(path, true);
                return Activator.CreateInstance(type);
            }
            catch(Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to load assembly: " + path, ex);
                return null;
            }
        }

        public static int? ParseIntString(string val)
        {
            if (string.IsNullOrEmpty(val))
                return null;

            try
            {
                return int.Parse(val);
            }
            catch
            {
                return null;
            }
        }

        public static string IntToString(int? val)
        {
            if (val.HasValue)
                return val.Value.ToString();
            else
                return null;
        }

        public static string LongToString(long? val)
        {
            if (val.HasValue)
                return val.Value.ToString();
            else
                return null;
        }

        /// <summary>
        /// Returns the Property Name
        /// </summary>
        /// <typeparam name="MyPropertyType"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetPropertyName<MyPropertyType>(Expression<Func<MyPropertyType>> property)
        {
            return ((MemberExpression)property.Body).Member.Name;
        }
        
        public static void RestartService(string serviceName)
        {
            Process proc = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.CreateNoWindow = true;
            psi.FileName = "cmd.exe";
            psi.Arguments = string.Format(@"/C net stop ""{0}"" && net start ""{0}""", serviceName);
            psi.LoadUserProfile = false;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo = psi;
            proc.Start();
        }

        public static void StopService(string serviceName)
        {
            Process proc = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.CreateNoWindow = true;
            psi.FileName = "cmd.exe";
            psi.Arguments = string.Format(@"/C net stop ""{0}"" ", serviceName);
            psi.LoadUserProfile = false;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo = psi;
            proc.Start();
        }

        public static void RestartProcess(string processName, ProcessWindowStyle windowStyle)
        {
            ProcessStartInfo Info = new ProcessStartInfo();
            
            Info.Arguments = "/C ping 127.0.0.1 -n 2 && \"" + processName + "\"";
            Info.WindowStyle = windowStyle;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
            Environment.Exit(0);
        }

        public static void ExitProcess(string processName)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Make a deep copy of an object by using a binary serialiser to serialise the input object
        /// and deserialise it in the return obj
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CloneObject(this object obj)
        {         
            /*MemoryStream mStream = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(mStream, obj);
            mStream.Position = 0;
            return b.Deserialize(mStream);*/
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            };

            string data = JsonConvert.SerializeObject(obj, jsonSetting);

            return JsonConvert.DeserializeObject(data, jsonSetting);
        }        
        
    }
}
