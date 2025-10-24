namespace PollingSystem.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
    }
}
