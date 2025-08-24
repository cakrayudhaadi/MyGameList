using System.Net;

namespace MyGameList.Src.Shared.Commons
{
    public class CommonResponse()
    {
    }

    public class ApiResponse<T>(int status, string message, T? data)
    {
        public int Status { get; set; } = status;
        public string Message { get; set; } = message;
        public T? Data { get; set; } = data;
    }

    public class ApiResponse(int status, string message)
    {
        public int Status { get; set; } = status;
        public string Message { get; set; } = message;
    }

    public class Response<T>(HttpStatusCode httpStatusCode, string message, T? data)
    {
        public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
        public string Message { get; set; } = message;
        public T? Data { get; set; } = data;
    }

    public class Response(HttpStatusCode httpStatusCode, string message)
    {
        public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
        public string Message { get; set; } = message;
    }
}
