using PollingSystem.Dtos;
using PollingSystem.Entities;
using PollingSystem.Enum;

namespace PollingSystem.Interface.IServices
{
    public interface IUserSurveyService
    {
        void AddUserSurvey(int userId, int surveyId);
        bool HasUserSurvey(int userId, int surveyId);
        void UpdateUserSurvey(int userId, int surveyId, SurveyStatus status);
        void SetSurveyDone(int userId, int surveyId);
        UserSurveydto? GetUserSurvey(int userId, int surveyId);

    }
}
