using PollingSystem.Entities;

namespace PollingSystem.Interface.IRepositories
{
    public interface IOptionRepository
    {
        List<Option> GetByQuestionId(int questionId);
        void Add(Option option);
    }
}
