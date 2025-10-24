namespace PollingSystem.Dtos
{
    public class QuestionResultDto
    {
        public string? QuestionText { get; set; }
        public List<OptionResultDto>? Options { get; set; }
    }
}