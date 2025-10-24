using PollingSystem.Dtos;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;

namespace PollingSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepo;
        private readonly INormalUserRepository _normalUserRepo;

        public AuthService(IAdminRepository adminRepo, INormalUserRepository normalUserRepo)
        {
            _adminRepo = adminRepo;
            _normalUserRepo = normalUserRepo;
        }
        public UserDto Login(string username, string password, bool isAdmin)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    throw new Exception("Username and password cannot be empty or whitespace.");

                if (isAdmin)
                {
                    var admin = _adminRepo.GetByUsername(username, password);
                    if (admin == null)
                        throw new Exception("Invalid username or password.");

                    return new UserDto
                    {
                        Id = admin.Id,
                        FullName = admin.FullName,
                        Username = admin.Username
                    };
                }
                else
                {
                    var user = _normalUserRepo.GetByUsername(username, password);
                    if (user == null)
                        throw new Exception("Invalid username or password.");

                    return new UserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Username = user.Username
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Login error: {ex.Message}");
            }
        }

    }

}