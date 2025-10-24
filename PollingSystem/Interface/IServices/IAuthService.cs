using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface IAuthService
    {
        UserDto Login(string username, string password, bool isAdmin);
    }
}
