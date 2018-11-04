using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using ClinicaMedica.Repository;
using System;
using System.Collections.Generic;

namespace ClinicaMedica.Business
{
    public class UsuarioBusiness
    {
        private UsuarioRepository usuarioRepository;

        public UsuarioBusiness()
        {
            usuarioRepository = new UsuarioRepository();
        }

        private void ValidarListar(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                throw new BusinessException("UsuarioDto é obrigatório");
            }

            if (string.IsNullOrEmpty(usuarioDto.Login))
            {
                throw new BusinessException("Login é obrigatório");
            }

            if (string.IsNullOrEmpty(usuarioDto.Senha))
            {
                throw new BusinessException("Senha é obrigatória");
            }
        }
        
        public List<UsuarioDto> Listar(UsuarioDto usuarioDto = null)
        {
            try
            {
                ValidarListar(usuarioDto);

                return usuarioRepository.Listar(usuarioDto);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
