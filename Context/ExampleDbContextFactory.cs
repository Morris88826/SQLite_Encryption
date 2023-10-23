using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEncryption.Context
{
    public class ExampleDbContextFactory
    {
        private readonly string _connectionString;

        public ExampleDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ExampleDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new ExampleDbContext(options);
        }
    }
}
