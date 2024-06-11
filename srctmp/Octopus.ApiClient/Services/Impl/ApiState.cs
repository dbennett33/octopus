namespace Octopus.ApiClient.Services.Impl
{
    public class ApiState
    {
        public int CallsRemaining { get; set; } = 1;
        public int ResetTime { get; set; } = 0;
    }
}
