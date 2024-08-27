using Microsoft.EntityFrameworkCore;

using SmartHire.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartHire.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Message GetById(long id)
        {
            return _context.Messages.Find(id);
        }

        public IEnumerable<Message> GetAll()
        {
            return _context.Messages.ToList();
        }

        public Message Create(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public Message Update(Message message)
        {
            _context.Messages.Update(message);
            _context.SaveChanges();
            return message;
        }

        public void Delete(long id)
        {
            var message = _context.Messages.Find(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
                _context.SaveChanges();
            }
        }
    }
}
