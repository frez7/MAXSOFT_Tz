using System.Text.Json;

namespace Market.Domain.Common
{
    /// <summary>
    /// Класс базового Response
    /// </summary>
    public class Response
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public Response(int statusCode, bool success, string message)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
