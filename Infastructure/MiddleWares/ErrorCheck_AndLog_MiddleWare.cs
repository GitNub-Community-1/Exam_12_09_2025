using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Infastructure.MiddleWares;

public class ErrorCheck_AndLog_MiddleWare
{
    private readonly RequestDelegate _next;

    public ErrorCheck_AndLog_MiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task<string> InvokeAsync(HttpContext context)
    {
        try
        {
            var headReques = context.Request.Headers.ToString();
            var queryParams = context.Request.Query.ToString();
            int statusCode = context.Response.StatusCode;
            string clientIp = context.Connection.RemoteIpAddress.ToString();
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine();
            Console.WriteLine("====================================================");
            Console.WriteLine($"Head of requerst: {headReques},\n query parametrs: {queryParams}, \n status code: {statusCode},\n IP:  {clientIp},\n Time: {time}");
            Console.WriteLine("====================================================");
            Console.WriteLine();
            await _next(context);
            stopwatch.Stop();
            long elapsedMs = stopwatch.ElapsedMilliseconds;
            Console.WriteLine();
            Console.WriteLine($"The Request and Response continue {elapsedMs}/ms");
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine();
            Console.WriteLine("====================================================");
            Console.WriteLine("Your Error Message: " + e.Message);
            Console.WriteLine();
            Console.WriteLine(e.InnerException?.Message);
            Console.WriteLine("====================================================");
            Console.WriteLine();
            return $"Error {e.Message}";
        }

        return "MiddleWare Work";
    }
}