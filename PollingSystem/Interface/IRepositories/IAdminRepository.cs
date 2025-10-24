using PollingSystem.Entities;

namespace PollingSystem.Interface.IRepositories
{
    public interface IAdminRepository
    {
        Admin? GetByUsername(string username, string password);
    }
}
