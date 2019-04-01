using Labixa.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Labixa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("culture", "language/{slug}", new { controller = "News", action = "SetCulture", slug = UrlParameter.Optional });
            routes.MapRoute("Room", "danh-sach-phong-ngan-han", new { controller = "RoomVer3", action = "ShortRoom" });
            routes.MapRoute("chitietNganHan", "chi-tiet-phong-ngan-han/{slug}", new { controller = "RoomVer3", action = "DetailShortRoom", slug = UrlParameter.Optional });
            routes.MapRoute("danhsachphongdaihan", "danh-sach-phong-dai-han", new { controller = "RoomVer3", action = "LongRoom"});
            routes.MapRoute("chitietDaihan", "chi-tiet-phong-dai-han/{slug}", new { controller = "RoomVer3", action = "DetailLongRoom" });
            routes.MapRoute("ChiTietPhong", "phong/{slug}", new { controller = "Home", action = "RoomDetail",slug = UrlParameter.Optional });
            routes.MapRoute("TinTuc", "tintuc/{slug}", new { controller = "News", action = "Detail", slug = UrlParameter.Optional });
            routes.MapRoute("DanhSachTinTuc", "danh-sach-tin-tuc", new { controller = "News", action = "Index" });
            routes.MapRoute("ChiTietDichVu", "dichvu/{slug}", new { controller = "Home", action = "DetailService", slug = UrlParameter.Optional });
            routes.MapRoute("ABouUS", "gioi-thieu", new { controller = "Home", action = "About" });
            routes.MapRoute("Contact", "lien-he", new { controller = "Home", action = "Contact" });
            routes.MapRoute("Activities", "hoat-dong", new { controller = "Home", action = "Activities" });
            //routes.MapRoute("RoomSingle", "phong-don", new { controller = "Home", action = "RoomSingle" });
            //routes.MapRoute("RoomCouple", "phong-doi", new { controller = "Home", action = "RoomCouple" });
            //routes.MapRoute("RoomMisstress", "phong-tinh-nhan", new { controller = "Home", action = "RoomMisstress" });
            //routes.MapRoute("Index2", "trang-chu2", new { controller = "Home", action = "Index2" });
            //routes.MapRoute("Index3", "trang-chu3", new { controller = "Home", action = "Index3" });
            routes.MapRoute("Booking", "dat-phong", new { controller = "Home", action = "Booking" });
            routes.MapRoute("Event", "su-kien", new { controller = "News", action = "Event" });
            routes.MapRoute("ActitityNews", "hoat-dong-moi", new { controller = "News", action = "ActitityNews" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );//
        }


    }
}
