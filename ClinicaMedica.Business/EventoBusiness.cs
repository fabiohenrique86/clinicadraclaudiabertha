using ClinicaMedica.Business.Exceptions;
using ClinicaMedica.Dto;
using ClinicaMedica.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace ClinicaMedica.Business
{
    public class EventoBusiness
    {
        private EventoRepository eventoRepository;

        public EventoBusiness()
        {
            eventoRepository = new EventoRepository();
        }

        private void ValidarInserir(EventoDto eventoDto)
        {
            if (eventoDto == null)
            {
                throw new BusinessException("EventoDto é obrigatório");
            }

            if (eventoDto.DataInicial == DateTime.MinValue)
            {
                throw new BusinessException("Data Inicial é obrigatória");
            }

            if (eventoDto.DataFinal == DateTime.MinValue)
            {
                throw new BusinessException("Data Final é obrigatória");
            }

            var evento = Listar(new EventoDto() { DataInicial = eventoDto.DataInicial.ToUniversalTime(), DataFinal = eventoDto.DataFinal.ToUniversalTime() }).FirstOrDefault();

            if (evento != null)
            {
                throw new BusinessException("Já foi cadastrada agenda para o período " + evento.DataInicial.ToString("dd/MM/yyyy HH:mm") + " às " + evento.DataFinal.ToString("HH:mm"));
            }
        }

        private void ValidarAtualizar(EventoDto eventoDto)
        {
            if (eventoDto == null)
            {
                throw new BusinessException("EventoDto é obrigatório");
            }

            if (eventoDto.Id <= 0)
            {
                throw new BusinessException("Id é obrigatório");
            }

            if (eventoDto.StatusDto != null && eventoDto.StatusDto.Id > 0 && eventoDto.StatusDto.Id == StatusDto.EStatus.Cancelada.GetHashCode())
            {
                if (eventoDto.MotivoDto == null || eventoDto.MotivoDto.Id <= 0)
                {
                    throw new BusinessException("Motivo do cancelamento é obrigatório");
                }
            }

            var evento = Listar(new EventoDto() { Id = eventoDto.Id });

            if (evento == null)
            {
                throw new BusinessException("Evento " + eventoDto.Id + " não foi encontrado");
            }
        }

        public void Inserir(EventoDto eventoDto)
        {
            try
            {
                ValidarInserir(eventoDto);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromMinutes(5) }))
                {
                    var dt = eventoDto.DataFinal.Subtract(eventoDto.DataInicial);
                    var ts = new TimeSpan(dt.Hours, dt.Minutes, dt.Seconds).TotalMinutes / 15;

                    for (int i = 0; i < ts; i++)
                    {
                        eventoDto.DataFinal = eventoDto.DataInicial.AddMinutes(15);
                        eventoRepository.Inserir(eventoDto);
                        eventoDto.DataInicial = eventoDto.DataInicial.AddMinutes(15);
                    }

                    scope.Complete();
                }
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

        public List<EventoDto> Listar(EventoDto eventoDto = null)
        {
            try
            {
                return eventoRepository.Listar(eventoDto);
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

        public void Atualizar(EventoDto eventoDto)
        {
            try
            {
                ValidarAtualizar(eventoDto);

                eventoRepository.Atualizar(eventoDto);
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
