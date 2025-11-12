public class ServicioMusica
{
    public GestorCanciones Gestor { get; }
    public List<Usuario> Usuarios { get; }

    public ServicioMusica(GestorCanciones gestor)
    {
        Gestor= gestor??throw new ArgumentNullException(nameof(gestor));
        Usuarios = new List<Usuario>();
    }

    public Usuario RegistrarUsuario(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre del usuario no puede estar vacío.");
            return null;
        }

        var UsuarioExistente=BuscarUsuario(nombre);
        if (UsuarioExistente != null)
        {
            Console.WriteLine($"El usuario {nombre} ya está registrado.");
            return UsuarioExistente;
        }

        var usuario = new Usuario(nombre);
        Usuarios.Add(usuario);
        Console.WriteLine($"Usuario '{nombre}' registrado exitosamente.");
        return usuario;
    }

    public Usuario BuscarUsuario(string nombre)
    {
        if(string.IsNullOrWhiteSpace(nombre)) return null;
        return Usuarios.FirstOrDefault(u => u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }
}