using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface IAdminService
    {
        SurveyDto CreateSurvey(CreateSurveyDto dto);
        SurveyResultDto GetSurveyResults(int surveyId);
        List<SurveyDto> GetSurveyByAadmin(int adminId);
        void DeleteSurvey(int surveyId, int adminId);
    }
}
