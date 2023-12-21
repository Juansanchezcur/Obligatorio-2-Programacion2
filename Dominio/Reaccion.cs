using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Sistema;

namespace Dominio
{
    public class Reaccion: IEquatable<Reaccion>, IValidable


    {

        public Sistema.TipoReaccion Tipo { get; set; }
        public Miembro Miembro { get; set; }



        public Reaccion(Miembro miembro, Sistema.TipoReaccion tipo)
        {
            Tipo = tipo;
            Miembro = miembro;
            Validar();
        }

        //VALIDACIÓN
        public void Validar()
        {
            if (Miembro == null)
            {
                throw new Exception("El miembro que autor de la reacción no puede ser nulo");
            }
        }

        public override bool Equals(object obj)
        {
            Reaccion reaccion = (Reaccion)obj;

            return reaccion != null && Miembro == reaccion.Miembro;
        }

        public override string ToString()
        {
            string respuesta = "";
            respuesta += $"Miembro: {Miembro} \n ";
            respuesta += $"Tipo: {Tipo} \n ";
            return respuesta;
        }

        public bool Equals(Reaccion? reaccion)
        {

            return reaccion != null && Miembro == reaccion.Miembro;
        }
    }
}

