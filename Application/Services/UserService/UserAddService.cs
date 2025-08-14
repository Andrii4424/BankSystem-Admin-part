using Application.DTO;
using Application.DTO.BankProductDto;
using Application.DTO.PersonDto;
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
        private readonly IWebHostEnvironment _env;

        public UserAddService(IUserRepository userRepository, ILogger<UserAddService> logger, IMapper mapper, IWebHostEnvironment env)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _env = env;
        }

        public async Task<OperationResult> AddUser(UserDto userDto, List<IFormFile>? documents, IFormFile? profilePhoto)
        {
            _logger.LogInformation("Trying to add user with surname {surname}", userDto.Surname);
            if (profilePhoto==null)
            {
                userDto.ProfilePicturePath = "uploads/no-avatar.svg";
            }
            else
            {
                Guid photoId = Guid.NewGuid();
                string fileName = $"{photoId}{profilePhoto.FileName.ToLower()}";
                string absolutePath = Path.Combine(_env.WebRootPath, "uploads", "user-logo", fileName);
                using (var stream = new FileStream(absolutePath, FileMode.Create))
                {
                    await profilePhoto.CopyToAsync(stream);
                }
                userDto.ProfilePicturePath = $"uploads/user-logo/{fileName}";
            }


            //Надо сделать добавление фото для документов, лого сделано, и дальше все оставшиеся круд операции
        }
    }
}
