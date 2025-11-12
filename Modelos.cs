//Clase Cancion (Modelos)
public class Cancion
{
    public string Nombre { get; }
    public string Artista { get; }
    public int DuracionSeguntos { get; }

    public Cancion(string nombre, string artista, int duracionSeguntos)
    {
        Nombre = nombre;
        Artista = artista;
        DuracionSeguntos = duracionSeguntos;
    }

    public override string ToString()
    {
        return $"{Nombre} - {Artista} ({DuracionSeguntos} segundos)";
    }
}

//Clase Usuario (Modelos)
public class Usuario
{
    public string Nombre { get; }
    public Dictionary<string,List<Cancion>> ListasReproduccion { get; }

    public Usuario(string nombre)
    {
        Nombre = nombre;
        ListasReproduccion = new Dictionary<string, List<Cancion>>();
    }

    //Crear una lista de reproduccion y verificar si ya existe
    public bool CrearListaReproduccion(string nombreLista)
    {
        if(string.IsNullOrWhiteSpace(nombreLista))
        {
            Console.WriteLine("El nombre de la lista no puede estar vacío.");
            return false;
        }

        if (ListasReproduccion.ContainsKey(nombreLista))
        {
            Console.WriteLine("La lista de reproducción ya existe.");
            return false;
        }

        ListasReproduccion[nombreLista] = new List<Cancion>();
        Console.WriteLine($"Lista de reproducción '{nombreLista}' creada.");
        return true;
    }

    //Agregar una cancion a una lista de reproduccion
    public bool AgregarCancionALista(string nombreLista, Cancion cancion)
    {
        if (!ListasReproduccion.ContainsKey(nombreLista))
        {
            Console.WriteLine("La lista de reproducción no existe.");
            return false;
        }
        ListasReproduccion[nombreLista].Add(cancion);
        Console.WriteLine($"Canción '{cancion.Nombre}' agregada a la lista '{nombreLista}'.");
        return true;
    }

    public void MostrarListasReproduccion()
    {
        if (ListasReproduccion.Count == 0)
        {
            Console.WriteLine("No hay listas de reproducción.");
            return;
        }
        foreach (var lst in ListasReproduccion)
        {
            Console.WriteLine($"Lista: {lst.Key}");
            var lista=lst.Value;
            if (lista.Count == 0)
            {
                Console.WriteLine("  (Vacía)");
            }
            else
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {lista[i]}");
                }
            }
        }
    }
}