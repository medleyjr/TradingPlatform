using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

using Medley.Common.Defs.Interfaces;
using Medley.Common.Utils;
using System.Reflection;

namespace IGTradingLib
{

    public static class DB
    {
        public static TradingRepository Rep = null;

       
    }

    public class RepositoryFactory
    {      

        public static TradingRepository GetRepository(string name = "Database.ADO")
        {
            /*if (name == "Database.ADO")
                return (IRepository)GenUtils.LoadAssembly("Medley.Common.ADODatabase.ADORepository, Medley.Common.ADODatabase");
            else
                return null;*/

            return new TradingRepository();
        }

        public static TradingRepository GetRepository(string name, string settingName)
        {
            return GetRepository(name, settingName, @".\");
        }

        public static TradingRepository GetRepository(string name, string settingName, string configPath)
        {
            TradingRepository repository = GetRepository(name);

            if (repository != null && !string.IsNullOrEmpty(settingName))
            {
                string path = Assembly.GetExecutingAssembly().Location;
                if (File.Exists(configPath + @"Config\Repositories.xml"))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(configPath + @"Config\Repositories.xml");
                    XmlNode xmlNode = xmlDoc.SelectSingleNode(string.Format("/Repositories/Repository[@Name=\"{0}\"]", settingName));
                    repository.Open(xmlNode.InnerXml);
                }
                else
                {
                    throw new Exception("Failed to open database. Config\\Repositories.xml file do not exist.");
                }
            }
            

            return repository;
        }
    }
}
