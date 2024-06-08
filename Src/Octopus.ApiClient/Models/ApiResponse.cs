namespace Octopus.ApiClient.Models
{
    public class ApiResponse<T>
    {
        public string? Get { get; set; }
        public List<object>? Parameters { get; set; }
        public List<object>? Errors { get; set; }
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