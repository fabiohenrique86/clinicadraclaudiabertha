using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using ClinicaMedica.Repository;
using System;
using System.Collections.Generic;

namespace ClinicaMedica.Business
{
    public class StatusBusiness
    {
        private StatusRepository statusRepository;

        public StatusBusiness()
        {
            statusRepository = new StatusRepository();
        }
                
        public List<StatusDto> Listar(StatusDto statusDto = null)
        {
            try
            {
                return statusRepository.Listar(statusDto);
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
