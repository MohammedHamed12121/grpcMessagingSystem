using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingSystem.GrpcService.Models;

namespace MessagingSystem.GrpcService.Providers
{
    public class MessagesProvider
    {
        private static readonly List<Message> _messages = new List<Message>();

        public MessagesProvider()
        {
            // _messages = new List<Message>();
        }

        public void AddMessage(Message message)
        {
            var messageId = _messages.Count()+1;
            message.Id = $"{messageId}";
            _messages.Add(message);
        }

        public Message GetMessageById(string id)
        {
            return _messages.FirstOrDefault(m => m.Id == id);
        }

        public bool UpdateMessage(string messageId, string newContent)
        {
            var message = _messages.FirstOrDefault(m => m.Id == messageId);
            if (message != null)
            {
                message.Content = newContent;
                return true;
            }
            return false;
        }

        public bool DeleteMessage(string messageId)
        {
            var message = _messages.FirstOrDefault(m => m.Id == messageId);
            if (message != null)
            {
                _messages.Remove(message);
                return true;
            }
            return false;
        }

        public List<Message> GetMessagesForUser(string userId)
        {
            return _messages.Where(m => m.SenderId == userId || m.RecipientId == userId).ToList();
        }
    }
}