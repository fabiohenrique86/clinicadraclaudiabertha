using ClinicaMedica.Dto;

namespace ClinicaMedica.Repository.Tradutors
{
    public static class MedicacaoTradutor
    {
        public static Medicacao ToBd(this MedicacaoDto medicacaoDto)
        {
            var medicacao = new Medicacao();

            medicacao.Id = medicacaoDto.Id;
            medicacao.Descricao = medicacaoDto.Descricao;
            medicacao.Ativo = medicacaoDto.Ativo;
            medicacao.DataCadastro = medicacaoDto.DataCadastro;
            medicacao.DataExclusao = medicacaoDto.DataExclusao;
            medicacao.PacienteId = medicacaoDto.PacienteDto.Id;

            return medicacao;
        }

        public static MedicacaoDto ToApp(this Medicacao medicacao)
        {
            var medicacaoDto = new MedicacaoDto();

            medicacaoDto.Id = medicacao.Id;
            medicacaoDto.Descricao = medicacao.Descricao;
            medicacaoDto.Ativo = medicacao.Ativo;
            medicacaoDto.DataCadastro = medicacao.DataCadastro;
            medicacaoDto.DataExclusao = medicacao.DataExclusao;

            if (medicacao.Paciente != null)
            {
                medicacaoDto.PacienteDto = new PacienteDto() { Id = medicacao.Paciente.Id, Nome = medicacao.Paciente.Nome };
            }

            return medicacaoDto;
        }
    }
}
