namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents a venue.
    /// </summary>
    public class Venue
    {
        /// <summary>
        /// Gets or sets the ID of the venue.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the venue.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the venue.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city of the venue.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the capacity of the venue.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the surface of the venue.
        /// </summary>
        public string Surface { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the image URL of the venue.
        /// </summary>
        public string Image { get; set; } = string.Empty;
    }
}