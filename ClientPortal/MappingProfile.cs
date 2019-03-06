using AutoMapper;
using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes.Service.Models;
using Codes.Service.ViewModels;

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
                .ForSourceMember(src => src.PreLoginVideoId, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.PostLoginVideoId, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.BenefitText, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.StatusText, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.IsSuccess, opt => opt.DoNotValidate());

            CreateMap<ApplicationUser, ProfileViewModel>(MemberList.Source);
            CreateMap<ClientModel, ClientEditViewModel>();
            CreateMap<ApplicationUser, MyAccountViewModel>(MemberList.Source);
            CreateMap<ApplicationUser, MyClientsViewModel>(MemberList.Source);
        }
    }
}
