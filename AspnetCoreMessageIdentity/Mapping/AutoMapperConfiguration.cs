using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Models.MessageModels;
using AutoMapper;

namespace AspnetCoreMessageIdentity.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Mails, CreateMessageViewModel>().ReverseMap();
            CreateMap<Mails, ReplayMailViewModel>().ReverseMap();
            CreateMap<Mails, ForwadMailViewModel>().ReverseMap();
     
    
        }
    }
}
