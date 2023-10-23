using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEncryption.Context
{
    public class ExampleDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExampleDbContext>
    {
        public ExampleDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=exampleDb.db").Options;

            return new ExampleDbContext(options);
        }
    }
}
