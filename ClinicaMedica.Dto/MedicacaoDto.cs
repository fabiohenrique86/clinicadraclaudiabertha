using System;

namespace ClinicaMedica.Dto
{
    public class MedicacaoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public PacienteDto PacienteDto { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}
