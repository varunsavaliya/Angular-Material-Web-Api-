using AutoMapper;
using Demo.DataModels;
using Demo.Models.SMSTemplate;
using Demo.Models.User;

namespace Demo.Configuration
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<SMSTemplate, SMSTemplateModel>().ReverseMap();
        }
    }
}
