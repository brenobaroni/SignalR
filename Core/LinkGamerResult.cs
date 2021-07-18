using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Core
{
    public class LinkGamerResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        public LinkGamerResult(HttpStatusCode statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
        }

        public LinkGamerResult(HttpStatusCode statusCode, bool success, string message)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
        }

        public LinkGamerResult(HttpStatusCode statusCode, bool success, object result)
        {
            StatusCode = statusCode;
            Success = success;
            Result = result;
        }

        public LinkGamerResult(HttpStatusCode statusCode, bool success, string message, object result)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
            Result = result;
        }
    }
}
