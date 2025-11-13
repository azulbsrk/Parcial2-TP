using SistemaMusica.Modelos;

namespace SistemaMusica.Tests
{
    public class UsuarioTests
    {
        [Fact]
        public void CrearListaReproduccion_CreaEntradaEnDiccionario()
        {
            // Arrange
            var usuario = new Usuario("Teo");

            // Act
            bool creada = usuario.CrearListaReproduccion("Favoritos");

            // Assert
            Assert.True(creada);
            Assert.True(usuario.ListasReproduccion.ContainsKey("Favoritos"));
            Assert.NotNull(usuario.ListasReproduccion["Favoritos"]);
        }

        [Fact]
        public void AgregarCancionALista_AgregaCancionALaListaCorrecta()
        {
            // Arrange
            var usuario = new Usuario("Teo");
            usuario.CrearListaReproduccion("Favoritos");
            var cancion = new Cancion("Imagine", "John Lennon", 183);

            // Act
            bool agregado = usuario.AgregarCancionALista("Favoritos", cancion);

            // Assert
            Assert.True(agregado);
            Assert.Contains(cancion, usuario.ListasReproduccion["Favoritos"]);
        }
    }
}
