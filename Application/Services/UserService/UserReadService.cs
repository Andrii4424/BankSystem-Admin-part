using Application.ServiceContracts.IUserService;
using AutoMapper;
using Domain.Entities.Persons;
using Domain.RepositoryContracts;

namespace Application.Services.UserService
{
    public class UserReadService: IUserReadService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserReadService> _logger;
        private readonly IMapper _mapper;

        public UserReadService(IUserRepository userRepository, ILogger<UserReadService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /*public async List<UserEntity?> GetUsersList()
        {

        }*/
    }
}
