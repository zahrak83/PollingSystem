namespace PollingSystem.Entities
{
    public class Admin : User
    {
        public List<Survey> Surveys { get; set; } = new List<Survey>();
    }
}
