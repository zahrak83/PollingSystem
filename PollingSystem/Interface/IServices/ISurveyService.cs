using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface ISurveyService
    {
        SurveyDto GetById(int id);
        List<SurveyDto> GetAll();
        bool HasVotes(int surveyId);
    }
}
