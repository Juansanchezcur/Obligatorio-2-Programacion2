using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public static class Utilidades
    {
        //Revisa que el string ingresado no sea nulo ni vacío
        public static bool StringValido(string texto)
        {
            return !string.IsNullOrEmpty(texto);
        }



        //Valida que el mail sea válido, generado con la ayuda de ChatGPT
        public static bool ValidarFormatoEmail(string email)
        {
            //Patrón que debe cumplir el email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Se fija si el email ingresado cumple con el patrón y devuelve el resultado
            return Regex.IsMatch(email, pattern);
        }




    }
}
