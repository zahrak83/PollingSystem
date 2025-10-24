using PollingSystem.Dtos;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;

namespace PollingSystem.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly IOptionRepository _optionRepo;

        public QuestionService(IQuestionRepository questionRepo, IOptionRepository optionRepo)
        {
            _questionRepo = questionRepo;
            _optionRepo = optionRepo;
        }

        public List<QuestionDto> GetBySurveyId(int surveyId)
        {
            try
            {
                var questions = _questionRepo.GetBySurveyId(surveyId);

                var dtos = new List<QuestionDto>();

                foreach (var q in questions)
                {
                    var options = _optionRepo.GetByQuestionId(q.Id);

                    var optDtos = new List<OptionDto>();

                    foreach (var o in options)
                        optDtos.Add(new OptionDto 
                        { 
                            Id = o.Id,
                            Text = o.Text,
                            VoteCount = o.Votes.Count 
                        });

                    dtos.Add(new QuestionDto 
                    { 
                        Id = q.Id, 
                        Text = q.Text,
                        Options = optDtos
                    });
                }
                return dtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Get questions error: {ex.Message}");
            }
        }
    }
}

