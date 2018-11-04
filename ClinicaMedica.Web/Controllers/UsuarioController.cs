using ClinicaMedica.Business;
using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaMedica.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioBusiness usuarioBusiness;

        public UsuarioController()
        {
            usuarioBusiness = new UsuarioBusiness();
        }

        public JsonResult Listar(UsuarioDto usuarioDto)
        {
            var usuariosDto = new List<UsuarioDto>();

            try
            {
                usuariosDto = usuarioBusiness.Listar(usuarioDto);

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Usuários listados com sucesso", UsuariosDto = usuariosDto }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message, UsuariosDto = usuariosDto }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao listar usuário", UsuariosDto = usuariosDto }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Entrar(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = usuarioBusiness.Listar(usuarioDto).FirstOrDefault();

                if (usuario == null)
                {
                    return Json(new { Sucesso = false, Mensagem = "Usuário e/ou senha inválidos", Erro = string.Empty }, JsonRequestBehavior.AllowGet);
                }

                Login(new UsuarioDto() { Id = usuario.Id, Login = usuario.Login });

                return Json(new { Sucesso = true, Erro = false, Mensagem = "Login do usuário efetuado com sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Erro = ex.ToString(), Mensagem = "Ocorreu um erro ao efetuar login do usuário" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Sair()
        {
            Logout();

            return RedirectToAction("Index", "Home");
        }

        private void Login(UsuarioDto usuarioDto)
        {
            try
            {
                var cookieName = "ClinicaDraClaudiaBertha_Usuario";
                HttpCookie httpCookie = Request.Cookies[cookieName] ?? new HttpCookie(cookieName);
                usuarioDto.Senha = string.Empty;
                httpCookie.Value = JsonConvert.SerializeObject(usuarioDto);
                httpCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(httpCookie);
            }
            catch (Exception ex)
            {

            }
        }

        private void Logout()
        {
            try
            {
                if (Request.Cookies["ClinicaDraClaudiaBertha_Usuario"] != null)
                {
                    Response.Cookies["ClinicaDraClaudiaBertha_Usuario"].Expires = DateTime.Now.AddDays(-1);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}