using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Labixa.Areas.Admin.ViewModel.WebsiteAtribute;
using Outsourcing.Data.Models;

namespace Labixa.Areas.Admin.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappings";

        public ViewModelToDomainMappingProfile()
        {
            Configure();
        }

        protected void Configure()
        {
            //Mapper.CreateMap<UserFormModel, User>();
            //Mapper.CreateMap<UserFormViewModel, User>().ForMember(x => x.Id, opt => opt.MapFrom(source => source.UserId));
            //Mapper.CreateMap<XViewModel, X()
            //    .ForMember(x => x.PropertyXYZ, opt => opt.MapFrom(source => source.Property1));     
            CreateMap<BlogFormModel, Blogs>();
            CreateMap<ProductFormModel, Product>();
            CreateMap<ProductAttributeFormModel, ProductAttribute>();
            CreateMap<OrderFormModel, Order>();
            CreateMap<WebsiteAttributeFormModel, WebsiteAttributes>();

            //LongT
            CreateMap<BlogCategoryFormModel, BlogCategories>();
            CreateMap<StaffFormModel, Staff>();
            CreateMap<ProductCategoryFormModel, ProductCategory>();
            CreateMap<AlbumPhotoFormModel, Product>();
        }
    }
}