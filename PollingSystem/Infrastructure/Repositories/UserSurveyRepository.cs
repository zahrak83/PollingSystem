using PollingSystem.Entities;
using PollingSystem.Enum;
using PollingSystem.Interface.IRepositories;

namespace PollingSystem.Infrastructure.Repositories
{
    public class UserSurveyRepository : IUserSurveyRepository
    {
        private readonly AppDbContext _context;

        public UserSurveyRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddUserSurvey(int userId, int surveyId, SurveyStatus status)
        {
            var userSurvey = new UserSurvey
            {
                NormalUserId = userId,
                SurveyId = surveyId,
                Status = status
            };
            _context.usersurveys.Add(userSurvey);
            _context.SaveChanges();
        }

        public UserSurvey? getByUserSurvey(int userId, int surveyId)
        {
           return _context.usersurveys
                .FirstOrDefault(us => us.NormalUserId == userId && us.SurveyId == surveyId);
        }

        public bool HasUserSurvey(int userId, int surveyId)
        {
            return _context.usersurveys
                .Any(us => us.NormalUserId == userId && us.SurveyId == surveyId);
        }

        public void SetStatus(int userId, int surveyId, SurveyStatus Status)
        {
            var existing = _context.usersurveys
                .FirstOrDefault(us => us.NormalUserId == userId && us.SurveyId == surveyId);
            if (existing != null)
            {
                existing.Status = Status;
                _context.SaveChanges();
            }
        }
    }
}
