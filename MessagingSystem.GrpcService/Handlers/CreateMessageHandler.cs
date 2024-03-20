using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MessagingSystem.GrpcService.Commands;
using MessagingSystem.GrpcService.Data;

namespace MessagingSystem.GrpcService.Handlers
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, bool>
    {
        private readonly AppDbContext _context;

        public CreateMessageHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            _context.Messages.Add(request.message);
            var saved =  _context.SaveChanges();
            return Task.FromResult(saved > 0);
        }
    }
}