
namespace ClinicaMedica.Dto
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public enum EStatus
        {
            Programada = 1,
            Confirmada = 2,
            Cancelada = 3
        }
    }
}
