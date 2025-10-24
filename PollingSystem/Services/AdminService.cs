
using PollingSystem.Dtos;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;

namespace PollingSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly ISurveyRepository _surveyRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly IOptionRepository _optionRepo;
        private readonly IVoteRepository _voteRepo;

        public AdminService(ISurveyRepository surveyRepo,
                            IQuestionRepository questionRepo,
                            IOptionRepository optionRepo,
                            IVoteRepository voteRepo)
        {
            _surveyRepo = surveyRepo;
            _questionRepo = questionRepo;
            _optionRepo = optionRepo;
            _voteRepo = voteRepo;
        }

        public SurveyDto CreateSurvey(CreateSurveyDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Title))
                    throw new Exception("Survey title cannot be empty.");

                if (dto.Questions == null || dto.Questions.Count == 0)
                    throw new Exception("Survey must have at least one question.");

                var survey = new Entities.Survey 
                {
                    Title = dto.Title,
                    AdminId = dto.AdminId 
                };

                _surveyRepo.Add(survey);

                foreach (var q in dto.Questions)
                {
                    if (string.IsNullOrWhiteSpace(q.Text))
                        throw new Exception("Question text cannot be empty.");

                    if (q.Options == null || q.Options.Count != 4)
                        throw new Exception("Each question must have exactly 4 options.");

                    var question = new Entities.Question 
                    { 
                        Text = q.Text, 
                        SurveyId = survey.Id 
                    };

                    _questionRepo.Add(question);

                    foreach (var o in q.Options)
                    {
                        if (string.IsNullOrWhiteSpace(o.Text))
                            throw new Exception("Option text cannot be empty.");

                        var option = new Entities.Option 
                        { 
                            Text = o.Text, 
                            QuestionId = question.Id 
                        };
                        _optionRepo.Add(option);
                    }
                }

                return new SurveyDto
                {
                    Id = survey.Id,
                    Title = survey.Title,
                    AdminName = survey.Admin?.FullName ?? "Unknown"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Create survey error: {ex.Message}");
            }
        }

        public List<SurveyDto> GetSurveyByAadmin(int adminId)
        {
            var surveys = _surveyRepo.GetByAdminId(adminId); 
            var surveyDtos = new List<SurveyDto>();

            foreach (var s in surveys)
            {
                var dto = new SurveyDto();
                dto.Id = s.Id;
                dto.Title = s.Title;
                dto.AdminName = s.Admin != null ? s.Admin.FullName : "-";
                dto.TotalParticipants = _voteRepo.GetParticipantId(s.Id).Count;

                surveyDtos.Add(dto);
            }

            return surveyDtos;
        }
        
        public SurveyResultDto GetSurveyResults(int surveyId)
        {
            var survey = _surveyRepo.GetById(surveyId);
            var questions = _questionRepo.GetBySurveyId(surveyId);

            var result = new SurveyResultDto();
            result.SurveyTitle = survey.Title;
            result.Questions = new List<QuestionResultDto>();

            foreach (var question in questions)
            {
                var options = _optionRepo.GetByQuestionId(question.Id);
                var qResult = new QuestionResultDto();
                qResult.QuestionText = question.Text;
                qResult.Options = new List<OptionResultDto>();

                int totalVotes = 0;
                foreach (var opt in options)
                {
                    totalVotes += _voteRepo.GetCountByOptionId(opt.Id);
                }

                foreach (var opt in options)
                {
                    int count = _voteRepo.GetCountByOptionId(opt.Id);
                    double percent = 0;
                    if (totalVotes != 0)
                        percent = (count * 100.0) / totalVotes;

                    var optResult = new OptionResultDto();
                    optResult.OptionText = opt.Text;
                    optResult.VoteCount = count;
                    optResult.Percentage = percent;

                    qResult.Options.Add(optResult);
                }

                result.Questions.Add(qResult);
            }

            result.TotalParticipants = _voteRepo.GetParticipantId(surveyId).Count;
            return result;
        }
        public void DeleteSurvey(int surveyId, int adminId)
        {
            try
            {
                var survey = _surveyRepo.GetById(surveyId);
                if (survey == null || survey.AdminId != adminId)
                    throw new Exception("Survey not found or not yours.");

                if (!_surveyRepo.HasVotes(surveyId))
                    throw new Exception("Cannot delete a survey.");

                _surveyRepo.Delete(survey);
            }
            catch (Exception ex)
            {
                throw new Exception($"Delete survey error: {ex.Message}");
            }
        }
    }
}
