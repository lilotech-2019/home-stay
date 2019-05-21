using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Outsourcing.Data.Models;

namespace Outsourcing.Core.Extensions
{
    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<User> users, string selectedId)
        {
            return
                users.OrderBy(user => user.Id)
                    .Select(user =>
                        new SelectListItem
                        {
                            Selected = (user.Id == selectedId),
                            Text = user.DisplayName,
                            Value = user.Id.ToString()
                        });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<HotelCategory> CategoryHotel, int selectedId)
        {
            return
                CategoryHotel.OrderBy(user => user.Id)
                    .Select(user =>
                        new SelectListItem
                        {
                            Selected = (user.Id == selectedId),
                            Text = user.Name,
                            Value = user.Id.ToString()
                        });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<Hotel> Hotels, int selectedId)
        {
            return
                Hotels.OrderBy(user => user.Id)
                    .Select(user =>
                        new SelectListItem
                        {
                            Selected = (user.Id == selectedId),
                            Text = user.Name,
                            Value = user.Id.ToString()
                        });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<Cost> Cost, int selectedId)
        {
            return
                Cost.OrderBy(user => user.Id)
                    .Select(user =>
                        new SelectListItem
                        {
                            Selected = (user.Id == selectedId),
                            Text = user.Name,
                            Value = user.Id.ToString()
                        });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<CostCategory> costCategory, int selectedId)
        {
            return
                costCategory.OrderBy(user => user.Id)
                    .Select(user =>
                        new SelectListItem
                        {
                            Selected = (user.Id == selectedId),
                            Text = user.Name,
                            Value = user.Id.ToString()
                        });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<BlogCategories> blogCategory, int selectedId)
        {
            return
                blogCategory.OrderBy(f => f.CategoryParentId)
                    .Select(f =>
                        new SelectListItem
                        {
                            Selected = (f.Id == selectedId),
                            Text = f.Name,
                            Value = f.Id.ToString()
                        });
        }


        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<Deposit> blogCategory, int selectedId)
        {
            return
                blogCategory.OrderBy(f => f.Id)
                    .Select(f =>
                        new SelectListItem
                        {
                            Selected = (f.Id == selectedId),
                            Text = f.Name,
                            Value = f.Id.ToString()
                        });
        }
    }
}