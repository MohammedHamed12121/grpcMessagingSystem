using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MessagingSystem.GrpcService.Models;
using MessagingSystem.GrpcService.Protos;

namespace MessagingSystem.GrpcService.Queries
{
    public record GetUserMessagesQuery(string userId) : IRequest<List<Message>>;
}