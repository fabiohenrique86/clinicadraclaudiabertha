using ClinicaMedica.Dto;

namespace ClinicaMedica.Repository.Tradutors
{
    public static class UsuarioTradutor
    {
        public static Usuario ToBd(this UsuarioDto usuarioDto)
        {
            var usuario = new Usuario();

            usuario.Id = usuarioDto.Id;
            usuario.Login = usuarioDto.Login;
            usuario.Senha = usuarioDto.Senha;

            return usuario;
        }

        public static UsuarioDto ToApp(this Usuario usuario)
        {
            var usuarioDto = new UsuarioDto();

            usuarioDto.Id = usuario.Id;
            usuarioDto.Login = usuario.Login;
            usuarioDto.Senha = usuario.Senha;

            return usuarioDto;
        }
    }
}
