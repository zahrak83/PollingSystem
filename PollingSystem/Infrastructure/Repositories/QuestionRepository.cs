using Microsoft.EntityFrameworkCore;
using PollingSystem.Entities;
using PollingSystem.Interface.IRepositories;

namespace PollingSystem.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Question> GetBySurveyId(int surveyId)
        {
            return _context.questions
                .Where(q => q.SurveyId == surveyId)
                .Include(q => q.Options)
                .ToList();
        }

        public void Add(Question question)
        {
            _context.questions.Add(question);
            _context.SaveChanges();
        }
    }
}