using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using ClinicaMedica.Repository;
using System;
using System.Collections.Generic;

namespace ClinicaMedica.Business
{
    public class PacienteBusiness
    {
        private PacienteRepository pacienteRepository;

        public PacienteBusiness()
        {
            pacienteRepository = new PacienteRepository();
        }

        private void ValidarInserir(PacienteDto pacienteDto)
        {
            if (pacienteDto == null)
            {
                throw new BusinessException("PacienteDto é obrigatório");
            }

            if (string.IsNullOrEmpty(pacienteDto.Nome))
            {
                throw new BusinessException("Nome é obrigatório");
            }

            if (string.IsNullOrEmpty(pacienteDto.Email))
            {
                throw new BusinessException("E-mail é obrigatório");
            }

            if (string.IsNullOrEmpty(pacienteDto.Cpf))
            {
                throw new BusinessException("Cpf é obrigatório");
            }

            //var paciente = Listar(new PacienteDto() { Email = pacienteDto.Email }).FirstOrDefault();

            //if (paciente != null)
            //{

            //}
        }

        private void ValidarAtualizar(PacienteDto pacienteDto)
        {
            if (pacienteDto == null)
            {
                throw new BusinessException("PacienteDto é obrigatório");
            }
            
            if (pacienteDto.Id <= 0)
            {
                throw new BusinessException("Id é obrigatório");
            }
        }

        public int Inserir(PacienteDto pacienteDto)
        {
            try
            {
                ValidarInserir(pacienteDto);

                return pacienteRepository.Inserir(pacienteDto);
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

        public void Atualizar(PacienteDto pacienteDto)
        {
            try
            {
                ValidarAtualizar(pacienteDto);

                pacienteRepository.Atualizar(pacienteDto);
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

        public List<PacienteDto> Listar(PacienteDto pacienteDto = null)
        {
            try
            {
                return pacienteRepository.Listar(pacienteDto);
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
