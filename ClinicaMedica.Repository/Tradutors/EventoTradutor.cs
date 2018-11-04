using ClinicaMedica.Dto;

namespace ClinicaMedica.Repository.Tradutors
{
    public static class EventoTradutor
    {
        public static Evento ToBd(this EventoDto eventoDto)
        {
            var evento = new Evento();

            evento.Id = eventoDto.Id;
            evento.DataInicial = eventoDto.DataInicial.ToUniversalTime();
            evento.DataFinal = eventoDto.DataFinal.ToUniversalTime();
            evento.Observacao = eventoDto.Observacao;

            if (eventoDto.PacienteDto != null && eventoDto.PacienteDto.Id > 0)
            {
                evento.PacienteId = eventoDto.PacienteDto.Id;
            }

            if (eventoDto.StatusDto != null && eventoDto.StatusDto.Id > 0)
            {
                evento.StatusId = eventoDto.StatusDto.Id;
            }

            if (eventoDto.MotivoDto != null && eventoDto.MotivoDto.Id > 0)
            {
                evento.MotivoId = eventoDto.MotivoDto.Id;
            }

            return evento;
        }

        public static EventoDto ToApp(this Evento evento)
        {
            var eventoDto = new EventoDto();

            eventoDto.Id = evento.Id;
            eventoDto.DataInicial = evento.DataInicial.ToLocalTime();
            eventoDto.DataFinal = evento.DataFinal.ToLocalTime();
            eventoDto.Observacao = evento.Observacao;

            if (evento.Paciente != null && evento.Paciente.Id > 0)
            {
                eventoDto.PacienteDto = new PacienteDto() { Id = evento.Paciente.Id, Nome = evento.Paciente.Nome, Email = evento.Paciente.Email };
            }

            if (evento.Status != null && evento.Status.Id > 0)
            { 
                eventoDto.StatusDto = new StatusDto() { Id = evento.Status.Id, Descricao = evento.Status.Descricao };
            }

            if (evento.Motivo != null && evento.Motivo.Id > 0)
            {
                eventoDto.MotivoDto = new MotivoDto() { Id = evento.Motivo.Id, Descricao = evento.Motivo.Descricao };
            }

            return eventoDto;
        }
    }
}
