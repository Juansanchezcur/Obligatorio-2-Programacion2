using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Usuario:IValidable
    {


        public string Email { get; set; }
        public string Contrasena { get; set; }

        public Usuario() { }

        public Usuario(string email, string contrasena)
        {
            Email = email;
            Contrasena = contrasena;
            Validar();
        }

        //VALIDACIONES
        public virtual void Validar()
        {
            ValidarEmail();
            ValidarContrasena();
        }

        //Validación de email
        private void ValidarEmail()
        {
            if (!Utilidades.StringValido(Email))
            {
                throw new Exception("Email no puede ser un dato vacío");

            }
            if (!Utilidades.ValidarFormatoEmail(Email))
            {
                throw new Exception("Formato de email inválido");
            }
        }

        //Validación de contraseña
        private void ValidarContrasena()
        {
            if (!Utilidades.StringValido(Contrasena))
            {
                throw new Exception("Constraseña no puede ser un dato vacío");

            }
        }

        //Método abstracto que se pisa en los hijos
        public abstract double CalcularVA();

        public override bool Equals(object obj)
        {
            Usuario usuario = (Usuario)obj;

            return usuario != null && Email == usuario.Email;
        }

        public override string ToString()
        {
            string respuesta = "";
            respuesta += $"Email: {Email} \n ";
            respuesta += $"Tipo: {this.GetType().Name} \n ";
            return respuesta;
        }
    }

}
