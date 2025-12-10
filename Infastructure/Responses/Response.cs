using System.Net;

namespace Infastructure.Responses;

public class Response<T>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public Response(HttpStatusCode statusCode, string message)
    {
        StatusCode = (int)statusCode;
        Message = message;
    }
    

    public Response(HttpStatusCode statusCode, string message, T? data)
    {
        StatusCode = (int)statusCode;
        Message = message;
        Data = data;
    }
}