using AutoMapper;
using Codes.Service.Models;
using Codes.Service.ViewModels;

namespace ClientPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CampaignModel, CampaignViewModel>();
        }
    }
}
