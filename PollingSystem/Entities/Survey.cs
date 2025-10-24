namespace PollingSystem.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int AdminId { get; set; }
        public Admin? Admin { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<UserSurvey> UserSurveys { get; set; } = new List<UserSurvey>();
        public List<Vote> Votes { get; set; } = new List<Vote>();
    }
}
