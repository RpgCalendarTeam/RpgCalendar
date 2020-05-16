namespace RPGCalendar.Core.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using User = Data.User;

    public interface IUserRepository : IEntityRepository<User>
    {
        public Task<User?> GetUserByAuthId(string authId);
        public Task<User?> GetUserById(int userId);
    }
    public class UserRepository : EntityRepository<User>, IUserRepository
    {

        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<List<User>> FetchAllAsync()
        {
            var users = await Query.Include(u => u.GameUsers)
                .ToListAsync();
            return users;
        }

        public override async Task<User?> FetchByIdAsync(int id)
        {
            var user = await Query.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User?> GetUserByAuthId(string authId)
        {
            return await Query.Include(u => u.GameUsers)
                .FirstOrDefaultAsync(x => x.AuthId == authId);
        }

        public async Task<User?> GetUserById(int userId)
            => await Query.Include(u => u.GameUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
    }
}
