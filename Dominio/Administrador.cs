using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Administrador : Usuario, IValidable
    {
        public Administrador() { } 
        public Administrador(string email, string contrasena) : base(email, contrasena)
        {

        }
        
        //Sobreescribimos el método de Calcular VA de la clase padre
        public override double CalcularVA()
        {

            return 0;
        }
    }

}
