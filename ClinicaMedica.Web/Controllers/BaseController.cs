using System;
using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class BaseController : Controller
    {
        public bool ValidarSessao(out string tela)
        {
            var retorno = false;
            tela = "~/Home/Index";

            try
            {
                if (Request.Cookies.Get("ClinicaDraClaudiaBertha_Usuario") != null)
                {
                    retorno = true;
                    tela = string.Empty;

                    return retorno;
                }

                var controller = Request.RequestContext.RouteData.Values["controller"].ToString();
                var action = Request.RequestContext.RouteData.Values["action"].ToString();

                if (controller == "MinhaAgenda")
                {
                    if (action == "Index")
                    {
                        retorno = false;
                    }
                }
                else if (controller == "Pacientes")
                {
                    if (action == "Index")
                    {
                        retorno = false;
                    }
                }
                else if (controller == "Inicio")
                {
                    if (action == "Index")
                    {
                        retorno = false;
                    }
                }
                else if (controller == "CadastrarAgenda")
                {
                    if (action == "Index")
                    {
                        retorno = false;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno = false;
            }

            return retorno;
        }
    }
}