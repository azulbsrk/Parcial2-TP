using SistemaMusica.Gestores;
using SistemaMusica.Modelos;

namespace SistemaMusica.Tests
{
    public class ServicioMusicaTests
    {
        [Fact]
        public void RegistrarUsuario_AgregaUsuarioALaLista()
        {
            // Arrange
            var gestor = new GestorCanciones();
            var servicio = new ServicioMusica(gestor);

            // Act
            Usuario usuario = servicio.RegistrarUsuario("Tester");

            // Assert
            Assert.NotNull(usuario);
            Assert.Single(servicio.Usuarios);
            Assert.Equal("Tester", servicio.Usuarios[0].Nombre);
        }

        [Fact]
        public void BuscarUsuario_IgnoraMayusculasMinusculas()
        {
            // Arrange
            var gestor = new GestorCanciones();
            var servicio = new ServicioMusica(gestor);
            servicio.RegistrarUsuario("MiUsuario");

            // Act
            Usuario encontrado = servicio.BuscarUsuario("miusuario");

            // Assert
            Assert.NotNull(encontrado);
            Assert.Equal("MiUsuario", encontrado.Nombre);
        }

        [Fact]
        public void BuscarUsuario_RegresaNullSiNoExiste()
        {
            // Arrange
            var gestor = new GestorCanciones();
            var servicio = new ServicioMusica(gestor);

            // Act
            Usuario encontrado = servicio.BuscarUsuario("Nadie");

            // Assert
            Assert.Null(encontrado);
        }
    }
}
