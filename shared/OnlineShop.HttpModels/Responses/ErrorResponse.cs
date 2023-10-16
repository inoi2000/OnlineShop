using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.HttpModels.Responses
{
    public record class ErrorResponse
    {
        public ErrorResponse(string message, HttpStatusCode httpStatusCode)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }

        public string Message { get; }
        public HttpStatusCode HttpStatusCode { get; }
    }
}
