using ClinicaMedica.Dto;
using ClinicaMedica.Repository.Tradutors;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicaMedica.Repository
{
    public class PacienteRepository
    {
        private clinicamedicaEntities _clinicamedicaEntities;

        public PacienteRepository()
        {
            _clinicamedicaEntities = new clinicamedicaEntities();
        }

        public List<PacienteDto> Listar(PacienteDto pacienteDto = null)
        {
            IQueryable<Paciente> query = _clinicamedicaEntities.Paciente;

            if (pacienteDto != null)
            {
                if (pacienteDto.Id > 0)
                {
                    query = query.Where(x => x.Id == pacienteDto.Id);
                }

                if (!string.IsNullOrEmpty(pacienteDto.Nome))
                {
                    query = query.Where(x => x.Nome.Contains(pacienteDto.Nome));
                }

                if (!string.IsNullOrEmpty(pacienteDto.Cpf))
                {
                    query = query.Where(x => x.Cpf == pacienteDto.Cpf.Replace(".", "").Replace("-", ""));
                }

                if (!string.IsNullOrEmpty(pacienteDto.Email))
                {
                    query = query.Where(x => x.Email == pacienteDto.Email);
                }
            }

            //return Mock();
            return query.AsEnumerable().Select(x => x.ToApp()).OrderBy(x => x.Nome).ToList();
        }

        private List<PacienteDto> Mock()
        {
            var retorno = new List<PacienteDto>();

            retorno.Add(new PacienteDto() { Id = 1, Cns = "12000.9854.34566.79", Cpf = "838.877.925-72", Email = "fabiohenrique86@gmail.com", Nome = "Fabio Henrique", Telefone = "(71) 99555-1568" });
            retorno.Add(new PacienteDto() { Id = 1, Cns = "44400.5987.33315.00", Cpf = "195.552.455-68", Email = "ritadecacia@gmail.com", Nome = "Rita de Cacia", Telefone = "(71) 99252-0067" });

            return retorno;
        }

        public int Inserir(PacienteDto pacienteDto)
        {
            var paciente = pacienteDto.ToBd();

            _clinicamedicaEntities.Entry(paciente).State = EntityState.Added;
            _clinicamedicaEntities.SaveChanges();

            return paciente.Id;
        }

        public void Atualizar(PacienteDto pacienteDto)
        {
            var p = _clinicamedicaEntities.Paciente.FirstOrDefault(x => x.Id == pacienteDto.Id);

            if (p != null)
            {
                var paciente = pacienteDto.ToBd();

                if (!string.IsNullOrEmpty(paciente.Nome))
                {
                    p.Nome = paciente.Nome;
                }

                if (!string.IsNullOrEmpty(paciente.Email))
                {
                    p.Email = paciente.Email;
                }

                if (!string.IsNullOrEmpty(paciente.Telefone))
                {
                    p.Telefone = paciente.Telefone;
                }

                if (!string.IsNullOrEmpty(paciente.Cns))
                {
                    p.Cns = paciente.Cns;
                }

                _clinicamedicaEntities.Entry(p).State = EntityState.Modified;
                _clinicamedicaEntities.SaveChanges();
            }
        }
    }
}
