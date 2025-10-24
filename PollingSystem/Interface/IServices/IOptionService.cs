using PollingSystem.Dtos;

namespace PollingSystem.Interface.IServices
{
    public interface IOptionService
    {
        List<OptionDto> GetOptionByQuestionId(int questionId);
    }
}
