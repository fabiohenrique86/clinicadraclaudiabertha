using ClinicaMedica.Business;
using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class MedicacaoController : Controller
    {
        private MedicacaoBusiness medicacaoBusiness;

        public MedicacaoController()
        {
            medicacaoBusiness = new MedicacaoBusiness();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(MedicacaoDto medicacaoDto)
        {
            var medicacoesDto = new List<MedicacaoDto>();

            try
            {
                medicacoesDto = medicacaoBusiness.Listar(medicacaoDto);

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Medicações listadas com sucesso", MedicacoesDto = medicacoesDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao listar medicação" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Inserir(MedicacaoDto medicacaoDto)
        {
            try
            {
                medicacaoDto.DataCadastro = DateTime.Now;

                medicacaoBusiness.Inserir(medicacaoDto);

                var medicacoesDto = medicacaoBusiness.Listar(new MedicacaoDto() { PacienteDto = new PacienteDto() { Id = medicacaoDto.PacienteDto.Id } });

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Medicação cadastrada com sucesso", MedicacoesDto = medicacoesDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao inserir medicação" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Excluir(MedicacaoDto medicacaoDto)
        {
            try
            {
                medicacaoDto.DataExclusao = DateTime.Now;

                medicacaoBusiness.Excluir(medicacaoDto);

                var medicacoesDto = medicacaoBusiness.Listar(new MedicacaoDto() { PacienteDto = new PacienteDto() { Id = medicacaoDto.PacienteDto.Id } });

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Medicação excluída com sucesso", MedicacoesDto = medicacoesDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao excluir medicação" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}