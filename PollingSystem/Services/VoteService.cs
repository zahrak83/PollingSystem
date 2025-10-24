using PollingSystem.Dtos;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;

namespace PollingSystem.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepo;

        public VoteService(IVoteRepository voteRepo)
        {
            _voteRepo = voteRepo;
        }

        public bool AddVote(VoteDto dto)
        {
            try
            {
               

                var vote = new Entities.Vote
                {
                    NormalUserId = dto.UserId,
                    SurveyId = dto.SurveyId,
                    OptionId = dto.OptionId
                };

                _voteRepo.Add(vote);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Add vote error: {ex.Message}");
            }
        }

        public int CountVotesByOption(int optionId)
        {
            try
            {
                return _voteRepo.GetCountByOptionId(optionId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Count votes error: {ex.Message}");
            }
        }

        public bool HasUserVoted(int userId, int surveyId)
        {
            var votes = _voteRepo.GetBySurveyId(surveyId);
            foreach (var v in votes)
            {
                if (v.NormalUserId == userId)
                    return true;
            }
            return false;
        }
    }
}


