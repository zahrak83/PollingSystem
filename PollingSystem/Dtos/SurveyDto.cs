namespace PollingSystem.Dtos
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? AdminName { get; set; }
        public int TotalParticipants { get; set; }
    }
}