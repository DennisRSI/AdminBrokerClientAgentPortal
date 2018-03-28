using AutoMapper;
using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using System;

namespace ClientPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CampaignModel, CampaignViewModel>()
                .ForMember(c => c.PreLoginVideoId, opt => opt.MapFrom(src => src.PreLoginVideo.VideoId))
                .ForMember(c => c.PostLoginVideoId, opt => opt.MapFrom(src => src.PostLoginVideo.VideoId));

            CreateMap<CampaignViewModel, CampaignModel>(MemberList.Source)
                .ForSourceMember(src => src.PreLoginVideoId, opt => opt.Ignore())
                .ForSourceMember(src => src.PostLoginVideoId, opt => opt.Ignore())
                .ForSourceMember(src => src.BenefitText, opt => opt.Ignore())
                .ForSourceMember(src => src.StatusText, opt => opt.Ignore())
                .ForSourceMember(src => src.IsSuccess, opt => opt.Ignore());

            CreateMap<ApplicationUser, ProfileViewModel>(MemberList.Source);
        }
    }
}
