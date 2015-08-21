using System;
using System.Net;

namespace BigRedCloud.Api.Exceptions
{
    public class ApiRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string ReasonPhrase { get; private set; }
        public string Content { get; private set; }

        public ApiRequestException() { }

        public ApiRequestException(HttpStatusCode statusCode, string reasonPhrase, string content) 
            : base(GetExceptionMessage(statusCode, reasonPhrase, content))
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
            Content = content;
        }

        private static string GetExceptionMessage(HttpStatusCode statusCode, string reasonPhrase, string content)
        {
            string messageTemplate = "Error occured during processing of the request. Code: {0} {1}.";
            if (!String.IsNullOrEmpty(content))
            {
                messageTemplate += " Message: {2}";
            }
            return String.Format(messageTemplate, (int)statusCode, reasonPhrase, content);
        }
    }
}
