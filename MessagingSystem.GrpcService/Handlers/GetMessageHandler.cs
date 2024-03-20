using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MessagingSystem.GrpcService.Data;
using MessagingSystem.GrpcService.Models;
using MessagingSystem.GrpcService.Protos;
using MessagingSystem.GrpcService.Queries;
using Microsoft.EntityFrameworkCore;

namespace MessagingSystem.GrpcService.Handlers
{
    public class GetMessageHandler : IRequestHandler<GetMessageQuery, Message>
    {
        private readonly AppDbContext _context;

        public GetMessageHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task<Message> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Messages.FirstOrDefault(m => m.Id == request.id));
        }
    }
}