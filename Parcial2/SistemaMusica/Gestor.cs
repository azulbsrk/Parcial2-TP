
using SistemaMusica.Gestores;
using SistemaMusica.Modelos;


namespace SistemaMusica.Tests
{
    public class GestorCancionesTests
    {
        [Fact]
        public void AgregarCancion_LuegoBuscarPorNombre_EncuentraLaCancion()
        {
            // Arrange
            var gestor = new GestorCanciones();
            var cancion = new Cancion("Imagine", "John Lennon", 183);

            // Act
            gestor.AgregarCancion(cancion);
            List<Cancion> resultados = gestor.BuscarPorNombre("Imagine");

            // Assert
            Assert.Single(resultados);
            Assert.Equal("Imagine", resultados[0].Nombre);
            Assert.Equal("John Lennon", resultados[0].Artista);
            Assert.Equal(183, resultados[0].DuracionSeguntos);
        }

        [Fact]
        public void BuscarPorNombre_EsCaseInsensitive()
        {
            // Arrange
            var gestor = new GestorCanciones();
            gestor.AgregarCancion(new Cancion("Like a Rolling Stone", "Bob Dylan", 369));
            gestor.AgregarCancion(new Cancion("Smells Like Teen Spirit", "Nirvana", 301));

            // Act
            List<Cancion> resultados = gestor.BuscarPorNombre("rolling");

            // Assert
            Assert.Single(resultados);
            Assert.Equal("Like a Rolling Stone", resultados[0].Nombre);
        }

        [Fact]
        public void OrdenarPorDuracion_DejaCancionesEnOrdenAscendente()
        {
            // Arrange
            var gestor = new GestorCanciones();
            var c1 = new Cancion("Larga", "X", 400);
            var c2 = new Cancion("Corta", "Y", 100);
            var c3 = new Cancion("Media", "Z", 250);

            gestor.AgregarCancion(c1);
            gestor.AgregarCancion(c2);
            gestor.AgregarCancion(c3);

            // Act
            gestor.OrdenarPorDuracion();

            List<Cancion> despuesOrdenar = gestor.BuscarPorNombre("");

            // Assert
            Assert.Equal(3, despuesOrdenar.Count);
            Assert.Equal(c2, despuesOrdenar[0]);
            Assert.Equal(c3, despuesOrdenar[1]);
            Assert.Equal(c1, despuesOrdenar[2]); 
        }
    }
}
