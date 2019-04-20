﻿using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Data.Repository.HMS;
using System.Linq;
using System.Web.Mvc;

namespace Outsourcing.Service.HMS
{
    public interface ICategoryHotelService
    {
        IQueryable<HotelCategory> FindAll();
        SelectList FindSelectList(int? id=null);
        HotelCategory FindById(int id);
        void Create(HotelCategory entity);
        void Edit(HotelCategory entity);
        void Delete(int id);
<<<<<<< HEAD
        void Delete(CategoryHotels entity);
        //object GetProductCategories();
=======
        void Delete(HotelCategory entity);
>>>>>>> 8713ac6b396c292f4772054d696193ff9056614f
    }

    public class CategoryHotelService : ICategoryHotelService
    {
        #region Field

        private readonly ICategoryHotelRepository _categoryHotelRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public CategoryHotelService(ICategoryHotelRepository categoryHotelRepository, IUnitOfWork unitOfWork)
        {
            _categoryHotelRepository = categoryHotelRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region BaseMethod

        public IQueryable<HotelCategory> FindAll()
        {
            var listEntities = _categoryHotelRepository.FindBy(w => w.Deleted == false);
            return listEntities;
        }

        public SelectList FindSelectList(int? id)
        {

            var data = _categoryHotelRepository.FindBy(w => w.Deleted == false);
            if (id != null)
            {
                data = data.Where(w => w.Id == id);
            }
            return new SelectList(data, "Id", "Name");
        }


        public HotelCategory FindById(int id)
        {
            var entity = _categoryHotelRepository.FindBy(w => w.Deleted == false & w.Id == id).SingleOrDefault();
            return entity;
        }

        public void Create(HotelCategory entity)
        {
            _categoryHotelRepository.Add(entity);
            Commit();
        }

        public void Edit(HotelCategory entity)
        {
            _categoryHotelRepository.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        private void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(HotelCategory entity)
        {
            if (entity != null)
            {
                entity.Deleted = true;
                Edit(entity);
            }
        }

        #endregion
    }
}