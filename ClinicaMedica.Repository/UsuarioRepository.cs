using ClinicaMedica.Dto;
using ClinicaMedica.Repository.Tradutors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicaMedica.Repository
{
    public class UsuarioRepository
    {
        private clinicamedicaEntities _clinicamedicaEntities;

        public UsuarioRepository()
        {
            _clinicamedicaEntities = new clinicamedicaEntities();
        }

        public List<UsuarioDto> Listar(UsuarioDto usuarioDto = null)
        {
            IQueryable<Usuario> query = _clinicamedicaEntities.Usuario;

            if (usuarioDto != null)
            {
                if (usuarioDto.Id > 0)
                {
                    query = query.Where(x => x.Id == usuarioDto.Id);
                }

                if (!string.IsNullOrEmpty(usuarioDto.Login))
                {
                    query = query.Where(x => x.Login == usuarioDto.Login.ToLower());
                }

                if (!string.IsNullOrEmpty(usuarioDto.Senha))
                {
                    query = query.Where(x => x.Senha == usuarioDto.Senha.ToLower());
                }
            }

            //return Mock();
            return query.AsEnumerable().Select(x => x.ToApp()).ToList();
        }

        private List<UsuarioDto> Mock()
        {
            var retorno = new List<UsuarioDto>();

            retorno.Add(new UsuarioDto() { Id = 1, Login = "claudia", Senha = "123456" });
            retorno.Add(new UsuarioDto() { Id = 1, Login = "eduardo", Senha = "123456" });

            return retorno;
        }
    }
}
