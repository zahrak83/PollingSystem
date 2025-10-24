using PollingSystem.Enum;

namespace PollingSystem.Entities
{
    public class UserSurvey
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public NormalUser User { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public SurveyStatus Status { get; set; } = SurveyStatus.NotDone;
    }
}
