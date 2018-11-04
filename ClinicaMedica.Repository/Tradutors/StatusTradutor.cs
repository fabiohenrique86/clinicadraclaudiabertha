using ClinicaMedica.Dto;

namespace ClinicaMedica.Repository.Tradutors
{
    public static class StatusTradutor
    {
        public static Status ToBd(this StatusDto statusDto)
        {
            var status = new Status();

            status.Id = statusDto.Id;
            status.Descricao = statusDto.Descricao;

            return status;
        }

        public static StatusDto ToApp(this Status status)
        {
            var statusDto = new StatusDto();

            statusDto.Id = status.Id;
            statusDto.Descricao = status.Descricao;

            return statusDto;
        }
    }
}
