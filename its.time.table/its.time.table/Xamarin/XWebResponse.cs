using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace its.time.table.Xamarin
{
    
    public class XWebResponse : WebResponse
    {
        public HttpStatusCode StatusCode { get; private set; }

        private XWebResponse(HttpStatusCode errorcode) : base()
        {
            StatusCode = errorcode;            
        }

        public static implicit operator XWebResponse(XWebError error)
        {
            return new XWebResponse(error.HttpStatus);
        }
    }
}
