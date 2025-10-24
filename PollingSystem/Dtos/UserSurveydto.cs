using PollingSystem.Enum;

namespace PollingSystem.Dtos
{
    public class UserSurveydto
    {
        public int UserId { get; set; }
        public int surveyId { get; set; }
        public string Status { get; set; } = "NotDone";
    }
}
