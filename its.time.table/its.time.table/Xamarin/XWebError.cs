using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace its.time.table.Xamarin
{
    public class XWebError
    {
        private WebExceptionStatus status;

        public int Code { get; private set; }
        public HttpStatusCode HttpStatus => (HttpStatusCode)Code;

        public XWebError(int code)
        {
            Code = code;
        }

        public XWebError(WebExceptionStatus status)
        {
            Code = (int)status;
        }
    }
}
