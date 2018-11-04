using ClinicaMedica.Business;
using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using System;
using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class CadastrarAgendaController : BaseController
    {
        private EventoBusiness eventoBusiness;

        public CadastrarAgendaController()
        {
            eventoBusiness = new EventoBusiness();
        }

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

        public JsonResult Inserir(EventoDto eventoDto)
        {
            try
            {
                eventoBusiness.Inserir(eventoDto);

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Agenda cadastrada com sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message, Eventos = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao cadastrar agenda", Eventos = string.Empty }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}