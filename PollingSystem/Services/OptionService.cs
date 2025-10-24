using PollingSystem.Dtos;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;

namespace PollingSystem.Services
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _optionRepo;
        private readonly IVoteRepository _voteRepo;

        public OptionService(IOptionRepository optionRepo, IVoteRepository voteRepo)
        {
            _optionRepo = optionRepo;
            _voteRepo = voteRepo;
        }

        public List<OptionDto> GetOptionByQuestionId(int questionId)
        {
            try
            {
                var options = _optionRepo.GetByQuestionId(questionId);

                var dtos = new List<OptionDto>();

                foreach (var o in options)
                    dtos.Add(new OptionDto 
                    { 
                        Id = o.Id, 
                        Text = o.Text,
                        VoteCount = _voteRepo.GetCountByOptionId(o.Id) 
                    });
                return dtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Get options error: {ex.Message}");
            }
        }
    }
}

