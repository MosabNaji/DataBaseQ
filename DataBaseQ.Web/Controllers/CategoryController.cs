using DataBaseQ.Web.Data;
using DataBaseQ.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseQ.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int page , string SearchKey)
        {
            var numberOfPages = Math.Ceiling(_db.Categories.Count(x => !x.IsDelete && (x.Name.Contains(SearchKey) || string.IsNullOrWhiteSpace(SearchKey))) /10.0);
            if(page > numberOfPages || page < 1)
            {
                page = 1;
            }
            var skipeValue = (page - 1) * 10;

            var categories = _db.Categories.Where(x => !x.IsDelete && (x.Name.Contains(SearchKey) || string.IsNullOrWhiteSpace(SearchKey))).Skip(skipeValue).Take(10).ToList();
            var categoriesVm = new List<CategoryViewModel>();
            foreach(var category in categories)
            {
                var categoryVm = new CategoryViewModel();
                categoryVm.Name = category.Name;
                categoryVm.Id = category.Id;
                categoryVm.PostCount = _db.Posts.Count(x => x.CategoryId == categoryVm.Id);
                var LastItem = _db.Posts.Where(x => x.CategoryId == category.Id).OrderByDescending(x => x.CreatedAt).FirstOrDefault();
                if(LastItem != null)
                {
                    categoryVm.LastPostAded = LastItem.CreatedAt;
                }else
                {
                    categoryVm.LastPostAded = null ;
                }
                categoriesVm.Add(categoryVm);

            }
            var result = new PagingResultViewModel();
            result.NumberOfPages = (int)numberOfPages;
            result.CurrentPage = page;
            result.Data = categoriesVm;

            ViewBag.searchKey = SearchKey;
            return View(result);
        }

        public IActionResult Delete(int id)
        {
            var category = _db.Categories.Find(id);
            category.IsDelete = true;
            _db.Categories.Update(category);
            _db.SaveChanges();
            return RedirectToAction("Index"); //بعد الحذف هيعمل اعادة توجيه للشاشة الرئيسية
        }

        //public IActionResult GetAll()
        //{
        //    var x = _db.Categories.ToList();
        //    return Ok(x);
        //}

        //public IActionResult Get(int Id)
        //{
        //    var y = _db.Categories.Find(Id);
        //    if(y == null)
        //    {
        //        return Ok("NotFounds");
        //    }
        //    else 
        //    { 
        //        return Ok(y);
        //    }

        //}

        //public IActionResult GetByOrder()
        //{
        //    var x = _db.Categories.OrderBy(x => x.Name).ToList();
        //    return Ok(x);
        //}

        //public IActionResult GetByOrderDesc()
        //{
        //    var x = _db.Categories.OrderByDescending(x => x.Name).ToList();
        //    return Ok(x);
        //}

        //public IActionResult GetWhere()
        //{
        //    var x = _db.Categories.Where(x => x.Name == "طبخ").ToList();
        //    return Ok(x);
        //}

        //public IActionResult GetByWhere()
        //{
        //    var x = _db.Categories.Where(x => x.Name.Contains("طبخ")).ToList();
        //    return Ok(x);
        //}

        //public IActionResult GetCount()
        //{
        //    var x = _db.Categories.Count();
        //    return Ok(x);
        //}

        //public IActionResult GetCount1()
        //{
        //    var x = _db.Categories.Where(x => x.Name.Contains("طبخ")).ToList();
        //    return Ok(x.Count());
        //}

        //public IActionResult GetCount2()
        //{
        //    var x = _db.Categories.Count(x => x.Name.Contains("طبخ"));
        //    return Ok(x);
        //}

        //public IActionResult IsDataFound()
        //{
        //    return Ok(_db.Categories.Any());
        //}

        //public IActionResult SA()
        //{
        //    var x = _db.Categories.Skip(2).Take(3).ToList();
        //    return Ok(x);
        //}

        //public IActionResult First()
        //{
        //    var x = _db.Categories.First();
        //    return Ok(x);
        //}

        //public IActionResult FirstTabekh()
        //{
        //    var x = _db.Categories.First(x => x.Name.Contains("احمد"));
        //    return Ok(x);
        //}

        //public IActionResult FirstAhmed()
        //{
        //    var x = _db.Categories.FirstOrDefault(x => x.Name.Contains("احمد"));
        //    if (x == null)
        //    { return Ok("not found"); }
        //    return Ok(x);
        //}

        //public IActionResult Delete(int id)
        //{
        //    var x = _db.Categories.Find(id);
        //    if(x == null)
        //    {
        //        return Ok("Invalid Id");
        //    }
        //    //var y = x; 
        //    _db.Categories.Remove(x);
        //    _db.SaveChanges();
        //    return Ok("done");
        //}


    }
}
