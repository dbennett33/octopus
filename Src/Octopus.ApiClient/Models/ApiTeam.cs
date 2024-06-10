namespace Octopus.ApiClient.Models
{
    public class ApiTeam
    {
        public TeamData? Team { get; set; }
        public VenueData? Venue { get; set; }
    }

    public class TeamData
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Country { get; set; }
        public int? Founded { get; set; }
        public bool National { get; set; }
        public string? Logo { get; set; }
    }

    public class VenueData
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int Capacity { get; set; }
        public string? Surface { get; set; }
        public string? Image { get; set; }
    }
}
