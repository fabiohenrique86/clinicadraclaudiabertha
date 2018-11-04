using ClinicaMedica.Business;
using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class PacientesController : BaseController
    {
        private PacienteBusiness pacienteBusiness;

        public PacientesController()
        {
            pacienteBusiness = new PacienteBusiness();
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

        public JsonResult Listar(PacienteDto pacienteDto)
        {
            var pacientesDto = new List<PacienteDto>();

            try
            {
                pacientesDto = pacienteBusiness.Listar(pacienteDto);

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Paciente listados com sucesso", PacientesDto = pacientesDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao listar paciente" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Inserir(PacienteDto pacienteDto)
        {
            try
            {
                pacienteBusiness.Inserir(pacienteDto);

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Paciente cadastrado com sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao inserir paciente" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Atualizar(PacienteDto pacienteDto)
        {
            try
            {
                pacienteBusiness.Atualizar(pacienteDto);

                var pacientesDto = pacienteBusiness.Listar();

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Paciente atualizado com sucesso", PacientesDto = pacientesDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao atualizar paciente" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}