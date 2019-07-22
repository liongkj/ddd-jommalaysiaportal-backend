using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Gateways
{
    public class GatewayException : Exception
    {
        /// <summary>
        /// Gets the type of error which triggered this exception.
        /// </summary>
        public WebServiceExceptionType Type { get; private set; }

        /// <summary>
        /// Gets the HTTP status code returned (if this is a response error).
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Brief information of the reason of exception.</param>
        public GatewayException(string message)
            : base(message)
        {
            this.Type = WebServiceExceptionType.ConnectionError;
        }

        /// <summary>
        /// Constructor for response error.
        /// </summary>
        /// <param name="statusCode">HTTP Status Code returned.</param>
        /// <param name="message">Brief information of the reason of exception.</param>
        public GatewayException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.Type = WebServiceExceptionType.ResponseError;
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Constructor for connection error.
        /// </summary>
        /// <param name="message">Brief information of the reason of exception.</param>
        /// <param name="innerException">Inner exception to be included.</param>
        public GatewayException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.Type = WebServiceExceptionType.ConnectionError;
        }
    }

    /// <summary>
    /// An enum indicating the type of error occured during the request.
    /// </summary>
    public enum WebServiceExceptionType
    {
        ConnectionError,
        ResponseError
    };
}