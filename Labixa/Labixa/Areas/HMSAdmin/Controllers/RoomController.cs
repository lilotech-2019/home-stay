using System;
using System.Web.Mvc;
using Outsourcing.Service.HMS;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Core.Common;
using Outsourcing.Core.Extensions;
using Outsourcing.Core.Framework.Controllers;
using Labixa.Areas.HMSAdmin.ViewModels;
using System.Collections.ObjectModel;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class RoomController : BaseController
    {
        #region Field

        readonly IRoomService _roomService;
        readonly IRoomImageService _roomImageService;
        readonly IHotelService _hotelService;
        readonly IRoomImageMappingService _roomImageMappingService;


        #endregion

        #region Ctor
        public RoomController(IRoomService roomService, IRoomImageService roomImageService
        , IHotelService hotelService
        , IRoomImageMappingService roomImageMappingService)
        {
            _roomService = roomService;
            _roomImageService = roomImageService;
            _hotelService = hotelService;
            _roomImageMappingService = roomImageMappingService;
        }
        #endregion
        //
        // GET: /HMSAdmin/Room/
        public ActionResult Index()
        {
            var model = _roomService.GetRooms();
            return View(model);
        }
        public ActionResult Create()
        {
            RoomModel model = new RoomModel();
            model.ListHotels = _hotelService.GetHotels().ToSelectListItems(-1);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(RoomModel newRoom, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var room = new Room
                {
                    //Mapping to domain
                    //Room = (Room)Mapper.Map<RoomModel, Room>(newRoom);
                    MetaDescription = newRoom.MetaDescription,
                    MetaKeywords = newRoom.MetaKeywords,
                    MetaTitle = newRoom.MetaTitle,
                    Name = newRoom.Name,
                    SharePercent = newRoom.SharePercent,
                    Status = false,
                    Description = newRoom.Description,
                    DiscountPercent = newRoom.DiscountPercent,
                    DisplayOrder = newRoom.DisplayOrder,
                    HotelId = newRoom.HotelId,
                    IsStaticPage = false,
                    Layout = 0,
                    Price = newRoom.Price,
                    string1 = newRoom.string1,
                    Utility_DryHair = newRoom.Utility_DryHair,
                    Utility_Iron = newRoom.Utility_Iron,
                    Utility_HotWater = newRoom.Utility_HotWater,
                    Utility_Kitchen = newRoom.Utility_Kitchen,
                    Utility_Snack = newRoom.Utility_Snack,
                    Utility_TeaCoffee = newRoom.Utility_TeaCoffee,
                    Utility_Tivi = newRoom.Utility_Tivi,
                    Utility_TuDo = newRoom.Utility_TuDo,
                    Utility_WashMachine = newRoom.Utility_WashMachine
                };
                if (String.IsNullOrEmpty(room.Slug))
                {
                    room.Slug = StringConvert.ConvertShortName(room.Name);
                }
                //Create Room
                _roomService.CreateRoom(room);
                if (room.RoomImageMappings==null)
                {
                    room.RoomImageMappings = new Collection<RoomImageMapping>();
                    for (int i = 0; i < 8; i++)
                    {
                        var newPic = new RoomImage();
                        bool ismain = i == 0;
                        _roomImageService.CreateRoomImage(newPic);
                        room.RoomImageMappings.Add(
                            new RoomImageMapping()
                            {
                                RoomImageId = newPic.Id,
                                RoomId = room.Id,
                                IsMainPicture = ismain,
                                DisplayOrder = 0,
                            });
                    }
                    _roomService.EditRoom(room);
                }
                return continueEditing ? RedirectToAction("Edit", "Room", new { RoomId = room.Id })
                                  : RedirectToAction("Index", "Room");
            }
            else
            {
                newRoom.ListHotels = _hotelService.GetHotels().ToSelectListItems(newRoom.HotelId);
                return View("Create", newRoom);
            }
        }

        [HttpGet]
        public ActionResult Edit(int roomId)
        {

            var room = _roomService.GetRoomById(roomId);
            //RoomModel RoomFormModel = Mapper.Map<Room, RoomModel>(Room);
            RoomModel roomFormModel = new RoomModel();
            // RoomFormModel = Mapper.Map<Room, RoomModel>(Room);
            roomFormModel.Description = room.Description;
            roomFormModel.DiscountPercent = room.DiscountPercent;
            roomFormModel.DisplayOrder = room.DisplayOrder;
            roomFormModel.HotelId = room.HotelId;
            roomFormModel.Id = room.Id;
            roomFormModel.IsStaticPage = room.IsStaticPage;
            roomFormModel.Layout = room.Layout;
            roomFormModel.MetaDescription = room.MetaDescription;
            roomFormModel.MetaKeywords = room.MetaKeywords;
            roomFormModel.MetaTitle = room.MetaTitle;
            roomFormModel.Name = room.Name;
            roomFormModel.Noted = room.Noted;
            roomFormModel.Price = room.Price;
            roomFormModel.RoomImageMappings = room.RoomImageMappings;
            roomFormModel.SharePercent = room.SharePercent;
            roomFormModel.Slug = room.Slug;
            roomFormModel.Status = room.Status;
            roomFormModel.string1 = room.string1;
            roomFormModel.Utility_DryHair = room.Utility_DryHair;
            roomFormModel.Utility_HotWater = room.Utility_HotWater;
            roomFormModel.Utility_Iron = room.Utility_Iron;
            roomFormModel.Utility_Kitchen = room.Utility_Kitchen;
            roomFormModel.Utility_Snack = room.Utility_Snack;
            roomFormModel.Utility_Snack = room.Utility_Snack;
            roomFormModel.Utility_DryHair = room.Utility_DryHair;
            roomFormModel.Utility_TeaCoffee = room.Utility_TeaCoffee;
            roomFormModel.Utility_Tivi = room.Utility_Tivi;
            roomFormModel.Utility_TuDo = room.Utility_TuDo;
            roomFormModel.Utility_WashMachine = room.Utility_WashMachine;
            roomFormModel.ListHotels = _hotelService.GetHotels().ToSelectListItems(roomFormModel.HotelId);
            return View(roomFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(RoomModel roomToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var room = _roomService.GetRoomById(roomToEdit.Id);
                //Mapping to domain
                //Mapping to domain
                room.Description = roomToEdit.Description;
                room.DiscountPercent = roomToEdit.DiscountPercent;
                room.DisplayOrder = roomToEdit.DisplayOrder;
                room.HotelId = roomToEdit.HotelId;
                room.Id = roomToEdit.Id;
                room.IsStaticPage = roomToEdit.IsStaticPage;
                room.Layout = roomToEdit.Layout;
                room.MetaDescription = roomToEdit.MetaDescription;
                room.MetaKeywords = roomToEdit.MetaKeywords;
                room.MetaTitle = roomToEdit.MetaTitle;
                room.Name = roomToEdit.Name;
                room.Noted = roomToEdit.Noted;
                room.Price = roomToEdit.Price;
                foreach (var item in roomToEdit.RoomImageMappings)
                {
                    _roomImageMappingService.EditRoomImageMapping(item);
                    _roomImageService.EditRoomImage(item.RoomImage);
                }
                room.SharePercent = roomToEdit.SharePercent;
                room.Slug = roomToEdit.Slug;
                room.Status = roomToEdit.Status;
                room.string1 = roomToEdit.string1;
                room.Utility_DryHair = roomToEdit.Utility_DryHair;
                room.Utility_HotWater = roomToEdit.Utility_HotWater;
                room.Utility_Iron = roomToEdit.Utility_Iron;
                room.Utility_Kitchen = roomToEdit.Utility_Kitchen;
                room.Utility_Snack = roomToEdit.Utility_Snack;
                room.Utility_Snack = roomToEdit.Utility_Snack;
                room.Utility_DryHair = roomToEdit.Utility_DryHair;
                room.Utility_TeaCoffee = roomToEdit.Utility_TeaCoffee;
                room.Utility_Tivi = roomToEdit.Utility_Tivi;
                room.Utility_TuDo = roomToEdit.Utility_TuDo;
                room.Utility_WashMachine = roomToEdit.Utility_WashMachine;

                room.Slug = StringConvert.ConvertShortName(room.Name);
                _roomService.EditRoom(room);
                return continueEditing ? RedirectToAction("Edit", "Room", new { RoomId = room.Id })
                                 : RedirectToAction("Index", "Room");
            }
            else
            {
                roomToEdit.ListHotels = _hotelService.GetHotels().ToSelectListItems(roomToEdit.HotelId);
                return View("Edit", roomToEdit);
            }
        }





        public ActionResult Delete(int roomId)
        {
            _roomService.DeleteRoom(roomId);
            return RedirectToAction("Index");
        }
    }
}