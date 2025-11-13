
using SistemaMusica.Gestores;
using SistemaMusica.Modelos;
using System.Linq.Expressions;

// Crear gestor y servicio
GestorCanciones gestor = new GestorCanciones();
ServicioMusica servicio = new ServicioMusica(gestor);

// Canciones de ejemplo
gestor.AgregarCancion(new Cancion("Imagine", "John Lennon", 183), false);
gestor.AgregarCancion(new Cancion("Bohemian Rhapsody", "Queen", 354), false);
gestor.AgregarCancion(new Cancion("Billie Jean", "Michael Jackson", 294), false);
gestor.AgregarCancion(new Cancion("Hotel California", "Eagles", 391), false);
gestor.AgregarCancion(new Cancion("Like a Rolling Stone", "Bob Dylan", 369), false);
gestor.AgregarCancion(new Cancion("Smells Like Teen Spirit", "Nirvana", 301), false);
gestor.AgregarCancion(new Cancion("No Control", "One Direction", 230), false);
gestor.AgregarCancion(new Cancion("Aluminio", "Peces Raros", 310), false);

Console.WriteLine("=== Bienvenido al Sistema de Música Simple ===");
Console.WriteLine();

// --- REGISTRO DE USUARIO ---
Console.Write("Ingrese su nombre de usuario: ");
string nombreUsuario = Console.ReadLine();

Usuario usuario = servicio.RegistrarUsuario(nombreUsuario);
if (usuario == null)
{
    Console.WriteLine("No se pudo registrar el usuario. Saliendo del programa.");
    return;
}

Console.WriteLine();
Console.WriteLine($"¡Bienvenido, {usuario.Nombre}!");
Console.WriteLine();

// --- CREACIÓN DE LISTA DE REPRODUCCIÓN ---
string listaActual = "";

while (true)
{
    Console.Write("Ingrese un nombre para su primera lista de reproducción: ");
    string nombreLista = Console.ReadLine();

    bool creada = usuario.CrearListaReproduccion(nombreLista);
    if (creada)
    {
        listaActual = nombreLista;
        break;
    }

    Console.WriteLine("Intente con otro nombre.");
}

// --- MENÚ PRINCIPAL ---
bool salir = false;

while (!salir)
{
    try
    { 
    Console.WriteLine();
    Console.WriteLine("=== MENÚ PRINCIPAL ===");
    Console.WriteLine("Usuario actual: " + usuario.Nombre);

    int cancionesEnLista = 0;
    if (!string.IsNullOrEmpty(listaActual) &&
        usuario.ListasReproduccion.ContainsKey(listaActual))
    {
        cancionesEnLista = usuario.ListasReproduccion[listaActual].Count;
    }

    Console.WriteLine($"Lista actual: '{listaActual}' ({cancionesEnLista} canciones)");
    Console.WriteLine("1. Buscar canciones para agregar a mi lista");
    Console.WriteLine("2. Ver mi lista de reproducción (ordenada por duración)");
    Console.WriteLine("3. Ver todas las canciones disponibles");
    Console.WriteLine("4. Crear nueva lista de reproducción");
    Console.WriteLine("5. Cambiar de lista actual");
    Console.WriteLine("6. Salir");
    Console.Write("Seleccione una opción: ");

    string opcionTexto = Console.ReadLine();
    Console.WriteLine();

    if (!int.TryParse(opcionTexto, out int opcion))
    {
        Console.WriteLine("Opción inválida. Intente de nuevo.");
        continue;
    }

    switch (opcion)
    {
        case 1:
            BuscarYAgregarCancion(gestor, usuario, listaActual);
            break;

        case 2:
            MostrarListaOrdenada(usuario, listaActual);
            break;

        case 3:
            gestor.MostrarCancionesDisponibles();
            break;

        case 4:
            CrearNuevaLista(usuario, ref listaActual);
            break;

        case 5:
            CambiarListaActual(usuario, ref listaActual);
            break;

        case 6:
            salir = true;
            break;

        default:
            Console.WriteLine("Opción inválida. Intente de nuevo.");
            break;
    }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ocurrió un error: " + ex.Message);
    }
}

Console.WriteLine("Gracias por usar el Sistema de Música. ¡Hasta luego!");



