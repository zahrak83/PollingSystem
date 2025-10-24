using PollingSystem.Entities;

namespace PollingSystem.Interface.IRepositories
{
    public interface INormalUserRepository
    {
        NormalUser? GetByUsername(string username, string password);
    }
}
