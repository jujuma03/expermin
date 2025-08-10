using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.DATABASE.Data
{
    public class ExperminContextFactory : IDesignTimeDbContextFactory<ExperminContext>
    {
        public ExperminContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../EXPERMIN.API");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<ExperminContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ExperminContext(optionsBuilder.Options);
        }
    }
}
