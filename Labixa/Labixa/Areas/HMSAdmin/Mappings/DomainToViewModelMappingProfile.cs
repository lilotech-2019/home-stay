using AutoMapper;
using Labixa.Areas.HMSAdmin.ViewModels;
using Outsourcing.Data.Models.HMS;

namespace Labixa.Areas.HMSAdmin.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            Configure();
        }
        public override string ProfileName => "DomainToViewModelMappings";

        protected void Configure()
        {

            //Mapper.CreateMap<UserProfile, UserProfileFormModel>();

            //Mapper.CreateMap<X, XViewModel>()
            //    .ForMember(x => x.Property1, opt => opt.MapFrom(source => source.PropertyXYZ));
            //Mapper.CreateMap<Goal, GoalListViewModel>().ForMember(x => x.SupportsCount, opt => opt.MapFrom(source => source.Supports.Count))
            //                                          .ForMember(x => x.UserName, opt => opt.MapFrom(source => source.User.UserName))
            //                                          .ForMember(x => x.StartDate, opt => opt.MapFrom(source => source.StartDate.ToString("dd MMM yyyy")))
            //                                          .ForMember(x => x.EndDate, opt => opt.MapFrom(source => source.EndDate.ToString("dd MMM yyyy")));
            //Mapper.CreateMap<Group, GroupsItemViewModel>().ForMember(x => x.CreatedDate, opt => opt.MapFrom(source => source.CreatedDate.ToString("dd MMM yyyy")));

            //Mapper.CreateMap<IPagedList<Group>, IPagedList<GroupsItemViewModel>>().ConvertUsing<PagedListConverter<Group, GroupsItemViewModel>>();
            CreateMap<HotelModel, Hotel>();
            CreateMap<Room, RoomModel>();

            //Mapper.CreateMap<Hotel, HotelModel>().ForMember(x => x.Address, opt => opt.MapFrom(source => source.Address))
            //                                          .ForMember(x => x.CategoryHotelId, opt => opt.MapFrom(source => source.CategoryHotelId))
            //                                          .ForMember(x => x.ContractDate, opt => opt.MapFrom(source => source.ContractDate))
            //                                          .ForMember(x => x.ContractExpire, opt => opt.MapFrom(source => source.ContractExpire))
            //                                          .ForMember(x => x.ContractNumber, opt => opt.MapFrom(source => source.ContractNumber))
            //                                          .ForMember(x => x.DateCreated, opt => opt.MapFrom(source => source.DateCreated))
            //                                          .ForMember(x => x.Description, opt => opt.MapFrom(source => source.Description))
            //                                          .ForMember(x => x.HostAddress, opt => opt.MapFrom(source => source.HostAddress))
            //                                          .ForMember(x => x.HostEmail, opt => opt.MapFrom(source => source.HostEmail))
            //                                          .ForMember(x => x.HostName, opt => opt.MapFrom(source => source.HostName))
            //                                          .ForMember(x => x.HostPhone, opt => opt.MapFrom(source => source.HostPhone))
            //                                          .ForMember(x => x.LastEditedTime, opt => opt.MapFrom(source => source.LastEditedTime))
            //                                          .ForMember(x => x.MetaDescription, opt => opt.MapFrom(source => source.MetaDescription))
            //                                          .ForMember(x => x.MetaKeywords, opt => opt.MapFrom(source => source.MetaKeywords))
            //                                          .ForMember(x => x.MetaTitle, opt => opt.MapFrom(source => source.MetaTitle))
            //                                          .ForMember(x => x.Name, opt => opt.MapFrom(source => source.Name))
            //                                          .ForMember(x => x.SharePercent, opt => opt.MapFrom(source => source.SharePercent))
            //                                          .ForMember(x => x.Status, opt => opt.MapFrom(source => source.Status))
            //                                          .ForMember(x => x.UrlImage1, opt => opt.MapFrom(source => source.UrlImage1))
            //                                          .ForMember(x => x.UrlImage2, opt => opt.MapFrom(source => source.UrlImage2))
            //                                          .ForMember(x => x.UrlImage3, opt => opt.MapFrom(source => source.UrlImage3));

        }
    }
}