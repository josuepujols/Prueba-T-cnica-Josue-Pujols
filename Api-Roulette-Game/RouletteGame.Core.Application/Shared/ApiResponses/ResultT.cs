using System.Net;

namespace RouletteGame.Core.Application.Shared.ApiResponses;

public class ResultT<T> : Result
{
    public T? Data { get; set; }

    public ResultT(string message, bool success, HttpStatusCode statusCode, T data) 
        : base(message, success, statusCode) => this.Data = data;
}
