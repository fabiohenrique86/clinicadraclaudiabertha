using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class InicioController : BaseController
    {
        public ActionResult Index()
        {
            string tela = "";
            if (!ValidarSessao(out tela))
            {
                Response.Redirect(tela, true);
                return null;
            }

            return View();
        }
    }
}