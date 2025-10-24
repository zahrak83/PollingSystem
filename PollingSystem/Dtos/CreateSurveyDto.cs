namespace PollingSystem.Dtos
{
    public class CreateSurveyDto
    {
        public string? Title { get; set; }
        public int AdminId { get; set; }
        public List<CreateQuestionDto>? Questions { get; set; }
    }
}