static void BuscarYAgregarCancion(GestorCanciones gestor, Usuario usuario, string listaActual)
{
    if (string.IsNullOrEmpty(listaActual) ||
        !usuario.ListasReproduccion.ContainsKey(listaActual))
    {
        Console.WriteLine("No hay una lista actual válida.");
        return;
    }

    Console.Write("Ingrese parte del nombre de la canción a buscar: ");
    string termino = Console.ReadLine();

    List<Cancion> resultados = gestor.BuscarPorNombre(termino);

    if (resultados.Count == 0)
    {
        return;
    }

    Console.WriteLine("Resultados de la búsqueda:");
    for (int i = 0; i < resultados.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {resultados[i]}");
    }

    Console.Write("Seleccione el número de la canción a agregar (0 para cancelar): ");
    string seleccionTexto = Console.ReadLine();

    if (!int.TryParse(seleccionTexto, out int seleccion) ||
        seleccion < 0 || seleccion > resultados.Count)
    {
        Console.WriteLine("Selección inválida.");
        return;
    }

    if (seleccion == 0)
    {
        Console.WriteLine("Operación cancelada.");
        return;
    }

    Cancion elegida = resultados[seleccion - 1];
    usuario.AgregarCancionALista(listaActual, elegida);
}

static void MostrarListaOrdenada(Usuario usuario, string listaActual)
{
    if (string.IsNullOrEmpty(listaActual) ||
        !usuario.ListasReproduccion.ContainsKey(listaActual))
    {
        Console.WriteLine("No hay una lista actual válida.");
        return;
    }

    List<Cancion> lista = usuario.ListasReproduccion[listaActual];

    if (lista.Count == 0)
    {
        Console.WriteLine("La lista actual está vacía.");
        return;
    }

    List<Cancion> copia = new List<Cancion>(lista);
    OrdenarPorDuracionSimple(copia);

    Console.WriteLine($"Lista '{listaActual}' (ordenada por duración):");
    for (int i = 0; i < copia.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {copia[i]}");
    }

    int totalSegundos = CalcularDuracionTotal(copia);
    Console.WriteLine($"Duración total: {totalSegundos} segundos");
}

static void OrdenarPorDuracionSimple(List<Cancion> lista)
{
    for (int i = 0; i < lista.Count - 1; i++)
    {
        for (int j = i + 1; j < lista.Count; j++)
        {
            if (lista[j].DuracionSeguntos < lista[i].DuracionSeguntos)
            {
                Cancion temp = lista[i];
                lista[i] = lista[j];
                lista[j] = temp;
            }
        }
    }
}

static int CalcularDuracionTotal(List<Cancion> lista)
{
    int total = 0;
    for (int i = 0; i < lista.Count; i++)
    {
        total += lista[i].DuracionSeguntos;
    }
    return total;
}

static void CrearNuevaLista(Usuario usuario, ref string listaActual)
{
    Console.Write("Nombre de la nueva lista de reproducción: ");
    string nombreLista = Console.ReadLine();

    bool creada = usuario.CrearListaReproduccion(nombreLista);
    if (creada)
    {
        Console.Write("¿Desea usar esta lista como lista actual? (s/n): ");
        string respuesta = Console.ReadLine();
        if (respuesta != null && respuesta.ToLower() == "s")
        {
            listaActual = nombreLista;
            Console.WriteLine($"Lista actual cambiada a '{listaActual}'.");
        }
    }
}

static void CambiarListaActual(Usuario usuario, ref string listaActual)
{
    if (usuario.ListasReproduccion.Count == 0)
    {
        Console.WriteLine("No hay listas de reproducción disponibles.");
        return;
    }

    Console.WriteLine("Listas disponibles:");
    foreach (var par in usuario.ListasReproduccion)
    {
        Console.WriteLine($"- {par.Key} ({par.Value.Count} canciones)");
    }

    Console.Write("Ingrese el nombre de la playlist a seleccionar: ");
    string nombre = Console.ReadLine();

    if (!usuario.ListasReproduccion.ContainsKey(nombre))
    {
        Console.WriteLine("La playlist no existe.");
    }
    else
    {
        listaActual = nombre;
        Console.WriteLine($"Playlist cambiada a '{listaActual}'.");
    }
}
