using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using MessagingSystem.GrpcService.Commands;
using MessagingSystem.GrpcService.Models;
using MessagingSystem.GrpcService.Protos;
using MessagingSystem.GrpcService.Providers;
using MessagingSystem.GrpcService.Queries;

namespace MessagingSystem.GrpcService.Services
{
    public class MessageService : MessagingService.MessagingServiceBase
    {
        private readonly IMediator _mediatr;

        public MessageService(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        public override async Task<MessageResponse> CreateMessage(CreateMessageRequest request, ServerCallContext context)
        {
            if (request.MessageContent == string.Empty || request.RecipientId == string.Empty || request.SenderId == string.Empty)
            {

                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must suppply a valid object"));
            }

            // TODO: use AutoMapper
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                Content = request.MessageContent
            };

            // TODO: This will return a bool use it to handle errors
            await _mediatr.Send(new CreateMessageCommand(message));

            return await Task.FromResult(new MessageResponse
            {
                Success = true,
                Message = "message was add to the data base"
            });
        }

        public override async Task<MessageDetailsResponse> GetMessage(GetMessageRequest request, ServerCallContext context)
        {
            if (int.Parse(request.MessageId) <= 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "resouce index must be greater than 0"));
            }

            var message = await _mediatr.Send(new GetMessageQuery(request.MessageId));


            return await Task.FromResult(new MessageDetailsResponse
            {
                MessageId = message.Id,
                SenderId = message.SenderId,
                RecipientId = message.RecipientId,
                MessageContent = message.Content
            });
        }

        public override async Task<UserMessagesResponse> GetUserMessages(GetUserMessagesRequest request, ServerCallContext context)
        {
            var response = new UserMessagesResponse();
            var userMessages = await _mediatr.Send(new GetUserMessagesQuery(request.UserId));

            foreach (Message message in userMessages)
            {
                response.Messages.Add(new MessageDetailsResponse
                {
                    MessageId = message.Id,
                    SenderId = message.SenderId,
                    RecipientId = message.RecipientId,
                    MessageContent = message.Content,

                });
            }

            return await Task.FromResult(response);
        }

        public override async Task<MessageResponse> UpdateMessage(UpdateMessageRequest request, ServerCallContext context)
        {
            
            if (request.NewMessageContent == string.Empty)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must suppply a valid object"));

            var message =  await _mediatr.Send(new UpdateMessageCommand(request.MessageId, request.NewMessageContent));

            if (!message){
                throw new RpcException(new Status(StatusCode.NotFound, $"No Message with Id {request.MessageId}"));
            }

            return await Task.FromResult(new MessageResponse
            {
                Success = true,
                Message = "message updated ..."
            });
        }
        public override async Task<MessageResponse> DeleteMessage(DeleteMessageRequest request, ServerCallContext context) 
        {
            
            var message = await _mediatr.Send(new DeleteMessageCommand(request.MessageId));

            if(!message)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No Message with Id {request.MessageId}"));
            }
            

            return await Task.FromResult(new MessageResponse
            {
                Success = true,
                Message = "message deleted ..."
            });
        }
    }
}