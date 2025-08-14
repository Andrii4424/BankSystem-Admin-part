using Application.DTO.FiltersDto;
using Application.DTO.PersonDto;

namespace Application.ServiceContracts.IUserService
{
    public interface IUserReadService
    {
        public Task<List<UserDto?>> GetUsersList(UserFilters filters);
        public Task<UserDto?> GetUserById(Guid userId);
    }
}
