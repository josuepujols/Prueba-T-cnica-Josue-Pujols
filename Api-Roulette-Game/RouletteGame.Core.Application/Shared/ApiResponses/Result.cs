using System.Net;

namespace RouletteGame.Core.Application.Shared.ApiResponses;

public class Result
{
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; } = false;
    public HttpStatusCode StatusCode { get; set; }

    public Result()
    {
        Message = string.Empty;
        Success = false;
        StatusCode = 0;
    }

    public Result(string message, bool success, HttpStatusCode statusCode)
    {
        Message = message;
        Success = success;
        StatusCode = statusCode;
    }
}
