using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TaskManager.HubService.Model;

namespace TaskManager.HubService.Tools
{
    public class ApiHelper
    {
        public static object Data(ClientResult result)
        {
            //return result.repObject.get_Item("data");
            return null;
        }

        public static ClientResult Get(string weburl, string data, Encoding encode)
        {
            byte[] byteArray = encode.GetBytes(data);

            var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(weburl));
            webRequest.Method = "POST";
            webRequest.Accept = "application/xml";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = byteArray.Length;
            using (Stream newStream = webRequest.GetRequestStream())
            {
                newStream.Write(byteArray, 0, byteArray.Length);
            }

            //接收返回信息：
            var str = string.Empty;
            using (var response = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var aspx = new StreamReader(response.GetResponseStream(), encode))
                {
                    str = aspx.ReadToEnd();
                }
            }
            var clientResult = new ClientResult
            {
                ResString = str
            };
            return clientResult;
        }

        public static long GetTimeStamp(DateTime dateTime)
        {
            var span = (dateTime.ToUniversalTime() - new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)span.TotalMilliseconds;
        }

        public static object Response(ClientResult result)
        {
            //return result.repObject.get_Item("response");
            return null;
        }
    }
}
