using System;

namespace ClinicaMedica.Dto
{
    public class EventoDto
    {
        public EventoDto()
        {
            PacienteDto = new PacienteDto();
            StatusDto = new StatusDto();
        }

        public int Id { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string Observacao { get; set; }
        public MotivoDto MotivoDto { get; set; }
        public PacienteDto PacienteDto { get; set; }
        public StatusDto StatusDto { get; set; }
    }
}
