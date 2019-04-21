using AutoMapper;
using Labixa.Areas.HMSAdmin.ViewModels;
using Outsourcing.Data.Models;
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

        private void Configure()
        {
            CreateMap<HotelModel, Hotel>();
            CreateMap<Room, RoomModel>();
        }
    }
}