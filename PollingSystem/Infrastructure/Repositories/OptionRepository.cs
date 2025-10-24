using Microsoft.EntityFrameworkCore;
using PollingSystem.Entities;
using PollingSystem.Interface.IRepositories;

namespace PollingSystem.Infrastructure.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly AppDbContext _context;

        public OptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Option> GetByQuestionId(int questionId)
        {
            return _context.options
                .Where(o => o.QuestionId == questionId)
                .Include(o => o.Votes)
                .OrderByDescending(o => o.Votes.Count)
                .ToList();
        }

        public void Add(Option option)
        {
            _context.options.Add(option);
            _context.SaveChanges();
        }
    }
}
