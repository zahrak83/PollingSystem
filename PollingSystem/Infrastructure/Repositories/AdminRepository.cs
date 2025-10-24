using Microsoft.EntityFrameworkCore;
using PollingSystem.Entities;
using PollingSystem.Interface.IRepositories;

namespace PollingSystem.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public Admin? GetByUsername(string username, string password)
        {
            return _context.admins
                .Include(a => a.Surveys)
                .FirstOrDefault(a => a.Username == username && a.Password == password);
        }
    }
}
