using PollingSystem.Entities;

namespace PollingSystem.Interface.IRepositories
{
    public interface IQuestionRepository
    {
        List<Question> GetBySurveyId(int surveyId);
        void Add(Question question);
    }
}
