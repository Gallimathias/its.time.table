using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace its.time.table.Xamarin
{
    public class XWebRequest : WebRequest
    {
        public XWebResponse GetResponse()
        {
            XWebResponse response = null;

            try
            {
                response = (XWebResponse)base.GetResponse();
            } catch(WebException)
            {
                response = new XWebError(306);
            }

            return response;
        }

        
        public static XWebRequest CreateXWebRequest(Uri requestUri)
        {
           var webRequest = Create(requestUri);

            return (XWebRequest)webRequest;
        }

        public static XWebRequest CreateXWebRequest(string requestUri) => CreateXWebRequest(new Uri(requestUri));
    }
}
