using PollingSystem.Entities;

namespace PollingSystem.Interface.IRepositories
{
    public interface ISurveyRepository
    {
        Survey? GetById(int id);
        List<Survey> GetAll();
        void Add(Survey survey);
        bool HasVotes(int surveyId);
        void Delete(Survey survey);
        List<Survey> GetByAdminId(int adminId);
    }
}
