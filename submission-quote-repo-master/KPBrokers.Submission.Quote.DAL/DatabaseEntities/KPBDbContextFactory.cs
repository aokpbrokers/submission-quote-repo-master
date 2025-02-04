using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities
{
    public class KPBDbContextFactory : IDesignTimeDbContextFactory<KPBDbContext>
    {
        public KPBDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<KPBDbContext>();

            // Here, you need to use your actual connection string (you can hard-code or load from configuration).
            optionsBuilder.UseSqlServer("Server=kpbsqldev.database.windows.net;Database=KPBQuoteSubmission;User Id=kpbrokersadmin;Password=kpbrokersadmin;");

            return new KPBDbContext(optionsBuilder.Options);
        }
    }
}