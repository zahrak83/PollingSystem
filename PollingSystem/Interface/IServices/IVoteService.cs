using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface IVoteService
    {
        bool AddVote(VoteDto dto);
    }
}
