using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MessagingSystem.GrpcService.Commands
{
    public record DeleteMessageCommand(string MessageId) : IRequest<bool>;
}