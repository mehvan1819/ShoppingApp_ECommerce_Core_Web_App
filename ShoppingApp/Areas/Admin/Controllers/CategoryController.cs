using Microsoft.AspNetCore.Mvc;
using ShoppingApp_DataAccess.Infrastracture.Interface;
using ShoppingApp_Models;
using ShoppingApp_Models.ViewModel;

namespace ShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }
        public IActionResult Index()
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.CategoriesList = _unitOfWork.Category.GetAll();
            ViewBag.CategoryList = _unitOfWork.Category.GetAll();
            return View(categoryVM);
        }
        
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM category = new CategoryVM();
            if (id == 0 || id == null)
            {
                return View(category);
            }
            else
            {
                category.Category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (category.Category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(category);
                }
            }
        }
        [HttpPost]
        public IActionResult CreateUpdate(CategoryVM categoryVm)
        {
            if (ModelState.IsValid)
            {
                if (categoryVm.Category.Id == 0)
                {
                    _unitOfWork.Category.Add(categoryVm.Category);
                    TempData["Success"] = "Category Created SuccesFully!!";
                }
                else
                {
                    _unitOfWork.Category.Update(categoryVm.Category);
                    TempData["Success"] = "Category Upadted SuccesFully!!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.Category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (categoryVM.Category == null)
            {
                return NotFound();
            }
            return View(categoryVM);

        }
        [HttpPost]
        public IActionResult Delete(CategoryVM categoryVm)
        {
            _unitOfWork.Category.Delete(categoryVm.Category);
            _unitOfWork.Save();
            TempData["Success"] = "Category Delete SuccesFully!!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.Category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (categoryVM.Category == null)
            {
                return NotFound();
            }
            return View(categoryVM);
        }
        public IActionResult Search(string search)
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.CategoriesList = _unitOfWork.Category.Search(x => x.CategoryName.Contains(search));
            if (categoryVM.CategoriesList == null)
            {
                return NotFound();
            }
            return View(categoryVM);

        }
    }
}
