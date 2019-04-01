using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Service.Properties;
using Outsourcing.Data.Repository.HMS;

namespace Outsourcing.Service.HMS
{
    public interface IHotelService
    {

        IEnumerable<Hotel> GetHotels();
        Hotel GetHotelContact();
        IEnumerable<Hotel> GetHomePageHotels();
        IEnumerable<Hotel> GetHotelByCategorySlug(string slug);
        IEnumerable<Hotel> GetHotelByCategoryId(int id);
        IEnumerable<Hotel> Get6HotelService();
        IEnumerable<Hotel> Get2HotelNews();
        IEnumerable<Hotel> Get3HotelNewsNewest();
        Hotel GetHotelById(int HotelId);
        void CreateHotel(Hotel Hotel);
        void EditHotel(Hotel HotelToEdit);
        void DeleteHotel(int HotelId);
        void SaveHotel();
        IEnumerable<ValidationResult> CanAddHotel(string HotelUrl);

        Hotel GetHotelByUrlName(string urlName);

        IEnumerable<Hotel> GetHotelsByCategory(int HotelTypeId);

        IEnumerable<Hotel> GetStaticPage();
        IEnumerable<Hotel> GetNewPost();
    }
    public class HotelService : IHotelService
    {
        #region Field
        private readonly IHotelRepository HotelRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public HotelService(IHotelRepository HotelRepository, IUnitOfWork unitOfWork)
        {
            this.HotelRepository = HotelRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Implementation for IHotelService
        public IEnumerable<Hotel> GetHotels()
        {
            var Hotels = HotelRepository.GetMany(b =>!b.Deleted).OrderBy(b => b.Position);
            return Hotels;
        }
        public IEnumerable<Hotel> Get3HotelsPosition()
        {
            var Hotels = HotelRepository.GetMany(b =>!b.Deleted).OrderBy(b => b.Position).Take(3);
            return Hotels;
        }
        public IEnumerable<Hotel> GetHomePageHotels()
        {
            //var Hotels = HotelRepository.
            //    GetMany(b => !b.HotelCategory.IsStaticPage && !b.Deleted && b.IsHomePage).
            //    OrderByDescending(b => b.DateCreated);
            //return Hotels;
            return null;
        }
        public IEnumerable<Hotel> GetHotelByCategoryId(int id)
        {
            //var Hotels = HotelRepository.GetMany(b => !b.HotelCategory.IsStaticPage
            //    && b.HotelCategory.Id.Equals(id)
            //    && !b.Deleted).
            //    OrderByDescending(b => b.DateCreated);
            //return Hotels;
            return null;

        }
        public IEnumerable<Hotel> GetHotelByCategorySlug(string slug)
        {
            //    var Hotels = HotelRepository.GetMany(b => !b.HotelCategory.IsStaticPage
            //        && b.HotelCategory.Slug.Equals(slug)
            //        && !b.Deleted).
            //        OrderByDescending(b => b.DateCreated);
            //    return Hotels;
            return null;

        }
        public IEnumerable<Hotel> GetStaticPage()
        {
            var Hotels = HotelRepository.GetMany(b =>!b.Deleted).OrderByDescending(b => b.DateCreated);
            return Hotels;
        }

        public Hotel GetHotelById(int HotelId)
        {
            var Hotel = HotelRepository.GetById(HotelId);
            return Hotel;
        }

        public void CreateHotel(Hotel Hotel)
        {
            HotelRepository.Add(Hotel);
            SaveHotel();
        }

        public void EditHotel(Hotel HotelToEdit)
        {
            HotelToEdit.LastEditedTime = DateTime.Now;
            HotelRepository.Update(HotelToEdit);
            SaveHotel();
        }

        public void DeleteHotel(int HotelId)
        {
            //Get Hotel by id.
            var Hotel = HotelRepository.GetById(HotelId);
            if (Hotel != null)
            {
                Hotel.Deleted = true;
                HotelRepository.Update(Hotel);
                SaveHotel();
            }
        }

        public void SaveHotel()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddHotel(string slug)
        {
            ////Get Hotel by url.
            //var Hotel = HotelRepository.Get(b => b.Slug.Equals(slug));
            ////Check if slug is exist
            //if (Hotel != null)
            //{
            //    yield return new ValidationResult("Hotel", Resources.HotelExist);
            //}
            return null;
        }

        public Hotel GetHotelByUrlName(string urlName)
        {
            var Hotel = HotelRepository.Get(b => b.Slug == urlName);
            return Hotel;
        }

        public IEnumerable<Hotel> GetHotelsByCategory(int HotelTypeId)
        {
            var Hotels = this.GetHotels().Where(b => b.CategoryHotelId == HotelTypeId);
            return Hotels;
        }

        public IEnumerable<Hotel> Get6HotelService()
        {
            var Hotels = this.GetHotels().Where(p => p.CategoryHotelId == 6).Take(6);
            return Hotels;
        }
        public IEnumerable<Hotel> Get2HotelNews()
        {
            var Hotels = this.GetHotels().Where(p => p.CategoryHotelId == 3).OrderBy(p => p.Position).Take(2);
            return Hotels;
        }
        public IEnumerable<Hotel> Get3HotelNewsNewest()
        {
            var Hotels = this.GetHotels().Where(p => p.CategoryHotelId == 3).OrderBy(p => p.Position).Take(3);
            return Hotels;
        }
        #endregion


        public Hotel GetHotelContact()
        {
            var item = HotelRepository.Get(p => p.Slug.Equals("lien-he"));
            return item;
        }


        public IEnumerable<Hotel> GetNewPost()
        {
            return HotelRepository.GetAll().Where(p => p.CategoryHotelId == 3).OrderByDescending(p => p.DateCreated).Take(5);
        }
    }
}
