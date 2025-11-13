using SistemaMusica.Modelos;
using SistemaMusica.Gestores;

namespace SistemaMusica.Tests
{
    public class CancionTests
    {
        [Fact]
        public void Constructor_AsignaPropiedadesCorrectamente()
        {
            // Arrange
            string nombre = "Bohemian Rhapsody";
            string artista = "Queen";
            int duracion = 354;

            // Act
            var cancion = new Cancion(nombre, artista, duracion);

            // Assert
            Assert.Equal(nombre, cancion.Nombre);
            Assert.Equal(artista, cancion.Artista);
            Assert.Equal(duracion, cancion.DuracionSeguntos);
        }

        [Fact]
        public void ToString_DevuelveFormatoEsperado()
        {
            var cancion = new Cancion("Bohemian Rhapsody", "Queen", 354);

            string texto = cancion.ToString();

            Assert.Equal("Bohemian Rhapsody - Queen (354 segundos)", texto);
        }
    }
}
