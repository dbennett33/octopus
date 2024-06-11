namespace Octopus.ApiClient.Models
{
    public class ApiResponse<T>
    {
        public string? Get { get; set; }
        public object? Parameters { get; set; }
        public object? Errors { get; set; }
        public int Results { get; set; }
        public Paging? Paging { get; set; }
        public List<T>? Response { get; set; }
    }

    public class Paging
    {
        public int Current { get; set; }
        public int Total { get; set; }
    }
}