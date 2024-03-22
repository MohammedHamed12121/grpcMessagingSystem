using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MessagingSystem.GrpcService.Models;
using MessagingSystem.GrpcService.Protos;

namespace MessagingSystem.GrpcService
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateMessageRequest, Message>()
                .ForMember(d => d.Content, o => o.MapFrom(c => c.MessageContent));
                

            CreateMap<Message, MessageDetailsResponse>()
                .ForMember(d => d.MessageId, o => o.MapFrom(c => c.Id))
                .ForMember(d => d.MessageContent, o => o.MapFrom(c => c.Content));
        }
    }
}