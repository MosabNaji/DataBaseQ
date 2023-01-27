using DataBaseQ.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataBaseQ.Web.Controllers
{
    public class PostController : Controller
    {
        public ApplicationDbContext  _db { get; set; }

        public PostController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var posts = _db.Posts.Include(x => x.Category).ToList();
            return View(posts);
        }
        
        //public IActionResult SumTime()
        //{
        //    var x = _db.Posts.Sum(x => x.TimeToRead);
        //    return Ok(x);
        //}
        //public IActionResult GetAll()
        //{
        //    var posts = _db.Posts.Include(x => x.Category).ToList();
        //    return Ok(posts);
        //}

        //public IActionResult GetReadTime()
        //{
        //    var posts = _db.Posts.Include(x=> x.Category).OrderBy(x=> x.TimeToRead).ToList();
        //    return Ok(posts);
        //}
    }
}
