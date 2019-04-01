//using Labixa.Areas.Admin.ViewModel;
//using Outsourcing.Data.Models;
//using Outsourcing.Service;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Outsourcing.Core.Extensions;

//namespace Labixa.Areas.Admin.Controllers
//{
//    public class InventoryController : Controller
//    {
//        #region Field

//        readonly IProductService _productService;
//        readonly IProductCategoryService _productCategoryService;

//        readonly IProductAttributeService _productAttributeService;
//        readonly IProductAttributeMappingService _productAttributeMappingService;

//        readonly IPictureService _pictureService;
//        readonly IInventoryService _InventoryService;
//        readonly IInventoryLogService _InventoryLogService;
//        readonly ILocationService _LocationService;
//        readonly IProductPictureMappingService _productPictureMappingService;



//        #endregion

//        #region Ctor
//        public InventoryController(IProductService productService, IProductCategoryService productCategoryService,
//            IProductAttributeService productAttributeService, IProductAttributeMappingService productAttributeMappingService,
//            IPictureService pictureService, IProductPictureMappingService productPictureMappingService,
//            IInventoryService _InventoryService, IInventoryLogService _InventoryLogService,
//         ILocationService _LocationService
//           )
//        {
//            this._productService = productService;
//            this._productCategoryService = productCategoryService;
//            this._productAttributeService = productAttributeService;
//            this._productAttributeMappingService = productAttributeMappingService;
//            this._pictureService = pictureService;
//            this._productPictureMappingService = productPictureMappingService;
//            this._InventoryService = _InventoryService;
//            this._InventoryLogService = _InventoryLogService;
//            this._LocationService = _LocationService;
//        }
//        #endregion
//        //
//        // GET: /Admin/Inventory/
//        public ActionResult Index()
//        {
//            var list = _productService.GetAllProducts();
//            return View(list);
//        }
//        public ActionResult Create(int ProductId)
//        {
//            var product = _productService.GetProductById(ProductId);
//            InventoryLog obj = new InventoryLog();
//            InventoryModels inventorymodels = new InventoryModels();
//            inventorymodels.inventory = obj;
//            inventorymodels.product = product;
//            var listLocation = _LocationService.GetLocations().ToSelectListItems(-1);
//            inventorymodels.Location = listLocation;

//            //Get the list category
//            return View(inventorymodels);
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult Create(InventoryModels newInventory)
//        {
//            if (ModelState.IsValid)
//            {
//                InventoryLog obj = new InventoryLog();
//                obj.DateCreated = DateTime.Now;
//                obj.Description = newInventory.inventory.Description;
//                obj.Price = newInventory.inventory.Price;
//                obj.Quantity = newInventory.inventory.Quantity;
//                obj.IsDelete = false;
//                obj.IsImport = newInventory.inventory.IsImport;
//                obj.Name = newInventory.product.Name;
//                obj.LocationId = newInventory.locationId;
//                obj.IsChangeLocation = false;
//                obj.ProductId = newInventory.product.Id;
//                //Mapping to domain
//                //Create Blog
//                var check = _productService.GetProductById(newInventory.product.Id);
//                Inventory inventory = new Inventory();
//                if (check.InventoryLogs.Any())
//                {
//                    if (check.InventoryLogs.FirstOrDefault().Inventory != null)
//                    {
//                        if (newInventory.inventory.IsImport)
//                        {
//                            check.InventoryLogs.FirstOrDefault().Inventory.Quantity += newInventory.inventory.Quantity;
//                        }
//                        else
//                        {
//                            check.InventoryLogs.FirstOrDefault().Inventory.Quantity -= newInventory.inventory.Quantity;
//                        }
//                        inventory = check.InventoryLogs.FirstOrDefault().Inventory;
//                        _productService.EditProduct(check);
//                    }
//                    else
//                    {
//                        inventory.DateCreated = DateTime.Now;
//                        inventory.Description = "null";
//                        inventory.IsDelete = false;
//                        inventory.isImport = true;
//                        inventory.Name = newInventory.product.Name;
//                        inventory.Price = obj.Price;
//                        inventory.Quantity = obj.Quantity;
//                      inventory =   _InventoryService.CreateInventory(inventory);
//                    }
//                }
//                else
//                {
//                    inventory.DateCreated = DateTime.Now;
//                    inventory.Description = "null";
//                    inventory.IsDelete = false;
//                    inventory.isImport = true;
//                    inventory.Name = newInventory.product.Name;
//                    inventory.Price = obj.Price;
//                    inventory.Quantity = obj.Quantity;
//                    inventory = _InventoryService.CreateInventory(inventory);

//                }
//                obj.InventoryId = inventory.Id;
//                _InventoryLogService.CreateInventoryLog(obj);
//                return RedirectToAction("Index", "Inventory");
//            }
//            else
//            {
//                return View("Create", newInventory);
//            }
//        }

//        [HttpGet]
//        public ActionResult Edit(int InventoryId)
//        {

//            var Inventory = _InventoryService.GetInventoryById(InventoryId);
//            return View(model: Inventory);
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult Edit(Inventory Inventorytoedit)
//        {
//            if (ModelState.IsValid)
//            {
//                //Mapping to domain
//                _InventoryService.EditInventory(Inventorytoedit);
//                return RedirectToAction("Index", "Inventory");
//            }
//            else
//            {
//                return View("Edit", Inventorytoedit);
//            }
//        }
//        [HttpPost]
//        public ActionResult Delete(int id)
//        {
//            _InventoryService.DeleteInventory(id);
//            return RedirectToAction("Index");
//        }
//        public ActionResult Location(int ProductId)
//        {
//            var ProLocation = _productService.GetProductById(ProductId);
//            return View(ProLocation);
//        }
//    }
//}