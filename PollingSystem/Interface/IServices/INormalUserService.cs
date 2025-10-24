using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface INormalUserService
    {
        List<SurveyDto> GetAvailableSurveys();
        bool Vote(VoteDto dto);
    }
}
