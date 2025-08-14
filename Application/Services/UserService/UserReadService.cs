using Application.DTO.FiltersDto;
using Application.DTO.PersonDto;
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

        public async Task<List<UserDto?>> GetUsersList(UserFilters filters)
        {
            _logger.LogInformation("Try to get users list");
            Filters<UserEntity> generalFilters = filters.ToGeneralFilters();
            _logger.LogInformation("Successfull getting users list");
            return _mapper.Map<List<UserDto?>>(await _userRepository.GetLimitedAsync(generalFilters.FirstItem, generalFilters.ElementsToLoad, generalFilters.SearchFilter, 
                generalFilters.Ascending, generalFilters.SortValue, generalFilters.EntityFilters));
        }

        public async Task<UserDto?> GetUserById(Guid userId)
        {
            _logger.LogInformation("Try to get user with id {userId}", userId);
            UserEntity? user = await _userRepository.GetValueByIdAsync(userId);
            if (user == null) {
                _logger.LogInformation("Failed when tryed get user. User with Id {userId} doesnt exist", userId);
                throw new ArgumentException($"Failed when tryed get user. User with Id {userId} doesnt exist");
            }
            _logger.LogInformation("Successfull getting user with id {userId}", userId);
            return _mapper.Map<UserDto>(user);
        }
    }
}
