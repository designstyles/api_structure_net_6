using Newtonsoft.Json;

namespace Backend.Domain.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<T>? Result { get; set; } = null;
        public IEnumerable<object>? Error { get; set; } = null;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ApiExceptionResponse
    {
        public ApiExceptionResponse(string errorMessage)
        {
            Result = errorMessage;
        }

        public string Result { get; set; }
    }


}
