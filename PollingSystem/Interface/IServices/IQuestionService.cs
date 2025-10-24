using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface IQuestionService
    {
        List<QuestionDto> GetBySurveyId(int surveyId);
    }
}
