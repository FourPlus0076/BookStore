using BookStore.Data;
using BookStore.Models;
using BookStore.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreDbContext _dbContext;
        
        public LanguageRepository(BookStoreDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<List<LanguageModel>> GetAllLanguage()
        {
            return await _dbContext.Language.Select(x => new LanguageModel() {
                Id= x.Id,
                Name = x.Name,
                Description= x.Description,
            }).ToListAsync();
            
            //try
            //{
            //    var language=new List<LanguageModel>();
            //    var allLanguages = await _dbContext.Language.Select(x=> new LanguageModel).ToListAsync();
            //    if (allLanguages?.Any()==true)
            //    {
            //        foreach (var item in allLanguages)
            //        {
            //            language.Add(new LanguageModel(){ 
            //               Id= item.Id,
            //               Name= item.Name,
            //               Description= item.Description,
            //            });                      
            //        }
            //    }
            //    return language;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
           
        }
    }
}
