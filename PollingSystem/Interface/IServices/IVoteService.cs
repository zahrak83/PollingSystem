using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface IVoteService
    {
        bool AddVote(VoteDto dto);
        public int CountVotesByOption(int optionId);
        bool HasUserVoted(int userId, int surveyId);
    }
}
