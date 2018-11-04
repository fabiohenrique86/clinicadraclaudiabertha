using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}