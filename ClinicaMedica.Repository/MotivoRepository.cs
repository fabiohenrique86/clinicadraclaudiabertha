using ClinicaMedica.Dto;
using ClinicaMedica.Repository.Tradutors;
using System.Collections.Generic;
using System.Linq;

namespace ClinicaMedica.Repository
{
    public class MotivoRepository
    {
        private clinicamedicaEntities _clinicamedicaEntities;

        public MotivoRepository()
        {
            _clinicamedicaEntities = new clinicamedicaEntities();
        }

        public List<MotivoDto> Listar(MotivoDto motivoDto = null)
        {
            IQueryable<Motivo> query = _clinicamedicaEntities.Motivo;

            if (motivoDto != null)
            {
                if (motivoDto.Id > 0)
                {
                    query = query.Where(x => x.Id == motivoDto.Id);
                }

                if (!string.IsNullOrEmpty(motivoDto.Descricao))
                {
                    query = query.Where(x => x.Descricao == motivoDto.Descricao.ToLower());
                }
            }

            //return Mock();
            return query.AsEnumerable().Select(x => x.ToApp()).ToList();
        }

        private List<MotivoDto> Mock()
        {
            var retorno = new List<MotivoDto>();

            retorno.Add(new MotivoDto() { Id = 1, Descricao = "Motivos Pessoais" });
            retorno.Add(new MotivoDto() { Id = 2, Descricao = "Inconsistência" });

            return retorno;
        }
    }
}
