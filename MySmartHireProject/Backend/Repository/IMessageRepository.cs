
using SmartHire.Models;
using System.Collections.Generic;

namespace SmartHire.Repositories
{
    public interface IMessageRepository
    {
        Message GetById(long id);
        IEnumerable<Message> GetAll();
        Message Create(Message message);
        Message Update(Message message);
        void Delete(long id);
    }
}
