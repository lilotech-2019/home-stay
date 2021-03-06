﻿using System.Collections.Generic;
using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IRoomAssetService : IServiceBase<RoomAsset>
    {
        //IEnumerable<RoomAsset> FindByRoomIdAndAssetId(int roomId, int assetId);
        //IEnumerable<RoomAsset> FindByRoomId(int roomId);
    }

    public class RoomRoomAssetService : ServiceBase<RoomAsset>, IRoomAssetService
    {
        public RoomRoomAssetService(IRepository<RoomAsset> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }

        //public IEnumerable<RoomAsset> FindByRoomIdAndAssetId(int roomId, int assetId)
        //{
        //    return Repository.FindBy(w => w.AssetId == assetId && w.RoomId == roomId && w.IsAvaiable).ToList();
        //}

        //public IEnumerable<RoomAsset> FindByRoomId(int roomId)
        //{
        //    return Repository.FindBy(w => w.RoomId == roomId && w.IsAvaiable).ToList();
        //}
    }
}