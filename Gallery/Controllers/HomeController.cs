using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gallery.Models.DomainModels;
using Gallery.Models.ViewModels;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private const int DefaultPicturesByPage = 6;
        private const int DefaultPage = 1;
        private readonly PicturesContext _context = new PicturesContext();

        public ActionResult Index(string filter = null, int page = DefaultPage, int picturesOnPage = DefaultPicturesByPage)
            => View(this.LoadPage(filter, page, picturesOnPage));

        public ActionResult PartialIndex(
            string filter = null, int page = DefaultPage, int picturesOnPage = DefaultPicturesByPage)
        {
            if (!Request.IsAjaxRequest())
            {
                return Redirect(this.GenerateRedirectUrl(filter, page, picturesOnPage));
            }

            return PartialView("_PartialIndex", LoadPage(filter, page, picturesOnPage));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        private static IEnumerable<string> GetPaths(IEnumerable<Picture> pictures)
            => pictures?.Select(x => $"../Pictures/{x.Name}.{x.Type}");

        private PageViewModel LoadPage(string filter, int page, int picturesOnPage)
        {
            ViewBag.Filter = filter;

            var content = _context.Pictures
                .Where(x => filter == null || x.Description.Contains(filter))
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * picturesOnPage)
                .Take(picturesOnPage)
                .ToList();

            return new PageViewModel
            {
                Current = page,
                PageSize = picturesOnPage,
                Records = content.Count,
                Paths = GetPaths(content).ToList(),
                Pages = this.GetPagesCount(picturesOnPage)
            };
        }

        private int GetPagesCount(int picturesOnPage)
        {
            var totalPagesAmount = _context.Pictures.Count();
            var filledPagesAmount = totalPagesAmount / picturesOnPage;

            return totalPagesAmount % 2 == 0 ? filledPagesAmount : filledPagesAmount + 1;
        }

        private string GenerateRedirectUrl(string filter, int page, int picturesOnPage)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return $"../Home/Index?{nameof(page)}={page}&&{nameof(picturesOnPage)}={picturesOnPage}";
            }

            return $"../Home/Index?{nameof(filter)}={filter}&&{nameof(page)}={page}&&{nameof(picturesOnPage)}={picturesOnPage}";
        }
    }
}