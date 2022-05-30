using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Medley.Common.Logging;

namespace Medley.Common.Utils
{
    public class MedleyVersion : IComparable<MedleyVersion>
    {
        private List<short> m_VersionArray = new List<short>();
        private string m_Version = "";

        public string Version 
        {
            get
            {
                return m_Version;
            }
        }

        public MedleyVersion()
        {
            
        }

        public MedleyVersion(string version)
        {            
            ParseVersion(version);
        }

        public void ParseVersion(string version)
        {            
            if (string.IsNullOrEmpty(version))
                throw new Exception("Version number is empty");

            try
            {
                version.Split('.').All(
                       s =>
                       {
                           m_VersionArray.Add(short.Parse(s));
                           return true;
                       });
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Version Parse Error.", ex);                
            }

            if(m_VersionArray.Count != 4)
                throw new Exception("Version Format Error");

            m_Version = version;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// 0 - values are equal
        /// 1 - This object is greater than the method parameter.
        /// -1 - This object is smaller than the method parameter.
        /// </returns>
        public int CompareTo(MedleyVersion obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.Version))
                return 1;
            else if (string.IsNullOrEmpty(Version))
                return -1;
            else if (Version == obj.Version)
                return 0;

            try
            {
                for(int i = 0; i < m_VersionArray.Count; i++)
                {
                    if (m_VersionArray[i] > obj.m_VersionArray[i])
                        return 1;
                    else if (m_VersionArray[i] < obj.m_VersionArray[i])
                        return -1;
                }
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to compare versions", ex);
            }
             
            //versions are equal if we reach this point
            return 0;
        }

        public static bool operator <(MedleyVersion vl, MedleyVersion vr)
        {
            return (vl.CompareTo(vr) == -1);
        }

        public static bool operator >(MedleyVersion vl, MedleyVersion vr)
        {
            return (vl.CompareTo(vr) == 1);
        }

       /* public static bool operator ==(TLVersion vl, TLVersion vr)
        {
            return vl.CompareTo(vr) == 0;
        }

        public static bool operator !=(TLVersion vl, TLVersion vr)
        {
            return vl.CompareTo(vr) != 0;
        }*/
    }
}
