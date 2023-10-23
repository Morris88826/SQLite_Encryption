using DatabaseEncryption.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEncryption.API
{
    public class User
    {
        public static async Task<IEnumerable<EncryptedEntity.EncryptedUser>> GetAllUsers(ExampleDbContextFactory exampleContextFactory)
        {
            using (ExampleDbContext dbContext = exampleContextFactory.CreateDbContext())
            {
                IEnumerable<EncryptedEntity.EncryptedUser> users = await dbContext.Users.ToListAsync();
                return users;
            }
        }

        public static async Task CreateUser(ExampleDbContextFactory exampleContextFactory, EncryptedEntity.EncryptedUser user)
        {
            using (ExampleDbContext dbContext = exampleContextFactory.CreateDbContext())
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteUserWithId(ExampleDbContextFactory exampleContextFactory, int userId)
        {
            using (ExampleDbContext dbContext = exampleContextFactory.CreateDbContext())
            {
                dbContext.Remove(dbContext.Users.Single(x => x.ID == userId));
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
