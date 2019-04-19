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
    public interface IWebsiteAttributeService
    {
        IEnumerable<WebsiteAttributes> GetWebsiteAttributeByType(string type);
        IEnumerable<WebsiteAttributes> GetWebsiteAttributes();
        WebsiteAttributes GetWebsiteAttributeById(int websiteAttributeId);
        WebsiteAttributes GetWebsiteAttributeByName(string name);

        void CreateWebsiteAttribute(WebsiteAttributes websiteAttribute);
        void EditWebsiteAttribute(WebsiteAttributes websiteAttributeToEdit);
        void DeleteWebsiteAttribute(int websiteAttributeId);
        void SaveWebsiteAttribute();

        //WebsiteAttribute GetCategoryByUrlName(string );

    }
    class WebsiteAttributeService : IWebsiteAttributeService
    {
        #region Field
        private readonly IWebsiteAttributeRepository _websiteAttributeRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public WebsiteAttributeService(IWebsiteAttributeRepository websiteAttributeRepository, IUnitOfWork unitOfWork)
        {
            this._websiteAttributeRepository = websiteAttributeRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public IEnumerable<WebsiteAttributes> GetAvailableCategorys()
        {
            var list = _websiteAttributeRepository.GetAll().Where(p=>p.Deleted==true);
            return list;
        }

        public IEnumerable<WebsiteAttributes> GetWebsiteAttributes()
        {
            var list = _websiteAttributeRepository.GetAll().Where(p => p.Deleted == false);
            return list;
        }

        public WebsiteAttributes GetWebsiteAttributeById(int websiteAttributeId)
        {
            var item = _websiteAttributeRepository.Get(p => p.Id == websiteAttributeId);
            return item;
        }
        public WebsiteAttributes GetWebsiteAttributeByName(string  name)
        {
            var item = _websiteAttributeRepository.Get(p => p.Name == name);
            return item;
        }

        public void CreateWebsiteAttribute(WebsiteAttributes websiteAttribute)
        {
            if (websiteAttribute != null)
            {
                _websiteAttributeRepository.Add(websiteAttribute);
                SaveWebsiteAttribute();
            }
        }

        public void EditWebsiteAttribute(WebsiteAttributes websiteAttributeToEdit)
        {
            if (websiteAttributeToEdit != null)
            {
                _websiteAttributeRepository.Update(websiteAttributeToEdit);
                SaveWebsiteAttribute();
            }
        }

        public void DeleteWebsiteAttribute(int websiteAttributeId)
        {
            var item = _websiteAttributeRepository.Get(p => p.Id == websiteAttributeId);
           // websiteAttributeRepository.Delete(item);
            item.Deleted = true;
            _websiteAttributeRepository.Update(item);
            SaveWebsiteAttribute();
        }

        public void SaveWebsiteAttribute()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<WebsiteAttributes> GetWebsiteAttributeByType(string type)
        {
            return _websiteAttributeRepository.GetAll().Where(p => p.Type.ToLower().Equals(type.ToLower()));
        }
    }
}
