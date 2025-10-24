namespace PollingSystem.Entities
{
    public class NormalUser : User
    {
        public List<Vote> Votes { get; set; } = new List<Vote>();
        public List<UserSurvey> StatusSurveys { get; set;} = new List<UserSurvey>();
    }
}
