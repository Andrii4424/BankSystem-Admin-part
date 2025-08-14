using Application.ServiceContracts.IUserService;
using AutoMapper;
using Domain.RepositoryContracts;

namespace Application.Services.UserService
{
    public class UserUpdateService : IUserUpdateService {

        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserUpdateService> _logger;
        private readonly IMapper _mapper;

        public UserUpdateService(IUserRepository userRepository, ILogger<UserUpdateService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }



    }
}
