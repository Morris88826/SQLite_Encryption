using DatabaseEncryption.EncryptedEntity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEncryption.Context
{
    public class ExampleDbContext : DbContext
    {
        public DbSet<EncryptedUser> Users { get; set; }
        public ExampleDbContext(DbContextOptions options) : base(options)
        {

        }

    }
}
