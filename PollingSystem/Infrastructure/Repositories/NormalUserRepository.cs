using Microsoft.EntityFrameworkCore;
using PollingSystem.Entities;
using PollingSystem.Interface.IRepositories;

namespace PollingSystem.Infrastructure.Repositories
{
    public class NormalUserRepository : INormalUserRepository
    {
        private readonly AppDbContext _context;

        public NormalUserRepository(AppDbContext context)
        {
            _context = context;
        }
        public NormalUser? GetByUsername(string username, string password)
        {
            return _context.normalusers
                .Include(u => u.Votes)
                .Include(u => u.UserSurveys)
                .FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}