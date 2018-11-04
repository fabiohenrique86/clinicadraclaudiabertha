using ClinicaMedica.Business;
using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using System;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class AgendarConsultaController : BaseController
    {
        private EventoBusiness eventoBusiness;
        private PacienteBusiness pacienteBusiness;
        private EmailBusiness emailBusiness;
        private MedicacaoBusiness medicacaoBusiness;

        public AgendarConsultaController()
        {
            eventoBusiness = new EventoBusiness();
            pacienteBusiness = new PacienteBusiness();
            emailBusiness = new EmailBusiness();
            medicacaoBusiness = new MedicacaoBusiness();
        }

        public ActionResult Index()
        {
            // lista os eventos
            // de hoje até 30 dias pra frente
            // sem pacientes
            // ou com pacientes, mas com status da consulta 'cancelada'            
            ViewData["EventosDto"] = eventoBusiness.Listar(new EventoDto() { DataInicial = DateTime.Now.ToUniversalTime(), DataFinal = DateTime.Now.ToUniversalTime().AddDays(30) }).Where(x => (x.PacienteDto == null || x.PacienteDto.Id <= 0) || (x.PacienteDto != null && x.PacienteDto.Id > 0 && x.StatusDto != null && x.StatusDto.Id == StatusDto.EStatus.Cancelada.GetHashCode())).Select(x => new { Id = x.Id, DataInicial = x.DataInicial.ToString("dd/MM/yyyy") + " às " + x.DataInicial.ToString("HH:mm") }).ToList();
            
            return View();
        }

        public JsonResult Inserir(EventoDto eventoDto)
        {
            try
            {
                var pacienteDto = pacienteBusiness.Listar(new PacienteDto() { Cpf = eventoDto.PacienteDto.Cpf }).FirstOrDefault();

                if (pacienteDto == null && eventoDto.PacienteDto.IsPaciente)
                {
                    throw new BusinessException("Paciente não encontrado");
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromMinutes(5) }))
                {
                    if (pacienteDto == null)
                    {
                        eventoDto.PacienteDto.Id = pacienteBusiness.Inserir(eventoDto.PacienteDto);
                    }
                    else
                    {
                        eventoDto.PacienteDto.Id = pacienteDto.Id;
                    }
                    
                    eventoBusiness.Atualizar(eventoDto);

                    scope.Complete();
                }

                var eventosDto = eventoBusiness.Listar(new EventoDto() { DataInicial = DateTime.Now.ToUniversalTime(), DataFinal = DateTime.Now.ToUniversalTime().AddDays(30) }).Where(x => (x.PacienteDto == null || x.PacienteDto.Id <= 0) || (x.PacienteDto != null && x.PacienteDto.Id > 0 && x.StatusDto != null && x.StatusDto.Id == StatusDto.EStatus.Cancelada.GetHashCode())).Select(x => new { Id = x.Id, DataInicial = x.DataInicial.ToString("dd/MM/yyyy") + " às " + x.DataInicial.ToString("HH:mm") }).ToList();

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Consulta agendada com sucesso", EventosDto = eventosDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao inserir consulta" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Atualizar(EventoDto eventoDto)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromMinutes(5) }))
                {
                    // atualiza o evento
                    eventoBusiness.Atualizar(eventoDto);

                    // insere as medicações
                    if (eventoDto.PacienteDto != null && eventoDto.PacienteDto.MedicacoesDto != null && eventoDto.PacienteDto.MedicacoesDto.Count() > 0)
                    {
                        var dataCadastro = DateTime.Now;
                        foreach (var medicacaoDto in eventoDto.PacienteDto.MedicacoesDto)
                        {
                            medicacaoDto.DataCadastro = dataCadastro;
                            medicacaoDto.Ativo = true;
                            medicacaoBusiness.Inserir(medicacaoDto);
                        }
                    }
                    
                    // envia o e-mail de confirmação ou cancelamento
                    if (eventoDto.StatusDto != null && eventoDto.StatusDto.Id > 0)
                    {
                        var path = string.Empty;
                        StreamReader sr = null;
                        var evento = eventoBusiness.Listar(new EventoDto() { Id = eventoDto.Id }).FirstOrDefault();
                        var emailDto = new EmailDto() { Destinatario = evento.PacienteDto.Email, Remetente = "contato@clinicadraclaudiabertha.com.br", Titulo = "Clínica Dra. Cláudia Bertha" };

                        if (eventoDto.StatusDto.Id == StatusDto.EStatus.Confirmada.GetHashCode())
                        {
                            path = System.Web.HttpContext.Current.Server.MapPath("/Views/Shared/_EmailConsultaConfirmada.cshtml");
                            sr = new StreamReader(path);
                            emailDto.Mensagem = sr.ReadToEnd().Replace("#NOME_PACIENTE#", evento.PacienteDto.Nome).Replace("#DATA_CONSULTA#", evento.DataInicial.ToString("dd/MM/yyyy HH:mm"));
                            emailDto.Assunto = "Consulta Confirmada";
                        }
                        else if (eventoDto.StatusDto.Id == StatusDto.EStatus.Cancelada.GetHashCode())
                        {
                            path = System.Web.HttpContext.Current.Server.MapPath("/Views/Shared/_EmailConsultaCancelada.cshtml");
                            sr = new StreamReader(path);
                            emailDto.Mensagem = sr.ReadToEnd().Replace("#NOME_PACIENTE#", evento.PacienteDto.Nome)
                                                              .Replace("#DATA_CONSULTA#", evento.DataInicial.ToString("dd/MM/yyyy HH:mm"))
                                                              .Replace("#MOTIVO#", evento.MotivoDto.Descricao);
                            emailDto.Assunto = "Consulta Cancelada";
                        }

                        if (sr != null)
                        {
                            sr.Close();
                            emailBusiness.Enviar(emailDto);
                        }
                    }

                    scope.Complete();
                }

                var eventosDto = eventoBusiness.Listar(new EventoDto() { DataInicial = DateTime.Now.ToUniversalTime(), DataFinal = DateTime.Now.ToUniversalTime().AddDays(30) }).Where(x => x.PacienteDto != null && x.PacienteDto.Id > 0).ToList();

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Consulta atualizada com sucesso", EventosDto = eventosDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao atualizar consulta. Tente salvar novamente em 5 segundos" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}