using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Octopus.EF.Data
{
    /// <summary>
    /// Factory class for creating the design-time instance of the OctoDbContext.
    /// </summary>
    public class DesignTimeOctopusDbContextFactory : IDesignTimeDbContextFactory<OctopusDbContext>
    {
        /// <summary>
        /// Creates a new instance of the OctoDbContext for design-time use.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>The design-time instance of the OctoDbContext.</returns>
        public OctopusDbContext CreateDbContext(string[] args)
        {
            // Configure the builder to use your appsettings.json configuration file
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<OctopusDbContext>();
            
            // Get the connection string from the configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new OctopusDbContext(builder.Options);
        }
    }
}