using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using ClinicaMedica.Repository;
using System;
using System.Collections.Generic;

namespace ClinicaMedica.Business
{
    public class MedicacaoBusiness
    {
        private MedicacaoRepository medicacaoRepository;

        public MedicacaoBusiness()
        {
            medicacaoRepository = new MedicacaoRepository();
        }

        private void ValidarInserir(MedicacaoDto medicacaoDto)
        {
            if (medicacaoDto == null)
            {
                throw new BusinessException("MedicacaoDto é obrigatório");
            }

            if (string.IsNullOrEmpty(medicacaoDto.Descricao))
            {
                throw new BusinessException("Nome do medicamento é obrigatório");
            }

            if (medicacaoDto.PacienteDto == null || medicacaoDto.PacienteDto.Id <= 0)
            {
                throw new BusinessException("Paciente é obrigatório");
            }
        }

        private void ValidarExcluir(MedicacaoDto medicacaoDto)
        {
            if (medicacaoDto == null)
            {
                throw new BusinessException("MedicacaoDto é obrigatório");
            }

            if (medicacaoDto.Id <= 0)
            {
                throw new BusinessException("Id é obrigatório");
            }
        }

        public List<MedicacaoDto> Listar(MedicacaoDto medicacaoDto = null)
        {
            try
            {
                return medicacaoRepository.Listar(medicacaoDto);
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

        public void Inserir(MedicacaoDto medicacaoDto)
        {
            try
            {
                ValidarInserir(medicacaoDto);

                medicacaoRepository.Inserir(medicacaoDto);
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

        public void Excluir(MedicacaoDto medicacaoDto)
        {
            try
            {
                ValidarExcluir(medicacaoDto);

                medicacaoRepository.Excluir(medicacaoDto);
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
