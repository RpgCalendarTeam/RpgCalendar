namespace RPGCalendar.Core.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Dto;
    using Repositories;
    using User = Data.User;

    public interface IUserService
    {
        public Task<Dto.User?> RegisterUser(UserInput userInput);
        public Task<Dto.User?> LoginUser(string authId);
        public Task<User?> GetUserById(int userId);
    }
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, ISessionService sessionService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _userRepository = userRepository;
        }

        public async Task<Dto.User?> RegisterUser(UserInput userInput)
        {
            User user = _mapper.Map<UserInput, User>(userInput);
            await _userRepository.InsertAsync(user);
            _sessionService.SetCurrentUserId(user.Id);
            return _mapper.Map<User, Dto.User>(user);
        }

        public async Task<Dto.User?> LoginUser(string authId)
        {
            var user = await _userRepository.GetUserByAuthId(authId);
            if (user is null)
                return null;
            _sessionService.SetCurrentUserId(user.Id);
            return _mapper.Map<User, Dto.User>(user);
        }

        public async Task<User?> GetUserById(int userId)
            => await _userRepository.GetUserById(userId);
    }
}
