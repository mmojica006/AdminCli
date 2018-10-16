﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Configuration;

namespace Helpers
{
    public class FCMPushNotification
    {
         string apiKey = ConfigurationManager.AppSettings["googleApi"];

        public FCMPushNotification()
        {
            // TODO: Add constructor logic here
        }

        public bool Successful
        {
            get;
            set;
        }

        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }



        public FCMPushNotification SendNotification(string _title, string _message, string device)
        {
            FCMPushNotification result = new FCMPushNotification();
            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;
                var requestUri = ConfigurationManager.AppSettings["fcmUrl"]; ;

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", apiKey));
                //webRequest.Headers.Add(string.Format("Sender: id={0}", YOUR_FCM_SENDER_ID));
                webRequest.ContentType = "application/json";

                var data = new
                {
                     to = device, // Uncoment this if you want to test for single device
                    //to = "/topics/" + _topic, // this is for topic 
                    notification = new
                    {
                        title = _title,
                        body = _message,
                        //icon="myicon"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }
    }
}