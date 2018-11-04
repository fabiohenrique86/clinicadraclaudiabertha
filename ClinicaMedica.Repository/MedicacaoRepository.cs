using ClinicaMedica.Dto;
using ClinicaMedica.Repository.Tradutors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicaMedica.Repository
{
    public class MedicacaoRepository
    {
        private clinicamedicaEntities _clinicamedicaEntities;

        public MedicacaoRepository()
        {
            _clinicamedicaEntities = new clinicamedicaEntities();
        }

        public void Inserir(MedicacaoDto medicacaoDto)
        {
            var medicacao = medicacaoDto.ToBd();

            _clinicamedicaEntities.Entry(medicacao).State = EntityState.Added;
            _clinicamedicaEntities.SaveChanges();
        }

        public void Excluir(MedicacaoDto medicacaoDto)
        {
            var medicacao = _clinicamedicaEntities.Medicacao.FirstOrDefault(x => x.Id == medicacaoDto.Id);

            if (medicacao != null)
            {
                medicacao.Ativo = false;
                medicacao.DataExclusao = medicacaoDto.DataExclusao;

                _clinicamedicaEntities.Entry(medicacao).State = EntityState.Modified;
                _clinicamedicaEntities.SaveChanges();
            }
        }

        public List<MedicacaoDto> Listar(MedicacaoDto medicacaoDto = null)
        {
            IQueryable<Medicacao> query = _clinicamedicaEntities.Medicacao;

            if (medicacaoDto != null)
            {
                if (medicacaoDto.Id > 0)
                {
                    query = query.Where(x => x.Id == medicacaoDto.Id);
                }

                if (!string.IsNullOrEmpty(medicacaoDto.Descricao))
                {
                    query = query.Where(x => x.Descricao.Contains(medicacaoDto.Descricao));
                }

                if (medicacaoDto.PacienteDto != null && medicacaoDto.PacienteDto.Id > 0)
                {
                    query = query.Where(x => x.PacienteId == medicacaoDto.PacienteDto.Id);
                }
            }

            query = query.Where(x => x.Ativo);

            //return Mock();
            return query.AsEnumerable().Select(x => x.ToApp()).OrderBy(x => x.Descricao).ToList();
        }

        private List<MedicacaoDto> Mock()
        {
            var retorno = new List<MedicacaoDto>();

            retorno.Add(new MedicacaoDto() { Id = 1, Descricao = "Dipirona 500mg", DataCadastro = DateTime.Now, Ativo = true, PacienteDto = new PacienteDto() { Id = 1, Nome = "Fabio Henrique" } });
            retorno.Add(new MedicacaoDto() { Id = 2, Descricao = "Dorflex", DataCadastro = DateTime.Now, Ativo = true, PacienteDto = new PacienteDto() { Id = 1, Nome = "Fabio Henrique" } });

            return retorno;
        }
    }
}
