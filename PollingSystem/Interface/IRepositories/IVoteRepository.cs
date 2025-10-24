using PollingSystem.Entities;

namespace PollingSystem.Interface.IRepositories
{
    public interface IVoteRepository
    {
        int GetCountByOptionId(int optionId);
        int GetCountBySurveyId(int surveyId);
        bool HasUserVoted(int userId, int surveyId);
        void Add(Vote vote);
        List<int> GetParticipantId(int surveyId);
    }
}
