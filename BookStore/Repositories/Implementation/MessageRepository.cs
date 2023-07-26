using BookStore.Models;
using BookStore.Repositories.Interface;
using Microsoft.Extensions.Options;

namespace BookStore.Repositories.Implementation
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AlertBookModel _alertBook;
        public MessageRepository(IOptionsMonitor<AlertBookModel> alertBook)
        {
            _alertBook = alertBook.CurrentValue;
        }
        public string GetName() 
        {
            return _alertBook.Name;
        }
    }
}
