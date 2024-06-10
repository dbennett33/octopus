namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents information about a particular installation.
    /// </summary>
    public class InstallInfo
    {
        /// <summary>
        /// Gets or sets the unique identifier for the installation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the system settings associated with the installation.
        /// </summary>
        public int SystemSettingsId { get; set; }

        /// <summary>
        /// Gets or sets the system settings associated with the installation.
        /// </summary>
        public SystemSettings? SystemSettings { get; set; }

        /// <summary>
        /// Gets or sets the version of the installation.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the date of the installation.
        /// </summary>
        public DateTime InstallStartDate { get; set; }
        public DateTime InstallEndDate { get; set; }

        public bool IsComplete { get; set; }
        public bool EnabledEntitiesApplied { get; set; }

        public string EnabledEntitiesJson { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the countries were installed.
        /// </summary>
        public bool CountriesInstalled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the leagues were installed.
        /// </summary>
        public bool LeaguesInstalled { get; set; }
    }
}