using BallastLane.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Domain.ValueObjects
{
    public class Result<T>
    {
        public Result()
        {
        }

        public Result(T content)
        {
            Content = content;
        }

        public Result(IDictionary<string, List<string>> errors)
        {
            Errors = errors;
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.";
            Title = "One or more validation errors occurred.";
            Status = 400;
            TraceId = Guid.NewGuid().ToString();
        }

        public Result(string errorCode, string errorMessage)
        {
            Errors.AddError(errorCode, errorMessage);
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.";
            Title = "One or more validation errors occurred.";
            Status = 400;
            TraceId = Guid.NewGuid().ToString();
        }

        public Result(T data, string errorCode, string errorMessage)
        {
            Content = data;
            Errors.AddError(errorCode, errorMessage);
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.";
            Title = "One or more validation errors occurred.";
            Status = 400;
            TraceId = Guid.NewGuid().ToString();
        }

        [JsonIgnore]
        public T Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("status")]
        public int Status { get; private set; }

        [JsonProperty("traceId")]
        public string TraceId { get; private set; }

        [JsonProperty("errors")]
        public IDictionary<string, List<string>> Errors { get; private set; } = new Dictionary<string, List<string>>();

        public void AddError(string errorCode, string errorMessage)
        {
            Errors.AddError(errorCode, errorMessage);
        }

        public bool HasErrors()
        {
            if (Errors is null)
            {
                return false;
            }

            return Errors.Any();
        }
    }

    public class Result
    {
        public Result()
        {
        }

        public Result(IDictionary<string, List<string>> errors)
        {
            Errors = errors;
        }

        public Result(string errorCode, string errorMessage)
        {
            Errors.AddError(errorCode, errorMessage);
        }

        public IDictionary<string, List<string>> Errors { get; private set; } = new Dictionary<string, List<string>>();

        public bool HasErrors()
        {
            if (Errors is null)
            {
                return false;
            }

            return Errors.Any();
        }
    }
}
