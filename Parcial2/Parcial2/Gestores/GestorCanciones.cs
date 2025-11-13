﻿using SistemaMusica.Modelos;

namespace SistemaMusica.Gestores
{
    public class GestorCanciones
    {
        //Atributo
        private List<Cancion> cancionesDisponibles;

        //Constructor
        public GestorCanciones()
        {
            cancionesDisponibles = new List<Cancion>();
        }

        //Metodos
        public void AgregarCancion(Cancion cancion, bool mostrarMensaje = true)
        {
            if (cancion != null)
            {
                cancionesDisponibles.Add(cancion);

                if (mostrarMensaje)
                {
                    Console.WriteLine("Canción agregada: " + cancion.Nombre);
                }
            }
            else
            {
                Console.WriteLine("Error: la canción no puede ser nula.");
            }
        }

        public List<Cancion> BuscarPorNombre(string nombre)
        {
            List<Cancion> resultados = new List<Cancion>();

            for (int i = 0; i < cancionesDisponibles.Count; i++)
            {
                Cancion actual = cancionesDisponibles[i];
               
                if (actual.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    resultados.Add(actual);
                }
            }

            if (resultados.Count == 0)
            {
                Console.WriteLine("No se encontraron canciones con ese nombre.");
            }

            return resultados;
        }

        public void MostrarCancionesDisponibles()
        {
            if (cancionesDisponibles.Count == 0)
            {
                Console.WriteLine("No hay canciones disponibles.");
            }
            else
            {
                Console.WriteLine("=== Canciones Disponibles ===");
                for (int i = 0; i < cancionesDisponibles.Count; i++)
                {
                    Console.WriteLine(cancionesDisponibles[i].ToString());
                }
            }
        }

        public void OrdenarPorDuracion()
        {
            if (cancionesDisponibles.Count > 1)
            {
                QuickSort(cancionesDisponibles, 0, cancionesDisponibles.Count - 1);
                Console.WriteLine("Canciones ordenadas por duración correctamente.");
            }
            else
            {
                Console.WriteLine("No hay suficientes canciones para ordenar.");
            }
        }

        private void QuickSort(List<Cancion> lista, int low, int high)
        {
            if (low < high)
            {
                int pi = Particionar(lista, low, high);
                QuickSort(lista, low, pi - 1);
                QuickSort(lista, pi + 1, high);
            }
        }

        private int Particionar(List<Cancion> lista, int low, int high)
        {
            int pivote = lista[high].DuracionSeguntos;
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (lista[j].DuracionSeguntos < pivote)
                {
                    i++;
                    Intercambiar(lista, i, j);
                }
            }

            Intercambiar(lista, i + 1, high);
            return i + 1;
        }

        private void Intercambiar(List<Cancion> lista, int i, int j)
        {
            Cancion temp = lista[i];
            lista[i] = lista[j];
            lista[j] = temp;
        }
    }
}
