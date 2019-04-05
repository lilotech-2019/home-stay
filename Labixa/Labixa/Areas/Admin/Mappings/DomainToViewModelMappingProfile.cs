using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Labixa.Areas.Admin.ViewModel.WebsiteAtribute;
using Outsourcing.Data.Models;

namespace Labixa.Areas.Admin.Mappings
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

            CreateMap<Blogs, BlogFormModel>();
            CreateMap<Product, ProductFormModel>();
            CreateMap<ProductAttribute, ProductAttributeFormModel>();
            CreateMap<Order, OrderFormModel>();
            CreateMap<WebsiteAttributes, WebsiteAttributeFormModel>();

            //LongT
            CreateMap<BlogCategories, BlogCategoryFormModel>();
            CreateMap<Staff, StaffFormModel>();
            CreateMap<ProductCategory, ProductCategoryFormModel>();
            CreateMap<Product, AlbumPhotoFormModel>();
        }
    }
}