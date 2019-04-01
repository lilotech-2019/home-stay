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

        readonly IRoomService _RoomService;
        readonly IRoomImageService _RoomImageService;
        readonly IHotelService _HotelService;
        readonly IRoomImageMappingService _RoomImageMappingService;


        #endregion

        #region Ctor
        public RoomController(IRoomService RoomService, IRoomImageService RoomImageService
        , IHotelService HotelService
        , IRoomImageMappingService RoomImageMappingService)
        {
            _RoomService = RoomService;
            _RoomImageService = RoomImageService;
            _HotelService = HotelService;
            _RoomImageMappingService = RoomImageMappingService;
        }
        #endregion
        //
        // GET: /HMSAdmin/Room/
        public ActionResult Index()
        {
            var model = _RoomService.GetRooms();
            return View(model: model);
        }
        public ActionResult Create()
        {
            RoomModel model = new RoomModel();
            model.ListHotels = _HotelService.GetHotels().ToSelectListItems(-1);
            return View(model: model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(RoomModel newRoom, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var Room = new Room
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
                if (String.IsNullOrEmpty(Room.Slug))
                {
                    Room.Slug = StringConvert.ConvertShortName(Room.Name);
                }
                //Create Room
                _RoomService.CreateRoom(Room);
                if (Room.RoomImageMappings==null)
                {
                    Room.RoomImageMappings = new Collection<RoomImageMapping>();
                    for (int i = 0; i < 8; i++)
                    {
                        var newPic = new RoomImage();
                        bool ismain = i == 0;
                        _RoomImageService.CreateRoomImage(newPic);
                        Room.RoomImageMappings.Add(
                            new RoomImageMapping()
                            {
                                RoomImageId = newPic.Id,
                                RoomId = Room.Id,
                                IsMainPicture = ismain,
                                DisplayOrder = 0,
                            });
                    }
                    _RoomService.EditRoom(Room);
                }
                return continueEditing ? RedirectToAction("Edit", "Room", new { RoomId = Room.Id })
                                  : RedirectToAction("Index", "Room");
            }
            else
            {
                newRoom.ListHotels = _HotelService.GetHotels().ToSelectListItems(newRoom.HotelId);
                return View("Create", newRoom);
            }
        }

        [HttpGet]
        public ActionResult Edit(int RoomId)
        {

            var Room = _RoomService.GetRoomById(RoomId);
            //RoomModel RoomFormModel = Mapper.Map<Room, RoomModel>(Room);
            RoomModel RoomFormModel = new RoomModel();
            // RoomFormModel = Mapper.Map<Room, RoomModel>(Room);
            RoomFormModel.Description = Room.Description;
            RoomFormModel.DiscountPercent = Room.DiscountPercent;
            RoomFormModel.DisplayOrder = Room.DisplayOrder;
            RoomFormModel.HotelId = Room.HotelId;
            RoomFormModel.Id = Room.Id;
            RoomFormModel.IsStaticPage = Room.IsStaticPage;
            RoomFormModel.Layout = Room.Layout;
            RoomFormModel.MetaDescription = Room.MetaDescription;
            RoomFormModel.MetaKeywords = Room.MetaKeywords;
            RoomFormModel.MetaTitle = Room.MetaTitle;
            RoomFormModel.Name = Room.Name;
            RoomFormModel.Noted = Room.Noted;
            RoomFormModel.Price = Room.Price;
            RoomFormModel.RoomImageMappings = Room.RoomImageMappings;
            RoomFormModel.SharePercent = Room.SharePercent;
            RoomFormModel.Slug = Room.Slug;
            RoomFormModel.Status = Room.Status;
            RoomFormModel.string1 = Room.string1;
            RoomFormModel.Utility_DryHair = Room.Utility_DryHair;
            RoomFormModel.Utility_HotWater = Room.Utility_HotWater;
            RoomFormModel.Utility_Iron = Room.Utility_Iron;
            RoomFormModel.Utility_Kitchen = Room.Utility_Kitchen;
            RoomFormModel.Utility_Snack = Room.Utility_Snack;
            RoomFormModel.Utility_Snack = Room.Utility_Snack;
            RoomFormModel.Utility_DryHair = Room.Utility_DryHair;
            RoomFormModel.Utility_TeaCoffee = Room.Utility_TeaCoffee;
            RoomFormModel.Utility_Tivi = Room.Utility_Tivi;
            RoomFormModel.Utility_TuDo = Room.Utility_TuDo;
            RoomFormModel.Utility_WashMachine = Room.Utility_WashMachine;
            RoomFormModel.ListHotels = _HotelService.GetHotels().ToSelectListItems(RoomFormModel.HotelId);
            return View(model: RoomFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(RoomModel RoomToEdit, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var Room = _RoomService.GetRoomById(RoomToEdit.Id);
                //Mapping to domain
                //Mapping to domain
                Room.Description = RoomToEdit.Description;
                Room.DiscountPercent = RoomToEdit.DiscountPercent;
                Room.DisplayOrder = RoomToEdit.DisplayOrder;
                Room.HotelId = RoomToEdit.HotelId;
                Room.Id = RoomToEdit.Id;
                Room.IsStaticPage = RoomToEdit.IsStaticPage;
                Room.Layout = RoomToEdit.Layout;
                Room.MetaDescription = RoomToEdit.MetaDescription;
                Room.MetaKeywords = RoomToEdit.MetaKeywords;
                Room.MetaTitle = RoomToEdit.MetaTitle;
                Room.Name = RoomToEdit.Name;
                Room.Noted = RoomToEdit.Noted;
                Room.Price = RoomToEdit.Price;
                foreach (var item in RoomToEdit.RoomImageMappings)
                {
                    _RoomImageMappingService.EditRoomImageMapping(item);
                    _RoomImageService.EditRoomImage(item.RoomImage);
                }
                Room.SharePercent = RoomToEdit.SharePercent;
                Room.Slug = RoomToEdit.Slug;
                Room.Status = RoomToEdit.Status;
                Room.string1 = RoomToEdit.string1;
                Room.Utility_DryHair = RoomToEdit.Utility_DryHair;
                Room.Utility_HotWater = RoomToEdit.Utility_HotWater;
                Room.Utility_Iron = RoomToEdit.Utility_Iron;
                Room.Utility_Kitchen = RoomToEdit.Utility_Kitchen;
                Room.Utility_Snack = RoomToEdit.Utility_Snack;
                Room.Utility_Snack = RoomToEdit.Utility_Snack;
                Room.Utility_DryHair = RoomToEdit.Utility_DryHair;
                Room.Utility_TeaCoffee = RoomToEdit.Utility_TeaCoffee;
                Room.Utility_Tivi = RoomToEdit.Utility_Tivi;
                Room.Utility_TuDo = RoomToEdit.Utility_TuDo;
                Room.Utility_WashMachine = RoomToEdit.Utility_WashMachine;

                Room.Slug = StringConvert.ConvertShortName(Room.Name);
                _RoomService.EditRoom(Room);
                return continueEditing ? RedirectToAction("Edit", "Room", new { RoomId = Room.Id })
                                 : RedirectToAction("Index", "Room");
            }
            else
            {
                RoomToEdit.ListHotels = _HotelService.GetHotels().ToSelectListItems(RoomToEdit.HotelId);
                return View("Edit", RoomToEdit);
            }
        }





        public ActionResult Delete(int RoomId)
        {
            _RoomService.DeleteRoom(RoomId);
            return RedirectToAction("Index");
        }
    }
}