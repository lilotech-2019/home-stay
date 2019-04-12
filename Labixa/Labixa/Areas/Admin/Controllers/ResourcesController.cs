﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Labixa.Areas.Admin.ViewModel;
using Labixa.Helpers;

namespace Labixa.Areas.Admin.Controllers
{
    public class ResourcesController : BaseController
    {
        //
        // GET: /Admin/Resources/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewVietNam()
        {
            XmlDocument loResource = new XmlDocument();
            loResource.Load(Server.MapPath("~/Resources.vi.resx"));

            XmlNodeList loRoot = loResource.SelectNodes(
                "root/data");
            if (loRoot != null)
                foreach (XmlNode item in loRoot)
                {
                    ResourcesFormModel obj = new ResourcesFormModel();
                    obj.Name = item.InnerText;
                    obj.Value = item.SelectSingleNode("data/value")?.Value;
                    obj.Value = item.InnerXml;
                }
            return View();
        }

        public ActionResult ViewEnglish()
        {
            List<ResourcesFormModel> listResource = GetListLang("en");
            return View(listResource);
        }

        public List<ResourcesFormModel> GetListLang(string lang)
        {
            #region [Get Language and set Cookies]

            string culture = CultureHelper.GetImplementedCulture(lang);
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture; // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            #endregion

            //ResourceSet resourceSet =
            //  Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            //ResXResourceReader read = new ResXResourceReader(AppDomain.CurrentDomain.BaseDirectory + "Resources.en.resx");
            List<ResourcesFormModel> listResource = new List<ResourcesFormModel>();
            //foreach (DictionaryEntry entry in resourceSet)
            //{
            //    ResourcesFormModel item = new ResourcesFormModel();
            //    item.Name = entry.Key.ToString();
            //    item.Value = entry.Value.ToString();
            //    listResource.Add(item);
            //}
            //resourceSet.Dispose();
            return listResource;
        }

        public ActionResult Create()
        {
            // ResourceSet resourceSet = Resources.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            // ResXResourceWriter listr = new ResXResourceWriter(AppDomain.CurrentDomain.BaseDirectory + "Resources.resx");
            // //ResourceWriter listwriter = new ResourceWriter(AppDomain.CurrentDomain.BaseDirectory + "Resources");
            // foreach (DictionaryEntry k in resourceSet)
            // {
            //     listr.AddMetadata(k.Key.ToString(), k.Value.ToString());
            // }
            //// listr.AddResource("abc", "Truong Long");
            // listr.Generate();
            // listr.Close();
            ClearCookie();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ResourcesFormModel obj)
        {
            string key = obj.Name;
            string value = obj.Value;

            XmlDocument loResource = new XmlDocument();
            loResource.Load(Server.MapPath("~/Resources.vi.resx"));

            XmlNode loRoot = loResource.SelectSingleNode(
                $"root/data[@name='{key}']/value");

            if (loRoot != null)
            {
                loRoot.InnerText = value;
                loResource.Save(Server.MapPath("~/Resources.vi.resx"));
            }
            ClearCookie();
            return RedirectToAction("Index");
        }

        private void ClearCookie()
        {
            #region [Get Language and set Cookies]

            HttpCookie cookie;
            HttpCookie cookie2;

            cookie = new HttpCookie("_culture");
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            cookie2 = new HttpCookie("_culture");
            cookie2.Value = null;
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            #endregion
        }
    }
}