using System.Web.Mvc;

namespace OnlineCassino.WebApi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Help", new { });
        }
    }
}