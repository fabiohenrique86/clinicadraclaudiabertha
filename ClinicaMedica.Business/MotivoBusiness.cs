using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using ClinicaMedica.Repository;
using System;
using System.Collections.Generic;

namespace ClinicaMedica.Business
{
    public class MotivoBusiness
    {
        private MotivoRepository motivoRepository;

        public MotivoBusiness()
        {
            motivoRepository = new MotivoRepository();
        }
                
        public List<MotivoDto> Listar(MotivoDto motivoDto = null)
        {
            try
            {
                return motivoRepository.Listar(motivoDto);
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
