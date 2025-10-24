namespace PollingSystem.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
        public int NormalUserId { get; set; }
        public NormalUser NormalUser { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
