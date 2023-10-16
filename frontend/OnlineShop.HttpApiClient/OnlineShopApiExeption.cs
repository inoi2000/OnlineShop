using OnlineShop.HttpApiClient;
using OnlineShop.HttpModels.Responses;
using System.Net;
using System.Runtime.Serialization;

namespace OnlineShopHttpApiClient
{
    [Serializable]
    public class OnlineShopApiExeption : Exception
    {
        private ErrorResponse? Error { get; }
        public HttpStatusCode StatusCode { get; }
        public ValidationProblemDetails Details { get; }

        public OnlineShopApiExeption(HttpStatusCode statusCode, ValidationProblemDetails details) : base(details.Title)
        {
            if (details is null) { throw new ArgumentNullException(nameof(details)); }

            StatusCode = statusCode;
            Details = details;
            Error = new ErrorResponse(details.Title!, statusCode);
        }

        public OnlineShopApiExeption(ErrorResponse error) : base(error.Message)
        {
            Error = error;
            StatusCode = error.HttpStatusCode;
            Details = new ValidationProblemDetails() { Title = error.Message, Status = (int)StatusCode };
        }

        public OnlineShopApiExeption(string? message) : base(message)
        {
        }

        public OnlineShopApiExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OnlineShopApiExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}