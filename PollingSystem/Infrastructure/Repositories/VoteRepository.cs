using PollingSystem.Entities;
using PollingSystem.Interface.IRepositories;

namespace PollingSystem.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;

        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetCountByOptionId(int optionId)
        {
            return _context.votes.Count(v => v.OptionId == optionId);
        }

        public int GetCountBySurveyId(int surveyId)
        {
            return _context.votes.Count(v => v.SurveyId == surveyId);
        }

        public bool HasUserVoted(int userId, int surveyId)
        {
            return _context.votes.Any(v => v.NormalUserId == userId && v.SurveyId == surveyId);
        }

        public void Add(Vote vote)
        {
            _context.votes.Add(vote);
            _context.SaveChanges();
        }

        public List<int> GetParticipantId(int surveyId)
        {
            return _context.votes
                .Where(v => v.SurveyId == surveyId)
                .Select(v => v.NormalUserId)
                .Distinct()
                .ToList();
        }

        public List<Vote> GetBySurveyId(int surveyId)
        {
            return _context.votes
                .Where(v => v.SurveyId == surveyId)
                .ToList();
        }
    }
}
