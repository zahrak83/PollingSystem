using PollingSystem.Dtos;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;

namespace PollingSystem.Services
{
    public class NormalUserService : INormalUserService
    {
        private readonly ISurveyRepository _surveyRepo;
        private readonly IVoteRepository _voteRepo;

        public NormalUserService(ISurveyRepository surveyRepo,
                                 IVoteRepository voteRepo)
        {
            _surveyRepo = surveyRepo;
            _voteRepo = voteRepo;
        }
        public List<SurveyDto> GetAvailableSurveys()
        {
            try
            {
                var surveys = _surveyRepo.GetAll();
                var dtos = new List<SurveyDto>();

                foreach (var s in surveys)
                {
                    dtos.Add(new SurveyDto
                    {
                        Id = s.Id,
                        Title = s.Title,
                        AdminName = s.Admin.FullName,
                        TotalParticipants = _voteRepo.GetCountBySurveyId(s.Id)
                    });
                }

                return dtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Get surveys error: {ex.Message}");
            }
        }

        public bool Vote(VoteDto dto)
        {
            try
            {
                if (_voteRepo.HasUserVoted(dto.UserId, dto.SurveyId))
                    throw new Exception("You have already voted in this survey.");

                var vote = new Entities.Vote
                {
                    OptionId = dto.OptionId,
                    NormalUserId = dto.UserId,
                    SurveyId = dto.SurveyId
                };

                _voteRepo.Add(vote);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Vote error: {ex.Message}");
            }
        }
    }
}

