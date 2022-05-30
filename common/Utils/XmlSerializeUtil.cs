using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

using Medley.Common.Logging;

namespace Medley.Common.Utils
{
    public class XmlSerializeUtil
    {
        public static T DeserializeFromXMLFile<T>(string strXMLFile)
        {            
            FileStream oXMLFile = null;
            T obj = default(T);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                oXMLFile = new FileStream(strXMLFile, FileMode.Open);
                obj = (T)serializer.Deserialize(oXMLFile);
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to load xml file " + strXMLFile, ex);
            }
            finally
            {
                if(oXMLFile != null)
                    oXMLFile.Close();
            }

            return obj;
        }

        public static T DeserializeFromXML<T>(string strXML)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T)); 
            T obj = default(T);

            try
            {
                obj = (T)serializer.Deserialize(new StringReader(strXML));
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to deserialize xml.", ex);
            }            
                   

            return obj;
        }

        public static string SerializeToXML<T>(T obj, XmlSerializerNamespaces ns, bool bOmitXmlDec)
        {
            if (ns == null)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add("", "");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = bOmitXmlDec;                
                
            MemoryStream oMemoryStream = new MemoryStream();
            XmlWriter xmlWriter = XmlWriter.Create(oMemoryStream, settings);
            serializer.Serialize(xmlWriter, obj, ns);
            oMemoryStream.Position = 0;                
            return new StreamReader(oMemoryStream).ReadToEnd();
           
        }

        public static bool SerializeToXMLFile<T>(T obj, string filename, XmlSerializerNamespaces ns, bool bOmitXmlDec)
        {           

            string xmlData = SerializeToXML<T>(obj, ns, bOmitXmlDec);

            if (string.IsNullOrEmpty(xmlData))
                return false;

            xmlData = IndentXMLString(xmlData);

            try
            {
                File.WriteAllText(filename, xmlData);
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to write xml for file " + filename, ex);
                return false;
            }

            return true;          
        }

        public static T DeserializeXMLNode<T>(XmlDocument xmlDoc, string xPath)
        {
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xPath);

            if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.OuterXml))
            {
                try
                {
                    return XmlSerializeUtil.DeserializeFromXML<T>(xmlNode.OuterXml);
                }
                catch (Exception ex)
                {
                    MedleyLogger.Instance.Error("Error while deserialising Xml node.", ex);
                }
            }

            return default(T);
        }

        public static string IndentXMLString(string xml)
        {
            if (String.IsNullOrEmpty(xml))
            {
                return xml;
            }

            string outXml = string.Empty;
            MemoryStream ms = new MemoryStream();
            // Create a XMLTextWriter that will send its output to a memory stream (file)
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8);
            
            XmlDocument doc = new XmlDocument();   
            
            try
            {
                // Load the unformatted XML text string into an instance 
                // of the XML Document Object Model (DOM)
                doc.LoadXml(xml);
                // Set the formatting property of the XML Text Writer to indented
                // the text writer is where the indenting will be performed
                xtw.Formatting = Formatting.Indented;
                xtw.Namespaces = false;

                // write dom xml to the xmltextwriter
                doc.WriteContentTo(xtw);
                // Flush the contents of the text writer
                // to the memory stream, which is simply a memory file
                xtw.Flush();

                // set to start of the memory stream (file)
                ms.Seek(0, SeekOrigin.Begin);
                // create a reader to read the contents of 
                // the memory stream (file)
                StreamReader sr = new StreamReader(ms);
                // return the formatted string to caller
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                MedleyLogger.Instance.Error("Failed to Indent XML", ex);
                return xml;
            }
        }
    }
}
