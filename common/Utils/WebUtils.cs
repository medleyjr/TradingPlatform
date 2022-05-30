using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Net;


namespace Medley.Common.Utils
{
    public class WebUtils
    {
        public static Stream GetGeneralWebFile(string baseFolder, WebOperationContext webOperationContext)
        {
            string HttpAction = webOperationContext.IncomingRequest.UriTemplateMatch.RequestUri.AbsolutePath;
            HttpAction = baseFolder + HttpAction.Replace('/', '\\');
            //  Check if File Exists
            FileInfo _FileInfo = new FileInfo(HttpAction);
            if (_FileInfo.Exists)
            {
                //  Set Response Access-Control-Allow-Origin: *
                webOperationContext.OutgoingResponse.Headers["Access-Control-Allow-Origin"] = "*";
                webOperationContext.OutgoingResponse.Headers["Cache-Control"] = "no-cache";
                webOperationContext.OutgoingResponse.Headers["Pragma"] = "no-cache";

                webOperationContext.OutgoingResponse.ContentType = GetContentType(HttpAction);
                webOperationContext.OutgoingResponse.ContentLength = _FileInfo.Length;
                //  Open and read file
                var FileStream = _FileInfo.Open(FileMode.Open, FileAccess.Read);
                return FileStream;
            }
            else
            {
                webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
            }

            return null;
        }

        public static string GetContentType(string fileName)
        {
            string contentType = "application/octetstream";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (registryKey != null && registryKey.GetValue("Content Type") != null)
                contentType = registryKey.GetValue("Content Type").ToString();
            return contentType;
        }

    }
}
