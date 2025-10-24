namespace PollingSystem.Dtos
{
    public class SurveyResultDto
    {
        public string? SurveyTitle { get; set; }
        public int TotalParticipants { get; set; }
        public List<QuestionResultDto>? Questions { get; set; }
    }
}