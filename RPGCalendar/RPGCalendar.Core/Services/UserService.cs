using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCalendar.Core.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Dto;
    using Microsoft.EntityFrameworkCore;
    using User = Data.User;

    public interface IUserService : IEntityService<Dto.User, Dto.UserInput>
    {
        public Task<Dto.User?> RegisterUser(UserInput userInput);
        public Task<Dto.User?> LoginUser(string authId);
        public Task<User?> GetUserById(int userId);
    }
    public class UserService : EntityService<Dto.User, Dto.UserInput, User>, IUserService
    {
        private readonly ISessionService _sessionService;

        public UserService(ApplicationDbContext dbContext, IMapper mapper, ISessionService sessionService)
            : base(dbContext, mapper)
        {
            _sessionService = sessionService;
        }

        public override async Task<List<Dto.User>> FetchAllAsync()
        {
            var users = await Query.Include(u => u.GameUsers)
                .ToListAsync();
            return Mapper.Map<List<User>, List<Dto.User>>(users);
        }

        public override async Task<Dto.User?> FetchByIdAsync(int id)
        {
            var user = await Query.FirstOrDefaultAsync(x => x.Id == id);
            return Mapper.Map<User, Dto.User>(user);
        }

        public override Task<Dto.User?> InsertAsync(UserInput dto)
            => RegisterUser(dto);


        public async Task<Dto.User?> RegisterUser(UserInput userInput)
        {
            User user = Mapper.Map<UserInput, User>(userInput);
            DbContext.Add(user);
            await DbContext.SaveChangesAsync();
            _sessionService.SetCurrentUserId(user.Id);
            return Mapper.Map<User, Dto.User>(user);
        }

        public async Task<Dto.User?> LoginUser(string authId)
        {
            var user = await Query.FirstOrDefaultAsync(x => x.AuthId == authId);
            _sessionService.SetCurrentUserId(user.Id);
            var mappedUser = Mapper.Map<User, Dto.User>(user);
            return mappedUser;
        }

        public async Task<User?> GetUserById(int userId)
            => await Query.Include(u => u.GameUsers).FirstOrDefaultAsync(x => x.Id == userId);
    }
}
