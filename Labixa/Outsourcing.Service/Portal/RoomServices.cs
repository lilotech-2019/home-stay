﻿using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IRoomService : IServiceBase<Room>
    {
        IQueryable<Room> FindByType(RoomType type);
        Room FindByIdAndSlug(int id, string slug);
    }

    public class RoomService : ServiceBase<Room>, IRoomService
    {
        #region Ctor

        public RoomService(IRepository<Room> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        #endregion

        #region BaseMethod

        public IQueryable<Room> FindByType(RoomType type)
        {
            var result = Repository.FindBy(w => w.Deleted == false & w.Type == type);
            return result;
        }

        public Room FindByIdAndSlug(int id, string slug)
        {
            return Repository.FindBy(w => w.Deleted == false & w.Id == id & w.Slug == slug).SingleOrDefault();
        }

        #endregion
    }
}