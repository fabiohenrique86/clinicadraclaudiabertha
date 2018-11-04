using ClinicaMedica.Dto;
using ClinicaMedica.Repository.Tradutors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicaMedica.Repository
{
    public class EventoRepository
    {
        private clinicamedicaEntities _clinicamedicaEntities;

        public EventoRepository()
        {
            _clinicamedicaEntities = new clinicamedicaEntities();
        }

        public List<EventoDto> Listar(EventoDto eventoDto = null)
        {
            IQueryable<Evento> query = _clinicamedicaEntities.Evento;

            if (eventoDto != null)
            {
                if (eventoDto.Id > 0)
                {
                    query = query.Where(x => x.Id == eventoDto.Id);
                }

                if (eventoDto.PacienteDto != null && eventoDto.PacienteDto.Id > 0)
                {
                    query = query.Where(x => x.PacienteId == eventoDto.PacienteDto.Id);
                }

                if (eventoDto.StatusDto != null && eventoDto.StatusDto.Id > 0)
                {
                    query = query.Where(x => x.StatusId == eventoDto.StatusDto.Id);
                }

                if (eventoDto.DataInicial != DateTime.MinValue)
                {
                    query = query.Where(x => x.DataInicial >= eventoDto.DataInicial);
                }

                if (eventoDto.DataFinal != DateTime.MinValue)
                {
                    query = query.Where(x => x.DataFinal <= eventoDto.DataFinal);
                }
            }

            //return Mock();
            return query.AsEnumerable().Select(x => x.ToApp()).OrderBy(x => x.DataInicial).ToList();
        }

        public void Inserir(EventoDto eventoDto)
        {
            var evento = eventoDto.ToBd();

            _clinicamedicaEntities.Entry(evento).State = EntityState.Added;
            _clinicamedicaEntities.SaveChanges();
        }

        public void Atualizar(EventoDto eventoDto)
        {
            var evento = _clinicamedicaEntities.Evento.FirstOrDefault(x => x.Id == eventoDto.Id);

            if (evento != null)
            {
                if (!string.IsNullOrEmpty(eventoDto.Observacao))
                {
                    evento.Observacao = eventoDto.Observacao.Trim();
                }

                if (eventoDto.MotivoDto != null && eventoDto.MotivoDto.Id > 0)
                {
                    evento.MotivoId = eventoDto.MotivoDto.Id;
                }

                if (eventoDto.StatusDto != null && eventoDto.StatusDto.Id > 0)
                {
                    evento.StatusId = eventoDto.StatusDto.Id;

                    if (eventoDto.StatusDto.Id == StatusDto.EStatus.Confirmada.GetHashCode())
                    {
                        evento.MotivoId = null;
                    }
                }

                if (eventoDto.PacienteDto != null && eventoDto.PacienteDto.Id > 0)
                {
                    evento.PacienteId = eventoDto.PacienteDto.Id;
                }
            }

            _clinicamedicaEntities.SaveChanges();
        }

        private List<EventoDto> Mock()
        {
            var retorno = new List<EventoDto>();

            retorno.Add(new EventoDto() { Id = 1, DataInicial = DateTime.Now.AddDays(1), DataFinal = DateTime.Now.AddDays(1).AddMinutes(15), StatusDto = new StatusDto() { Id = 1 }, PacienteDto = new PacienteDto() { Id = 1, Nome = "Fabio Henrique" } });
            retorno.Add(new EventoDto() { Id = 2, DataInicial = DateTime.Now.AddDays(3), DataFinal = DateTime.Now.AddDays(3).AddMinutes(15), StatusDto = new StatusDto() { Id = 2 }, PacienteDto = new PacienteDto() { Id = 1, Nome = "Fabio Henrique" } });
            retorno.Add(new EventoDto() { Id = 3, DataInicial = DateTime.Now.AddDays(4), DataFinal = DateTime.Now.AddDays(4).AddMinutes(15), StatusDto = new StatusDto() { Id = 1 }, PacienteDto = new PacienteDto() { Id = 2, Nome = "Joana Maria Perreira" } });

            return retorno;
        }
    }
}
