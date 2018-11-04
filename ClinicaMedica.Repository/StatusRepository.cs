using ClinicaMedica.Dto;
using ClinicaMedica.Repository.Tradutors;
using System.Collections.Generic;
using System.Linq;

namespace ClinicaMedica.Repository
{
    public class StatusRepository
    {
        private clinicamedicaEntities _clinicamedicaEntities;

        public StatusRepository()
        {
            _clinicamedicaEntities = new clinicamedicaEntities();
        }

        public List<StatusDto> Listar(StatusDto statusDto = null)
        {
            IQueryable<Status> query = _clinicamedicaEntities.Status;

            if (statusDto != null)
            {
                if (statusDto.Id > 0)
                {
                    query = query.Where(x => x.Id == statusDto.Id);
                }

                if (!string.IsNullOrEmpty(statusDto.Descricao))
                {
                    query = query.Where(x => x.Descricao == statusDto.Descricao.ToLower());
                }
            }

            //return Mock();
            return query.AsEnumerable().Select(x => x.ToApp()).ToList();
        }

        private List<StatusDto> Mock()
        {
            var retorno = new List<StatusDto>();

            retorno.Add(new StatusDto() { Id = 1, Descricao = "Programada" });
            retorno.Add(new StatusDto() { Id = 2, Descricao = "Confirmada" });
            retorno.Add(new StatusDto() { Id = 3, Descricao = "Cancelada" });

            return retorno;
        }
    }
}
