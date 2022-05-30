using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Globalization;
using System.IO;

namespace Medley.Common.Utils
{
    public class StringUtils
    {
        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                return null;
            }

            byte[] HexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < HexAsBytes.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return HexAsBytes;
        }

        public static string ExtractFormatString(string strText, string strStart, string strStop)
        {
            string strResult = "";
            int nStartPos = 0;
            int nEndPos;
            if ((nStartPos = strText.IndexOf(strStart, nStartPos)) != -1)
            {
                if (strStop == "")
                {
                    strResult = strText.Substring(nStartPos + strStart.Length, strText.Length - nStartPos - strStart.Length);
                }
                else
                {
                    nEndPos = strText.IndexOf(strStop, nStartPos);
                    if (nEndPos != -1)
                    {
                        strResult = strText.Substring(nStartPos + strStart.Length, nEndPos - nStartPos - strStart.Length);
                    }
                }
            }

            return strResult;
        }

        public static StringCollection CreateStringList(string strText, int nMaxLineLen, string[] lineSeperator)
        {
            StringCollection stringList = new StringCollection();

            if (lineSeperator.Length < 2)
                return stringList;

            //first loop through every display line as definded by user.
            foreach (string strUserLine in strText.Split(new string[] { lineSeperator[0] }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (strUserLine.Length <= nMaxLineLen)
                {
                    stringList.Add(strUserLine.Trim(' '));
                }
                else
                {
                    string strLine = "";
                    foreach (string strWord in strUserLine.Split(new string[] { lineSeperator[1] }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        //first test if the new word will fit in current line
                        if ((strLine.Length + strWord.Length) > nMaxLineLen)
                        {
                            stringList.Add(strLine.Trim(' '));
                            strLine = "";
                        }

                        strLine += (strWord + " ");
                    }

                    stringList.Add(strLine.Trim(' '));
                }
            }

            return stringList;
        }


        //remove the last part of a string starting from the fromChar.
        public static string RemoveTailFromChar(string str, char fromChar)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            bool found = false;
            int i = str.Length -1;
            for (; i >= 0; i--)
            {
                if (str[i] == fromChar)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                return str.Substring(0, i); 
            }
            else
            {
                return str;
            }
        }

        public static string StreamToString(Stream stream)
        {            
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

    }
}
