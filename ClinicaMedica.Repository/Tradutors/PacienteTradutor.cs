using ClinicaMedica.Dto;
using System;

namespace ClinicaMedica.Repository.Tradutors
{
    public static class PacienteTradutor
    {
        public static Paciente ToBd(this PacienteDto pacienteDto)
        {
            var paciente = new Paciente();

            paciente.Id = pacienteDto.Id;
            paciente.Nome = pacienteDto.Nome;
            paciente.Email = pacienteDto.Email;

            if (!string.IsNullOrEmpty(pacienteDto.Telefone))
            {
                paciente.Telefone = pacienteDto.Telefone.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            }

            if (!string.IsNullOrEmpty(pacienteDto.Cpf))
            {
                paciente.Cpf = pacienteDto.Cpf.Replace(".", "").Replace(",", "").Replace("-", "");
            }

            paciente.Cns = pacienteDto.Cns;

            return paciente;
        }

        public static PacienteDto ToApp(this Paciente paciente)
        {
            var pacienteDto = new PacienteDto();

            pacienteDto.Id = paciente.Id;
            pacienteDto.Nome = paciente.Nome;
            pacienteDto.Email = paciente.Email;

            if (!string.IsNullOrEmpty(paciente.Telefone))
            {
                if (paciente.Telefone.Length > 10)
                {
                    pacienteDto.Telefone = Convert.ToInt64(paciente.Telefone).ToString("(##) #####-####");
                }
                else
                {
                    pacienteDto.Telefone = Convert.ToInt64(paciente.Telefone).ToString("(##) ####-####");
                }
            }

            if (!string.IsNullOrEmpty(paciente.Cpf))
            {
                pacienteDto.Cpf = Convert.ToUInt64(paciente.Cpf).ToString(@"000\.000\.000\-00");
            }

            pacienteDto.Cns = paciente.Cns;

            return pacienteDto;
        }
    }
}
