using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MessagingSystem.GrpcService.Commands;
using MessagingSystem.GrpcService.Data;

namespace MessagingSystem.GrpcService.Handlers
{
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteMessageHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == request.MessageId);
            if (message != null)
            {
                _context.Remove(message);
                var saved = _context.SaveChanges();
                return Task.FromResult(saved > 0);
            }
            return Task.FromResult(false);
        }
    }
}