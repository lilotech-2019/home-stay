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
        IEnumerable<WebsiteAtribute> GetWebsiteAttributeByType(string type);
        IEnumerable<WebsiteAtribute> GetWebsiteAttributes();
        WebsiteAtribute GetWebsiteAttributeById(int websiteAttributeId);
        WebsiteAtribute GetWebsiteAttributeByName(string name);

        void CreateWebsiteAttribute(WebsiteAtribute websiteAtribute);
        void EditWebsiteAttribute(WebsiteAtribute websiteAtributeToEdit);
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

        public IEnumerable<WebsiteAtribute> GetAvailableCategorys()
        {
            var list = _websiteAttributeRepository.GetAll().Where(p=>p.Deleted==true);
            return list;
        }

        public IEnumerable<WebsiteAtribute> GetWebsiteAttributes()
        {
            var list = _websiteAttributeRepository.GetAll().Where(p => p.Deleted == false);
            return list;
        }

        public WebsiteAtribute GetWebsiteAttributeById(int websiteAttributeId)
        {
            var item = _websiteAttributeRepository.Get(p => p.Id == websiteAttributeId);
            return item;
        }
        public WebsiteAtribute GetWebsiteAttributeByName(string  name)
        {
            var item = _websiteAttributeRepository.Get(p => p.Name == name);
            return item;
        }

        public void CreateWebsiteAttribute(WebsiteAtribute websiteAtribute)
        {
            if (websiteAtribute != null)
            {
                _websiteAttributeRepository.Add(websiteAtribute);
                SaveWebsiteAttribute();
            }
        }

        public void EditWebsiteAttribute(WebsiteAtribute websiteAtributeToEdit)
        {
            if (websiteAtributeToEdit != null)
            {
                _websiteAttributeRepository.Update(websiteAtributeToEdit);
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

        public IEnumerable<WebsiteAtribute> GetWebsiteAttributeByType(string type)
        {
            return _websiteAttributeRepository.GetAll().Where(p => p.Type.ToLower().Equals(type.ToLower()));
        }
    }
}
