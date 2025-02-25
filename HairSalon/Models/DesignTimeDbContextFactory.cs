using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HairSalon.Models
{
  public class HairSalonContextFactory : IDesignTimeDbContextFactory<HairSalonContext>
  {
    HairSalonContext IDesignTimeDbContextFactory<HairSalonContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var builder = new DbContextOptionsBuilder<HairSalonContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new HairSalonContext(builder.Options);
    }
  }
}