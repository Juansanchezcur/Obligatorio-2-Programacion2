using Dominio;

namespace AppConsola
{
    internal class Program
    {
        //Instancio el sistema
        static private Sistema _sistema = new Sistema();

        static void Main(string[] args)
        {
            //Se precargan los datos
            Precarga();

            //Se despliega el menú
            int opcion = 0;
            do
            {


                Console.Clear();
                Console.WriteLine
                        (
                        "-------------------------------------\n" +
                        "Ingrese opcion:\n\n" +

                        "1-Alta de miembro\n" +
                        "2-Lista de publicaciones por miembro\n" +
                        "3-Lista de posts con comentarios, por miembro\n" +
                        "4-Lista de posts entre fechas\n" +
                        "5-Lista de miembros con más publicaciones\n\n" +
                        "0-salir" +
                        "-------------------------------------\n\n"
                        );

                //Opciones del menú
                opcion = PedirNumero();
                switch (opcion)
                {
                    case 0:
                        break;
                    case 1:
                        AltaMiembro();
                        break;
                    case 2:
                        PublicacionesPorMiembro();
                        break;
                    case 3:
                        PostsConComentarios();
                        break;
                    case 4:
                        PostsEntreFechas();
                        break;
                    case 5:
                        ListarMiembrosConMasPublicaciones();
                        break;
                    default:
                        Console.WriteLine($"\n\n{opcion} no es una opción válida.\nPor favor ingrese una opción correcta");
                        Console.ReadKey();
                        break;
                }

            } while (opcion != 0);

        }


