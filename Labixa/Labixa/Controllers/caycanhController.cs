using HtmlAgilityPack;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Labixa.Controllers
{
    public class caycanhController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IWebsiteAttributeService _websiteAttributeService;
        private readonly IStaffService _staffService;
        private readonly INotificationService _notificationService;
        private readonly ITypeNotifyService _typeNotifyService;
        private readonly IProductAttributeMappingService _productAttributeMappingService;
        private readonly IProductCategoryMappingService _productCategoryMappingService;
        private readonly IPictureService _pictureService;
        private readonly IProductPictureMappingService _productPictureMappingService;


        public caycanhController(IProductService productService, IBlogService blogService,
            IWebsiteAttributeService websiteAttributeService, IBlogCategoryService blogCategoryService,
            IStaffService staffService, IProductAttributeMappingService productAttributeMappingService,
            INotificationService _notificationService, ITypeNotifyService _typeNotifyService,
            IProductCategoryMappingService _productCategoryMappingService, IPictureService _pictureService,
            IProductPictureMappingService _productPictureMappingService
        )
        {
            this._productService = productService;
            this._blogService = blogService;
            this._websiteAttributeService = websiteAttributeService;
            this._blogCategoryService = blogCategoryService;
            this._staffService = staffService;
            this._productAttributeMappingService = productAttributeMappingService;
            this._typeNotifyService = _typeNotifyService;
            this._notificationService = _notificationService;
            this._productCategoryMappingService = _productCategoryMappingService;
            this._productPictureMappingService = _productPictureMappingService;
            this._pictureService = _pictureService;
        }

        public ActionResult demo(string url = "1")
        {
            ViewBag.url = Session["url"];
            return View();
        }

        [HttpPost]
        public ActionResult demo(HttpPostedFileBase file)
        {
            string fileName_ = "";
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = fileName_ = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/images/uploaded/"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            Session["url"] = "/images/uploaded/" + fileName_;
            return RedirectToAction("demo", "tinhte");
        }

        //
        // GET: /tinhte/
        public ActionResult Index()
        {
            var url = "https://tinhte.vn/page-";
            List<string> ListProductUrl = new List<string>();
            List<string> PageUrl = new List<string>();
            for (int i = 101; i <= 102; i++)
            {
                PageUrl.Add(url + i + "/");
            }
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            //get list item url
            foreach (var page in PageUrl)
            {
                try
                {
                    web = new HtmlWeb();
                    doc = web.Load(page);
                    Thread.Sleep(1000);
                    foreach (var productUrl in doc.DocumentNode.SelectNodes(
                        "//div[contains(@class,'thread-title') and contains(@class,'width-narrow')]//a"))
                    {
                        if (productUrl.Attributes["href"].Value != null)
                        {
                            ListProductUrl.Add("https://tinhte.vn/" + productUrl.Attributes["href"].Value);
                        }
                    }
                }

                catch (Exception)
                {
                    continue;
                }
            }
            List<Blog> ListBlog = new List<Blog>();
            IEnumerable<string> list = ListProductUrl;
            foreach (var urlProduct in list.Reverse())
            {
                try
                {
                    if (!urlProduct.Contains("threads"))
                    {
                        continue;
                    }
                    web = new HtmlWeb();
                    doc = web.Load(urlProduct);
                    Thread.Sleep(500);
                    Blog item = new Blog();
                    var id = urlProduct.Split('.').LastOrDefault().ToString().Replace("/", null).Trim();
                    var blog = _blogService.GetBlogs().Where(p => p.Id.Equals(id)).FirstOrDefault();
                    if (blog != null)
                    {
                        continue;
                    }
                    item.Title = doc.DocumentNode.SelectSingleNode("//div[@class='bg']//p[@class='title']").InnerText
                        .Trim();
                    item.Slug = StringConvert.ConvertShortName(item.Title);
                    item.BlogCategoryId = 3;
                    item.IsAvailable = false;
                    item.DateCreated = DateTime.Now;
                    item.Deleted = false;
                    item.IsHomePage = true;
                    item.LastEditedTime = DateTime.Now;
                    item.MetaDescription = id;
                    var content = "";

                    if (doc.DocumentNode.SelectSingleNode("//div[@class='entry']//div[@class='share-post']") != null)
                    {
                        doc.DocumentNode.SelectSingleNode("//div[@class='entry']//div[@class='share-post']").Remove();
                    }
                    if (item.BlogImage == null)
                    {
                        foreach (var img in doc.DocumentNode.SelectNodes("//div[contains(@class,'thread-cover')]//img"))
                        {
                            if (img != null)
                            {
                                url = Regex.Match(img.GetAttributeValue("style", ""), @"(?<=url\()(.*)(?=\))").Groups[1]
                                    .Value;
                                if (url == "")
                                {
                                    continue;
                                }
                                img.Attributes["src"].Value = SaveImage(url);
                                if (item.BlogImage != "")
                                {
                                    item.BlogImage = img.Attributes["src"].Value;
                                }
                            }
                        }
                    }
                    //content = doc.DocumentNode.SelectSingleNode("//div[@class='uix_message ']//div[contains(@class, 'messageInfo') and contains(class, 'primaryContent')]//blockquote").InnerHtml;
                    content = doc.DocumentNode.SelectSingleNode("//div[@class='uix_message ']//article//blockquote")
                        .InnerHtml.Trim();
                    content = changeLinkaHref(content, item.Title.Trim());
                    content = removePStrongB(content, item.Title.Trim());
                    item.Content = content.Replace("<!-- .share-post -->", null).Replace("<p>", "<div>")
                        .Replace("</p>", "</div>").Replace("<strong>",
                            "<a href='/tin-tuc/" + StringConvert.ConvertShortName(item.Slug) + "' alt='" + item.Title +
                            "'>")
                        .Replace("</strong>", "</a>").Replace("<b>", null).Replace("</b>", null).Trim();
                    _blogService.CreateBlog(item);
                    ListBlog.Add(item);
                }

                catch (Exception)
                {
                    continue;
                }
            }
            return View(ListBlog);
        }

        public ActionResult cayvahoa()
        {
            var url = "http://cayvahoa.net/cay-canh-trong-nha/page/";
            List<string> ListProductUrl = new List<string>();
            List<string> PageUrl = new List<string>();
            for (int i = 1; i <= 2; i++)
            {
                PageUrl.Add(url + i + "/");
            }
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            //get list item url
            foreach (var page in PageUrl)
            {
                try
                {
                    web = new HtmlWeb();
                    doc = web.Load("https://google.com");
                    Thread.Sleep(2000);
                    foreach (var productUrl in doc.DocumentNode.SelectNodes(
                        "//div[contains(@class,'sp-show') and contains(@class,'box-item-inner')]//a//div[@class='thumbnail']")
                    )
                    {
                        if (productUrl.ParentNode.Attributes["href"].Value != null)
                        {
                            ListProductUrl.Add(productUrl.ParentNode.Attributes["href"].Value);
                        }
                    }
                }

                catch (Exception)
                {
                    throw;
                    //continue;
                }
            }

            foreach (var item in ListProductUrl)
            {
                try
                {
                    web = new HtmlWeb();
                    doc = web.Load(item);
                    Thread.Sleep(1000);
                    var product = new Product();
                    product.Name = doc.DocumentNode
                        .SelectSingleNode("//h1[contains(@class,'product_title') and contains(@class,'entry-title')]")
                        .InnerText;
                    product.Description = doc.DocumentNode.SelectSingleNode("//div[@class='description']").InnerHtml;
                    product.Description = doc.DocumentNode.SelectSingleNode("//div[@class='description']").InnerHtml;
                    product.Content = doc.DocumentNode.SelectSingleNode("//div[@id='tab-description']").InnerHtml;
                    product.DateCreated = DateTime.Now;
                    product.LastEditedTime = DateTime.Now;
                    product.IsHomePage = true;
                    product.IsPublic = true;
                    _productService.CreateProduct(product);
                    product.Slug = StringConvert.ConvertShortName(product.Name + " " + product.Id);
                    _productService.EditProduct(product);
                    var cate = new ProductCategoryMapping
                    {
                        ProductCategoryId = 36,
                        ProductId = product.Id
                    };
                    _productCategoryMappingService.CreateProductCategoryMapping(cate);

                    #region Add image

                    var imageUrl = doc.DocumentNode
                        .SelectSingleNode(
                            "//a[contains(@class,'woocommerce-main-image') and contains(@class,'zoom')]//img")
                        .Attributes["src"].Value;
                    var urlimage = SaveImage(imageUrl);
                    for (int i = 0; i < 5; i++)
                    {
                        var img = new Picture();
                        img.Url = urlimage;
                        img.IsDeleted = false;
                        _pictureService.CreatePicture(img);
                        var picMap = new ProductPictureMapping();
                        picMap.ProductId = product.Id;
                        picMap.PictureId = img.Id;
                        _productPictureMappingService.CreateProductPictureMapping(picMap);
                    }

                    #endregion
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return View();
        }

        public ActionResult sieuthicayxanh()
        {
            var url = "http://sieuthicayxanh.vn/cay-an-qua";
            List<string> ListProductUrl = new List<string>();
            List<string> PageUrl = new List<string>();
            for (int i = 1; i <= 1; i++)
            {
                PageUrl.Add(url);
            }
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            //get list item url
            foreach (var page in PageUrl)
            {
                try
                {
                    web = new HtmlWeb();
                    doc = web.Load(page);
                    Thread.Sleep(2000);
                    foreach (var productUrl in doc.DocumentNode.SelectNodes("//div[@class='thumb']//a"))
                    {
                        if (productUrl.Attributes["href"].Value != null)
                        {
                            ListProductUrl.Add("http://sieuthicayxanh.vn" + productUrl.Attributes["href"].Value);
                        }
                    }
                }

                catch (Exception)
                {
                    throw;
                    //continue;
                }
            }

            foreach (var item in ListProductUrl)
            {
                try
                {
                    web = new HtmlWeb();
                    doc = web.Load(item);
                    Thread.Sleep(1000);
                    var product = new Product();
                    product.Name = doc.DocumentNode.SelectSingleNode("//h1//span[@class='title']").InnerText;
                    product.Description = doc.DocumentNode.SelectSingleNode("//div[@class='summary']//p").InnerHtml;
                    product.Content = doc.DocumentNode.SelectSingleNode("//div[@class='summary']//p").InnerHtml;
                    product.DateCreated = DateTime.Now;
                    product.LastEditedTime = DateTime.Now;
                    product.IsHomePage = true;
                    product.IsPublic = true;
                    _productService.CreateProduct(product);
                    product.Slug = StringConvert.ConvertShortName(product.Name + " " + product.Id);
                    _productService.EditProduct(product);
                    var cate = new ProductCategoryMapping();
                    cate.ProductCategoryId = 42;
                    cate.ProductId = product.Id;
                    _productCategoryMappingService.CreateProductCategoryMapping(cate);

                    #region Add image

                    var imageUrl = "http://sieuthicayxanh.vn" + doc.DocumentNode
                                       .SelectSingleNode("//ul[@id='thumb_img']//li/img").Attributes["src"].Value;
                    var urlimage = SaveImage(imageUrl);
                    for (int i = 0; i < 5; i++)
                    {
                        var img = new Picture();
                        img.Url = urlimage;
                        img.IsDeleted = false;
                        _pictureService.CreatePicture(img);
                        var picMap = new ProductPictureMapping();
                        picMap.ProductId = product.Id;
                        picMap.PictureId = img.Id;
                        _productPictureMappingService.CreateProductPictureMapping(picMap);
                    }

                    #endregion
                }
                catch (Exception)
                {
                    //throw;
                    continue;
                }
            }
            return View();
        }

        public string SaveImage(string url)
        {
            if (url == "")
            {
                return url;
            }
            try
            {
                Stream imageStream = new WebClient().OpenRead(url);
               //Image img = Image.FromStream(imageStream);
                var path = Path.Combine(Server.MapPath("~/images/uploaded/caycanh/"), url.Split('/').LastOrDefault());
               // img.Save(path);
                return "/images/uploaded/caycanh/" + url.Split('/').LastOrDefault();
            }
            catch (Exception)
            {
                return url;
            }
        }

        public string removePStrongB(string content, string name)
        {
            return content.Replace("<p>", "<div>").Replace("</p>", "</div>").Replace("<strong>",
                    "<a href='/tin-tuc/" + StringConvert.ConvertShortName(name) + "' alt='" + name + "'>")
                .Replace("</strong>", "</a>").Replace("<b>", null).Replace("</b>", null);
        }

        public string changeLinkaHref(string content, string name)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(content);
            foreach (var a in doc.DocumentNode.Descendants("a"))
            {
                try
                {
                    if (a != null)
                    {
                        a.Attributes["href"].Value = "/tim-kiem/" + StringConvert.ConvertShortName(a.InnerText.Trim());
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            var newContent = doc.DocumentNode.OuterHtml;
            return newContent;
        }
    }
}