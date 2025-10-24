using PollingSystem.Entities;
using PollingSystem.Enum;

namespace PollingSystem.Interface.IRepositories
{
    public interface IUserSurveyRepository
    {
        void AddUserSurvey(int userId,int surveyId, SurveyStatus status);
        bool HasUserSurvey(int userId, int surveyId);
        void SetStatus(int userId, int surveyId, SurveyStatus Status);
        UserSurvey? getByUserSurvey(int userId, int surveyId);
    }
}
