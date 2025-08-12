using Application.ServiceContracts.IUserService;
using AutoMapper;
using Domain.RepositoryContracts;

namespace Application.Services.UserService
{
    public class UserDeleteService :IUserDeleteService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserDeleteService> _logger;
        private readonly IMapper _mapper;

        public UserDeleteService(IUserRepository userRepository, ILogger<UserDeleteService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
    }
}
