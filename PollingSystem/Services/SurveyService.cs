using PollingSystem.Dtos;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;

namespace PollingSystem.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepo;
        private readonly IVoteRepository _voteRepo;

        public SurveyService(ISurveyRepository surveyRepo, IVoteRepository voteRepo)
        {
            _surveyRepo = surveyRepo;
            _voteRepo = voteRepo;
        }

        public SurveyDto GetById(int id)
        {
            var s = _surveyRepo.GetById(id);

            if (s == null)
                throw new Exception("Survey not found.");

            return new SurveyDto
            {
                Id = s.Id,
                Title = s.Title,
                AdminName = s.Admin.FullName,
                TotalParticipants = _voteRepo.GetCountBySurveyId(s.Id)
            };
        }

        public List<SurveyDto> GetAll()
        {
            var surveys = _surveyRepo.GetAll();

            var dtos = new List<SurveyDto>();

            foreach (var s in surveys)
                dtos.Add(new SurveyDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    AdminName = s.Admin.FullName,
                    TotalParticipants = _voteRepo.GetParticipantId(s.Id).Count
                });
            return dtos;
        }

        public bool HasVotes(int surveyId)
        {
            return _surveyRepo.HasVotes(surveyId);
        }
    }
}

