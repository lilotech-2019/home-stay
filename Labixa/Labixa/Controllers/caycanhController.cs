using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Controllers
{
    public class CaycanhController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBlogService _blogService;
        private readonly IProductCategoryMappingService _productCategoryMappingService;
        private readonly IPictureService _pictureService;
        private readonly IProductPictureMappingService _productPictureMappingService;
        
        public CaycanhController(IProductService productService, IBlogService blogService,
            IWebsiteAttributeService websiteAttributeService, IBlogCategoryService blogCategoryService,
            IStaffService staffService, IProductAttributeMappingService productAttributeMappingService,
            INotificationService notificationService, ITypeNotifyService typeNotifyService,
            IProductCategoryMappingService productCategoryMappingService, IPictureService pictureService,
            IProductPictureMappingService productPictureMappingService
        )
        {
            _productService = productService;
            _blogService = blogService;

            _productCategoryMappingService = productCategoryMappingService;
            _productPictureMappingService = productPictureMappingService;
            _pictureService = pictureService;
        }

        public ActionResult Demo(string url = "1")
        {
            ViewBag.url = Session["url"];
            return View();
        }

        [HttpPost]
        public ActionResult Demo(HttpPostedFileBase file)
        {
            string fileName = "";
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/images/uploaded/"),
                    fileName ?? throw new InvalidOperationException());
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            Session["url"] = "/images/uploaded/" + fileName;
            return RedirectToAction("demo", "tinhte");
        }

        //
        // GET: /tinhte/
        public ActionResult Index()
        {
            var url = "https://tinhte.vn/page-";
            List<string> listProductUrl = new List<string>();
            List<string> pageUrl = new List<string>();
            for (int i = 101; i <= 102; i++)
            {
                pageUrl.Add(url + i + "/");
            }
            HtmlWeb web;
            HtmlDocument doc;
            //get list item url
            foreach (var page in pageUrl)
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
                            listProductUrl.Add("https://tinhte.vn/" + productUrl.Attributes["href"].Value);
                        }
                    }
                }

                catch (Exception)
                {
                    // ignored
                }
            }
            List<Blogs> listBlog = new List<Blogs>();
            IEnumerable<string> list = listProductUrl;
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
                    var item = new Blogs();
                    var id = urlProduct.Split('.').LastOrDefault().Replace("/", null).Trim();
                    var blog = _blogService.GetBlogs().FirstOrDefault(p => p.Id == int.Parse(id));
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

                    if (doc.DocumentNode.SelectSingleNode("//div[@class='entry']//div[@class='share-post']") != null)
                    {
                        doc.DocumentNode.SelectSingleNode("//div[@class='entry']//div[@class='share-post']").Remove();
                    }
                    if (item.BlogImage == null)
                    {
                        foreach (var img in doc.DocumentNode.SelectNodes("//div[contains(@class,'thread-cover')]//img"))
                        {
                            if (img == null) continue;
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
                    //content = doc.DocumentNode.SelectSingleNode("//div[@class='uix_message ']//div[contains(@class, 'messageInfo') and contains(class, 'primaryContent')]//blockquote").InnerHtml;
                    var content = doc.DocumentNode.SelectSingleNode("//div[@class='uix_message ']//article//blockquote")
                        .InnerHtml.Trim();
                    content = ChangeLinkaHref(content, item.Title.Trim());
                    content = RemovePStrongB(content, item.Title.Trim());
                    item.Content = content.Replace("<!-- .share-post -->", null).Replace("<p>", "<div>")
                        .Replace("</p>", "</div>").Replace("<strong>",
                            "<a href='/tin-tuc/" + StringConvert.ConvertShortName(item.Slug) + "' alt='" + item.Title +
                            "'>")
                        .Replace("</strong>", "</a>").Replace("<b>", null).Replace("</b>", null).Trim();
                    _blogService.Create(item);
                    listBlog.Add(item);
                }

                catch (Exception)
                {
                    // ignored
                }
            }
            return View(listBlog);
        }

        public ActionResult Cayvahoa()
        {
            var url = "http://cayvahoa.net/cay-canh-trong-nha/page/";
            List<string> listProductUrl = new List<string>();
            List<string> pageUrl = new List<string>();
            for (int i = 1; i <= 2; i++)
            {
                pageUrl.Add(url + i + "/");
            }
            HtmlWeb web;
            HtmlDocument doc;
            //get list item url
            foreach (var page in pageUrl)
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
                            listProductUrl.Add(productUrl.ParentNode.Attributes["href"].Value);
                        }
                    }
                }

                catch (Exception)
                {
                    throw;
                    //continue;
                }
            }

            foreach (var item in listProductUrl)
            {
                try
                {
                    web = new HtmlWeb();
                    doc = web.Load(item);
                    Thread.Sleep(1000);
                    var product = new Product
                    {
                        Name = doc.DocumentNode
                            .SelectSingleNode(
                                "//h1[contains(@class,'product_title') and contains(@class,'entry-title')]")
                            .InnerText,
                        Description = doc.DocumentNode.SelectSingleNode("//div[@class='description']").InnerHtml
                    };
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
                        var img = new Picture
                        {
                            Url = urlimage,
                            IsDeleted = false
                        };
                        _pictureService.CreatePicture(img);
                        var picMap = new ProductPictureMapping
                        {
                            ProductId = product.Id,
                            PictureId = img.Id
                        };
                        _productPictureMappingService.CreateProductPictureMapping(picMap);
                    }

                    #endregion
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return View();
        }

        public ActionResult Sieuthicayxanh()
        {
            var url = "http://sieuthicayxanh.vn/cay-an-qua";
            List<string> listProductUrl = new List<string>();
            List<string> pageUrl = new List<string>();
            for (int i = 1; i <= 1; i++)
            {
                pageUrl.Add(url);
            }
            HtmlWeb web;
            HtmlDocument doc;
            //get list item url
            foreach (var page in pageUrl)
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
                            listProductUrl.Add("http://sieuthicayxanh.vn" + productUrl.Attributes["href"].Value);
                        }
                    }
                }

                catch (Exception)
                {
                    throw;
                    //continue;
                }
            }

            foreach (var item in listProductUrl)
            {
                try
                {
                    web = new HtmlWeb();
                    doc = web.Load(item);
                    Thread.Sleep(1000);
                    var product = new Product
                    {
                        Name = doc.DocumentNode.SelectSingleNode("//h1//span[@class='title']").InnerText,
                        Description = doc.DocumentNode.SelectSingleNode("//div[@class='summary']//p").InnerHtml,
                        Content = doc.DocumentNode.SelectSingleNode("//div[@class='summary']//p").InnerHtml,
                        DateCreated = DateTime.Now,
                        LastEditedTime = DateTime.Now,
                        IsHomePage = true,
                        IsPublic = true
                    };
                    _productService.CreateProduct(product);
                    product.Slug = StringConvert.ConvertShortName(product.Name + " " + product.Id);
                    _productService.EditProduct(product);
                    var cate = new ProductCategoryMapping
                    {
                        ProductCategoryId = 42,
                        ProductId = product.Id
                    };
                    _productCategoryMappingService.CreateProductCategoryMapping(cate);

                    #region Add image

                    var imageUrl = "http://sieuthicayxanh.vn" + doc.DocumentNode
                                       .SelectSingleNode("//ul[@id='thumb_img']//li/img").Attributes["src"].Value;
                    var urlimage = SaveImage(imageUrl);
                    for (int i = 0; i < 5; i++)
                    {
                        var img = new Picture
                        {
                            Url = urlimage,
                            IsDeleted = false
                        };
                        _pictureService.CreatePicture(img);
                        var picMap = new ProductPictureMapping
                        {
                            ProductId = product.Id,
                            PictureId = img.Id
                        };
                        _productPictureMappingService.CreateProductPictureMapping(picMap);
                    }

                    #endregion
                }
                catch (Exception)
                {
                    //throw;
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
                // Image img = Image.FromStream(imageStream);
                var path = Path.Combine(Server.MapPath("~/images/uploaded/caycanh/"), url.Split('/').LastOrDefault() ??
                                                                                      throw new
                                                                                          InvalidOperationException());
                // img.Save(path);
                return "/images/uploaded/caycanh/" + url.Split('/').LastOrDefault();
            }
            catch (Exception)
            {
                return url;
            }
        }

        public string RemovePStrongB(string content, string name)
        {
            return content.Replace("<p>", "<div>").Replace("</p>", "</div>").Replace("<strong>",
                    "<a href='/tin-tuc/" + StringConvert.ConvertShortName(name) + "' alt='" + name + "'>")
                .Replace("</strong>", "</a>").Replace("<b>", null).Replace("</b>", null);
        }

        public string ChangeLinkaHref(string content, string name)
        {
            HtmlDocument doc = new HtmlDocument();
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
                    // ignored
                }
            }
            var newContent = doc.DocumentNode.OuterHtml;
            return newContent;
        }
    }
}