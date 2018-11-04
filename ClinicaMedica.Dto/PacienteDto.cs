using System.Collections.Generic;

namespace ClinicaMedica.Dto
{
    public class PacienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public string Cns { get; set; }
        public List<EventoDto> EventosDto { get; set; }
        public List<MedicacaoDto> MedicacoesDto { get; set; }

        public bool IsPaciente { get; set; }
    }
}
