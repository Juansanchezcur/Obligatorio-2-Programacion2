using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Miembro : Usuario, IValidable, IComparable<Miembro>
    {
        //Propiedaes de los miembros
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }

        private List<Miembro> _amigos = new List<Miembro>();
        public bool Bloqueado { get; set; }

        //Método para obtener la lista de amigos
        public List<Miembro> Amigos
        {
            get { return _amigos; }
        }

        public Miembro() { }

        public Miembro(string email, string contrasena, string nombre, string apellido, DateTime fechaNacimiento, bool bloqueado) : base(email, contrasena)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Bloqueado = bloqueado;
            Validar();
        }

        //VALIDACIONES
        public void Validar()
        {
            base.Validar();
            ValidarNombre();
            ValidarApellido();
        }

        //Validaciones del nombre
        private void ValidarNombre()
        {
            if (!Utilidades.StringValido(Nombre))
            {
                throw new Exception($"Nombre no puede ser un dato vacío");
            }

        }
        
        //Validaciones del apellido
        private void ValidarApellido()
        {
            if (!Utilidades.StringValido(Apellido))
            {
                throw new Exception("Apellido no puede ser un dato vacío");

            }
        }
        
        //Agrega un amigo a la lista de amigos
        public void AgregarAmigo(Miembro miembro)
        {
            if (miembro == null)
            {
                throw new Exception("El amigo a agregar a la lista de amigos no puede ser nulo");
            }
            if (miembro.Email == Email)
            {
                throw new Exception("Un miembro no puede agregarse a si mismo como amigo");
            }
            if (_amigos.Contains(miembro) )
            {
                throw new Exception("El miembro a ingresar ya se encuentra en la lista de amigos");
            }
            _amigos.Add(miembro);
        }


        //Sobreescribimos el método de Calcular VA de la clase padre
        public override double CalcularVA()
        {

            return 0;
        }
        public override string ToString()
        {
            string respuesta = base.ToString();
            respuesta += $"Nombre: {Nombre}\n ";
            respuesta += $"Apellido: {Apellido}\n ";
            respuesta += $"Fecha de Nacimiento: {FechaNacimiento}\n ";
            respuesta += $"Bloqueado: {Bloqueado}\n ";
            respuesta += $"Amigos: {_amigos.Count}\n ";

            return respuesta;


        }

        public int CompareTo(Miembro? other)
        {
            if (other == null)
            {
                return -1;
            }
            int order = Apellido.CompareTo(other.Apellido);
            if (order == 0)
            {
                order = Nombre.CompareTo(other.Nombre);
            }
            return order;
        }
    }
}
