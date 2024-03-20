using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingSystem.GrpcService.Models
{
    public class Message
    {
        public string? Id { get; set; }
        public string? SenderId { get; set; }
        public string? RecipientId { get; set; }
        public string? Content { get; set; }
    }
}