namespace Octopus.Sync.Configurations
{
    public class EnabledEntitiesConfig
    {
        public List<EnabledCountry> EnabledCountries { get; set; } = new List<EnabledCountry>();
    }

    public class EnabledCountry
    {
        public string Name { get; set; }
        public List<string> Leagues { get; set; } = new List<string>();
    }

}
