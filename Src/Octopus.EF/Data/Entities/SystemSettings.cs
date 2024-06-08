namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the system settings in the application.
    /// </summary>
    public class SystemSettings
    {
        /// <summary>
        /// Gets or sets the unique identifier for the system settings.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the current version of the system settings.
        /// </summary>
        public string CurrentVersion { get; set; }
        
        /// <summary>
        /// Gets or sets the Installed status
        /// </summary>
        public bool Installed { get; set; }
    }
}