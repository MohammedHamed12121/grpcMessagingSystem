using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MessagingSystem.GrpcService.Models;

namespace MessagingSystem.GrpcService.Commands
{
    public record CreateMessageCommand(Message message) : IRequest<bool>;
    
}