using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface ISurveyService
    {
        List<SurveyDto> GetAll();
    }
}
