using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MessagingSystem.GrpcService.Commands;
using MessagingSystem.GrpcService.Data;

namespace MessagingSystem.GrpcService.Handlers
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateMessageHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == request.MessageId);
            if (message != null)
            {
                message.Content = request.Content;
                int saved = _context.SaveChanges();
                return Task.FromResult(saved > 0);
            }
            return Task.FromResult(false);
        }
    }
}