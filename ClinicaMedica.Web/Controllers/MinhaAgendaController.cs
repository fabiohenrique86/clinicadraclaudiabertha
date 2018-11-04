using ClinicaMedica.Business;
using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class MinhaAgendaController : BaseController
    {
        private EventoBusiness eventoBusiness;
        private StatusBusiness statusBusiness;
        private MotivoBusiness motivoBusiness;

        public MinhaAgendaController()
        {
            eventoBusiness = new EventoBusiness();
            statusBusiness = new StatusBusiness();
            motivoBusiness = new MotivoBusiness();
        }

        public ActionResult Index()
        {
            string tela = "";
            if (!ValidarSessao(out tela))
            {
                Response.Redirect(tela, true);
                return null;
            }

            ViewBag.StatusDto = statusBusiness.Listar().Where(x => x.Id != StatusDto.EStatus.Programada.GetHashCode()).ToList();
            ViewBag.MotivoDto = motivoBusiness.Listar();

            return View();
        }

        public JsonResult Listar(string dtInicial = null, string dtFinal = null, string pacienteId = null)
        {
            try
            {
                DateTime dataInicial;
                DateTime.TryParse(dtInicial, out dataInicial);

                DateTime dataFinal;
                DateTime.TryParse(dtFinal, out dataFinal);

                int idPaciente = 0;
                int.TryParse(pacienteId, out idPaciente);

                var eventosDto = eventoBusiness.Listar(new EventoDto()
                {
                    DataInicial = dataInicial == DateTime.MinValue ? DateTime.MinValue : dataInicial.ToUniversalTime(),
                    DataFinal = dataFinal == DateTime.MinValue ? DateTime.MinValue : dataFinal.ToUniversalTime(),
                    PacienteDto = new PacienteDto() { Id = idPaciente }
                })
                .Where(x => x.PacienteDto != null && x.PacienteDto.Id > 0)
                .ToList();

                return Json(new { Sucesso = true, Erro = false, Mensagem = string.Empty, EventosDto = eventosDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message, Eventos = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao listar agenda", Eventos = string.Empty }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}