using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using ShoppingApp_DataAccess.Infrastracture.Interface;
using ShoppingApp_Models;
using ShoppingApp_Models.ViewModel;
using System.Drawing.Printing;

namespace ShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _environment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM product = new ProductVM()
            {
                Product = new(),
                Categories = _unitOfWork.Category.GetAll().Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })
            };
            if (id == 0 || id == null)
            {
                return View(product);
            }
            else
            {
                product.Product = _unitOfWork.Product.GetT(x => x.Id == id);
                if (product.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }
        [HttpPost]        
        public IActionResult CreateUpdate(ProductVM productVm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if(file != null)
                {
                    //Add Path for Image
                    string uploadDirectry = Path.Combine(_environment.WebRootPath,"Images");
                        
                    //Get fileName
                    fileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                    //Path and Filename Combine
                    string filePath = Path.Combine(uploadDirectry, fileName);

                    //Update file and get database sile file
                    if(productVm.Product.ImageUrl != null)
                    {
                        var oldPath = Path.Combine(_environment.WebRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    //Add Image in Images Folder 
                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVm.Product.ImageUrl = @"\Images\" + fileName;

                }
                if(productVm.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVm.Product);
                    TempData["Success"] = "Product Created SuccesFully!!";
                }
                else
                {
                    _unitOfWork.Product.Update(productVm.Product);
                    TempData["Success"] = "Product Update SuccesFully!!";
                }
                
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    ProductVM productVm = new ProductVM();
        //    productVm.Product = _unitOfWork.Product.GetT(x => x.Id == id);
        //    if (productVm.Product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productVm);

        //}

        #region DeleteApi
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var Product = _unitOfWork.Product.GetT(x => x.Id == id);
            if (Product == null)
            {
                return Json(new {success = false, message="Error in Feching Data!!"});
            }
            else
            {
                var oldPath = Path.Combine(_environment.WebRootPath, Product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                _unitOfWork.Product.Delete(Product);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Product Deleted Successfully!!" });
            }
        }
        #endregion

        [HttpGet]
        public IActionResult Details(int id)
        {
            ProductVM productVm = new ProductVM();
            productVm.Product = _unitOfWork.Product.GetT(x => x.Id == id);
            if (productVm.Product == null)
            {
                return NotFound();
            }
            return View(productVm);
        }
        //public IActionResult Search(string search)
        //{
        //    ProductVM productVm = new ProductVM();
        //    productVm.ProductList = _unitOfWork.Product.Search(x => x.Name.Contains(search) && x.Category.CategoryName.Contains(search));
        //    if (productVm.ProductList == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productVm);

        //}

        #region GetAllApi
         
        public IActionResult GetAllProducts()
        {
            var result = _unitOfWork.Product.GetAll(incldeProperty:"Category");
              return Json(new { data = result });
        }

        #endregion

    }
}
