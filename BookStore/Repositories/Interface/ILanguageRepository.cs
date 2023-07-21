using BookStore.Models;

namespace BookStore.Repositories.Interface
{
    public interface ILanguageRepository
    {
        public Task<List<LanguageModel>> GetAllLanguage();
    }
}
