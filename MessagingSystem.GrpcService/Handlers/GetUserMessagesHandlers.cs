using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MessagingSystem.GrpcService.Data;
using MessagingSystem.GrpcService.Models;
using MessagingSystem.GrpcService.Queries;
using Microsoft.EntityFrameworkCore;

namespace MessagingSystem.GrpcService.Handlers
{
    public class GetUserMessagesHandlers : IRequestHandler<GetUserMessagesQuery, List<Message>>
    {
        private readonly AppDbContext _context;

        public GetUserMessagesHandlers(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Message>> Handle(GetUserMessagesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Messages
                                            .Where(m => m.SenderId == request.userId || m.RecipientId == request.userId)
                                            .AsNoTracking()
                                            .ToList()
                                );  
        }
    }
}