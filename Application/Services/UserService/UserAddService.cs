using Application.ServiceContracts.IUserService;
using AutoMapper;
using Domain.RepositoryContracts;

namespace Application.Services.UserService
{
    public class UserAddService: IUserAddService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserAddService> _logger;
        private readonly IMapper _mapper;

        public UserAddService(IUserRepository userRepository, ILogger<UserAddService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
    }
}