        //Precarga de datos
        private static void Precarga()
        {
            try
            {
                _sistema.Precargar();
                Console.WriteLine("Precarga finalizada con éxito");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }

        //Opción 1
        //Da de alta un miembro
        private static void AltaMiembro()
        {
            try
            {
                //Pedimos los datos necesarios para crear un miembro
                Console.WriteLine("\nEscribe tu email:");
                string email = PedirEmail();
                Console.WriteLine("\nEscribe tu contraseña:");
                string contrasena = PedirTexto();
                Console.WriteLine("\nEscribe tu nombre:");
                string nombre = PedirTexto();
                Console.WriteLine("\nEscribe tu apellido:");
                string apellido = PedirTexto();
                Console.Clear();
                Console.WriteLine("\nEscribe tu fecha de nacimiento:\n\n");
                DateTime fechaNacimiento = PedirFecha();

                //Creamos el miembro, lo agregamos al sistema y mostramos un mensaje satisfactorio con los datos dele mismo
                Miembro miembro = new Miembro(email, contrasena, nombre, apellido, fechaNacimiento, false);
                _sistema.AgregarUsuario(miembro);
                Console.Clear();
                Console.WriteLine("Miembro agregado con éxito\n\n");
                Console.WriteLine(miembro);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        //Opcion 2
        //Lista las publicaciones que tiene un miembro
        private static void PublicacionesPorMiembro()
        {
            try
            {//Pedimos los datos del miembro y lo buscamos
                Console.WriteLine("Escribe el email del miembro:");
                string email = PedirEmail();
                Miembro miembro = _sistema.BuscarMiembroPorEmail(email);

                //Validamos el miembro
                if (miembro == null)
                {
                    throw new Exception($"No existe miembro para el email: {email}");
                }
                //Buscamos las publicaciones para ese miembro
                List<Publicacion> publicacionesPorMiembro = _sistema.ListarPublicacionesPorMiembro(miembro);

                //Si tiene publicaciones las mostramos
                if (publicacionesPorMiembro.Count() == 0)
                {
                    throw new Exception("No hay publicaciones para este miembro");
                }
                Console.WriteLine($"Publicaciones del miembro de email {miembro.Email}:\n\n");
                foreach (Publicacion publicacion in publicacionesPorMiembro)
                {
                    Console.WriteLine(publicacion);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            Console.ReadKey();
        }

        //Opción 3
        //Listas todos los posts que tienen comentarios de un miembro
        private static void PostsConComentarios()
        {
            try
            {
                //Pedimos el mail del miembro
                Console.WriteLine("Escribe el email del miembro:");
                string email = PedirEmail();

                //buscamos el miembro
                Miembro miembro = _sistema.BuscarMiembroPorEmail(email);

                //Validamos el miembro
                if (miembro == null)
                {
                    throw new Exception($"No existe miembro para el email :{email}");
                }

                //Si existen datos los mostramos
                List<Post> postsConComentarios = _sistema.ListarPostsConComentarios(miembro);
                if (postsConComentarios.Count() == 0)
                {
                    throw new Exception("No hay Posts con comentarios para este miembro");
                }
                Console.WriteLine($"Posts con comentarios del miembro con email: {miembro.Email}:\n\n");
                foreach (Post post in postsConComentarios)
                {
                    Console.WriteLine(post);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            Console.ReadKey();
        }

        //Opción 4
        //Lista los posts entre dos fechas
        private static void PostsEntreFechas()
        {
            try
            {//Pedimos las fechas
                Console.WriteLine("Escribe la primer fecha:");
                DateTime fecha1 = PedirFecha();
                Console.WriteLine("Escribe la segunda fecha:");
                DateTime fecha2 = PedirFecha();

                //Validamos que no sean iguales
                if (fecha1 == fecha2)
                {
                    throw new Exception("Ambas fechas no pueden ser iguales");
                }
                //Ordenamos las fechas para que siempre la fecha 1 sea la más antigua
                if (fecha1 > fecha2)
                {
                    DateTime aux;
                    aux = fecha1;
                    fecha1 = fecha2;
                    fecha2 = aux;
                }
                //buscamos los posts entre las fechas
                List<Post> postsEntreFechas = _sistema.ListarPostsEntreFechas(fecha1, fecha2);

                //Si existen datos los mostramos
                if (postsEntreFechas.Count() == 0)
                {
                    throw new Exception($"No hay Posts entre las fechas {fecha1} y {fecha2}");
                }
                Console.WriteLine($"Posts entre las fechas {fecha1} y {fecha2} :\n\n");
                foreach (Post post in postsEntreFechas)
                {
                    string mensaje = "";
                    mensaje += $"ID:{post.Id}\n";
                    mensaje += $"Fecha:{post.Fecha}\n";
                    mensaje += $"Titulo:{post.Titulo}\n";
                    if (post.Contenido.Length >= 50)
                    {
                        mensaje += $"Contenido:{post.Contenido.Substring(0, 50)}\n";
                    }
                    else
                    {
                        mensaje += $"Contenido:{post.Contenido}\n";
                    }
                    Console.WriteLine(mensaje);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }

        //Opción 5
        //Lista los miembros que han hecho más publicaciones
        private static void ListarMiembrosConMasPublicaciones()
        {
            try
            {//Busca los miembros con más publicaciones
                List<Miembro> miembrosConMasPublicaciones = _sistema.MiembrosConMasPublicaciones();
                //Valida que existan datos
                if (miembrosConMasPublicaciones.Count() == 0)
                {
                    throw new Exception("Todavia no hay publicaciones");
                }
                //Si hay datos los muestra
                Console.Clear();
                Console.WriteLine("Miembros con más publicaciones:\n\n");
                foreach (Miembro miembro in miembrosConMasPublicaciones)
                {
                    Console.WriteLine(miembro);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
        //AUXILIARES

        //Funcion auxiliar para pedir un número
        private static int PedirNumero()
        {
            int numero = 0;
            bool salir = false;
            do
            {
                try
                {
                    salir = true;
                    numero = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    salir = false;
                    Console.WriteLine("\nSolo debe ingresar numeros.\n");
                }
            } while (!salir);
            return numero;
        }
        //Funcion auxiliar para pedir un email
        private static string PedirEmail()
        {
            string email = "";
            bool salir = false;
            do
            {
                try
                {
                    email = Console.ReadLine().ToLower();
                    salir = true;
                    if (!Utilidades.ValidarFormatoEmail(email))
                    {
                        throw new Exception("\nFormato de email inválido, por favor ingrese un email Correcto.\n");
                    };

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    salir = false;

                }
            } while (!salir);
            return email;
        }

        //Funcion auxiliar para pedir un texto
        private static string PedirTexto()
        {
            string texto = "";
            bool salir = false;
            do
            {
                try
                {
                    texto = Console.ReadLine().ToLower();
                    salir = true;
                    if (!Utilidades.StringValido(texto))
                    {
                        throw new Exception("\nPor favor ingrese un texto no vacío. \n");
                    };

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    salir = false;

                }
            } while (!salir);
            return texto;
        }

        //Funcion auxiliar para pedir una fecha
        private static DateTime PedirFecha()
        {
            int dia = 0;
            int mes = 0;
            int anio = 0;
            bool salir = false;
            DateTime fecha;
            ;

            do
            {
                try
                {
                    salir = true;

                    Console.WriteLine("\nEscribe el día:");
                    dia = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nEscribe el mes:");
                    mes = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nEscribe el año:");
                    anio = int.Parse(Console.ReadLine());

                    DateTime fechaAux = new DateTime(anio, mes, dia);
                }
                catch (Exception)
                {
                    salir = false;
                    Console.Clear();
                    Console.WriteLine("\nIngrese una fecha correcta \n");
                }
            } while (!salir);
            fecha = new DateTime(anio, mes, dia);
            return fecha;
        }

    }
}