using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

using Medley.Common.Logging;

namespace Medley.Common.Utils
{
    public class XmlUtils
    {
        //merge xml2 into xml1. xml2 will over write elements of xml1
        public static string MergeXMLDocs(string xml1, string xml2)
        {
            string resultXml = "";

            if (string.IsNullOrEmpty(xml1))
                return xml2;

            if (string.IsNullOrEmpty(xml2))
                return xml1;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml1);
            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(xml2);

            if (doc.DocumentElement == null || doc2.DocumentElement == null ||
                doc.DocumentElement.Name != doc2.DocumentElement.Name)
            {
                return xml1;
            }
            else
            {
                string xpath = @"/" + doc2.DocumentElement.Name;
                MergeXmlDocsRecursive(doc.DocumentElement, doc2.DocumentElement, ref xpath);
            }

            resultXml = doc.OuterXml;

            return IndentXMLString(resultXml);
        }

        private static void MergeXmlDocsRecursive(XmlNode xmlMasterNode, XmlNode xmlSlaveNode, ref string xpath)
        {
            //  Iterate through child document and pick up nodes
            foreach (XmlNode node in xmlSlaveNode.ChildNodes)
            {
                //  copy leaf node to source tree
                //  Reached the last node in the tree so these are the only ones to merge
                if (node.ChildNodes.Count == 0 || node.FirstChild.NodeType == XmlNodeType.Text)
                {
                    //  Find same node in master
                    XmlNode sourceNode = xmlMasterNode.SelectSingleNode(xpath);

                    if (sourceNode == null)
                        continue; //copy nothing.

                    //  Check the a match attribute
                    //  This will try and identify a specific node to replace
                    string matchingNodePath = node.Name;
                    XmlAttribute matchAttrib = (XmlAttribute)node.Attributes.GetNamedItem("apply_where");
                    if (matchAttrib != null)
                    {
                        //  Must be of format xx=yyy
                        if (matchAttrib.Value.IndexOf('=') > -1)
                        {
                            string[] keyvalues = matchAttrib.Value.Split(new char[] {'='});
                            matchingNodePath += "[@" + keyvalues[0] + "='" + keyvalues[1].Replace("'","") + "']";
                            //  Remove the attribute
                            node.Attributes.Remove(matchAttrib);
                        }
                    }
                    var sourceNodeChild = sourceNode.SelectSingleNode(matchingNodePath);

                    var newNode = sourceNode.OwnerDocument.ImportNode(node, true);
                    if (sourceNodeChild == null)
                        sourceNode.AppendChild(newNode);
                    else
                    {
                        sourceNode.ReplaceChild(newNode, sourceNodeChild);
                    }
                }
                else
                {
                    xpath += @"/" + node.Name;
                    MergeXmlDocsRecursive(xmlMasterNode, node, ref xpath);
                }
            }
        }

        public static string GetXmlAttrValFromPath(XmlDocument xmlDoc, string xPath, string attributeName)
        {
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xPath);

            if (xmlNode == null || xmlNode.Attributes.GetNamedItem(attributeName) == null)
                return null;

            return xmlNode.Attributes.GetNamedItem(attributeName).Value;
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
                //  xtw.Namespaces = false;

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
