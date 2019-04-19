using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;

namespace Outsourcing.Service
{
    public interface IStaffService
    {
        IEnumerable<Staff> GetAvailableCategorys();

        IEnumerable<Staff> GetStaffs();
        Staff GetStaffById(int staffId);
        void CreateStaff(Staff staff);
        void EditStaff(Staff staffToEdit);
        void DeleteStaff(int staffId);
        void SaveStaff();

        //Staff GetCategoryByUrlName(string );

    }
    class StaffService : IStaffService
    {
        #region Field
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public StaffService(IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            this._staffRepository = staffRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public IEnumerable<Staff> GetAvailableCategorys()
        {
            var list = _staffRepository.GetAll().Where(p=>p.Deleted==true);
            return list;
        }

        public IEnumerable<Staff> GetStaffs()
        {
            try
            {
                var list = _staffRepository.GetAll().Where(p => p.Deleted == false);
                return list;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public Staff GetStaffById(int staffId)
        {
            var item = _staffRepository.Get(p => p.Id == staffId);
            return item;
        }

        public void CreateStaff(Staff staff)
        {
            if (staff != null)
            {
                _staffRepository.Add(staff);
                SaveStaff();
            }
        }

        public void EditStaff(Staff staffToEdit)
        {
            if (staffToEdit != null)
            {
                _staffRepository.Update(staffToEdit);
                SaveStaff();
            }
        }

        public void DeleteStaff(int staffId)
        {
            var item = _staffRepository.Get(p => p.Id == staffId);
           // staffRepository.Delete(item);
            item.Deleted = true;
            _staffRepository.Update(item);
            SaveStaff();
        }

        public void SaveStaff()
        {
            _unitOfWork.Commit();
        }
    }
}
