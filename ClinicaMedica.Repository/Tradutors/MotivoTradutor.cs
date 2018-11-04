using ClinicaMedica.Dto;

namespace ClinicaMedica.Repository.Tradutors
{
    public static class MotivoTradutor
    {
        public static Motivo ToBd(this MotivoDto motivoDto)
        {
            var motivo = new Motivo();

            motivo.Id = motivoDto.Id;
            motivo.Descricao = motivoDto.Descricao;

            return motivo;
        }

        public static MotivoDto ToApp(this Motivo motivo)
        {
            var motivoDto = new MotivoDto();

            motivoDto.Id = motivo.Id;
            motivoDto.Descricao = motivo.Descricao;

            return motivoDto;
        }
    }
}
