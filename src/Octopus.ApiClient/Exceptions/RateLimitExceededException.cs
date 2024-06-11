namespace Octopus.ApiClient.Exceptions
{
    public class RateLimitExceededException : Exception
    {
        public DateTime ResetTime { get; }

        public RateLimitExceededException(DateTime resetTime)
            : base($"API rate limit exceeded. Try again after {resetTime}.")
        {
            ResetTime = resetTime;
        }

        public RateLimitExceededException(DateTime resetTime, string message)
            : base(message)
        {
            ResetTime = resetTime;
        }

        public RateLimitExceededException(DateTime resetTime, string message, Exception inner)
            : base(message, inner)
        {
            ResetTime = resetTime;
        }
    }
}
