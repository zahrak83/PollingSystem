using Microsoft.EntityFrameworkCore;
using PollingSystem.Entities;
using PollingSystem.Interface.IRepositories;

namespace PollingSystem.Infrastructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly AppDbContext _context;

        public SurveyRepository(AppDbContext context)
        {
            _context = context;
        }

        public Survey? GetById(int id)
        {
            return _context.surveys
                .Include(s => s.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefault(s => s.Id == id);
        }

        public List<Survey> GetAll()
        {
            return _context.surveys.Include(s => s.Admin).ToList();
        }

        public void Add(Survey survey)
        {
            _context.surveys.Add(survey);
            _context.SaveChanges();
        }

        public void Delete(Survey survey)
        {
            _context.surveys.Remove(survey);
            _context.SaveChanges();
        }

        public bool HasVotes(int surveyId)
        {
            return _context.votes.Any(v => v.SurveyId == surveyId);
        }

        public List<Survey> GetByAdminId(int adminId)
        {
            return _context.surveys
                .Where(s => s.AdminId == adminId)
                .ToList();
        }
    }
}
