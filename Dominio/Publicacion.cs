using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Publicacion: IValidable, IAutomaticId
    {
        public int Id { get; private set; }
        private static int ultimoId;
        public string Titulo { get; set; }
        public Miembro Autor { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public Sistema.TipoPrivacidad Privacidad { get; set; }
        private List<Reaccion> _reacciones = new List<Reaccion>();

        public List<Reaccion> Reacciones
        {
            get { return _reacciones; }
        }

        public Publicacion() {
            Id = ++ultimoId;
        }
        public Publicacion(string titulo, string contenido, Miembro autor, Sistema.TipoPrivacidad privacidad)

        {
            Id = ++ultimoId;
            Autor = autor;
            Titulo = titulo;
            Contenido = contenido;
            Fecha = DateTime.Now;
            Privacidad = privacidad;
            Validar();
        }
        //Este segundo constructor lo usamos solamente para la precarga (para tener diferentes fechas de publicacion de los posts, ya que por letra usamos siempre el primero, tomando en cuenta que dice que la fecha al cargar un post es siempre la fecha actual

        public Publicacion(string titulo, string contenido, Miembro autor, Sistema.TipoPrivacidad privacidad, DateTime fecha)

        {
            Id = ++ultimoId;
            Autor = autor;
            Titulo = titulo;
            Contenido = contenido;
            Fecha = fecha;
            Privacidad = privacidad;
            Validar();
        }

        //VALIDACIONES
        public virtual void Validar()
        {
            ValidarAutor();
            ValidarTitulo();
            ValidarContenido();
        }


        //Validación del autor
        private void ValidarAutor()
        {
            if (Autor == null)
            {
                throw new Exception("El autor de la publicación no puede ser nulo")
                ;
            }
            Autor.Validar();
        }
        //Validación del título
        private void ValidarTitulo()
        {
            ValidarTituloNoVacio();
            ValidarTituloCantCaracteres();
        }

        private void ValidarTituloNoVacio()
        {
            if (!Utilidades.StringValido(Titulo))
            {
                throw new Exception("El Título de la publicación no puede ser vacío");
            };
        }

        private void ValidarTituloCantCaracteres()
        {
            if (Titulo.Length < 3)
            {
                throw new Exception("El Título no puede contener menos de 3 caracteres");
            }
        }

        //Validación del contenido
        private void ValidarContenido()
        {
            if (!Utilidades.StringValido(Contenido))
            {
                throw new Exception("El contenido de la publicación no puede ser vacío");
            };
        }

        //Agrega una reacción a la publicacion
        public void AgregarReacccion(Reaccion reaccion)
        {
            if (reaccion == null)
            {

                throw new Exception("La reaccion a agregar no puede ser vacía");
            }
            if (!_reacciones.Contains(reaccion))
            {
                _reacciones.Add(reaccion);
            }
            else
            {
                _reacciones.Remove(reaccion);
                _reacciones.Add(reaccion);
            }

        }

        //Revisa que ya no existe, para la publicación, una reacción del mismo miembro
        public bool VerSiTieneReaccion(Reaccion reaccion)
        {
            return _reacciones.Contains(reaccion);
        }

        public int CantidadLikes()
        {
            int aux = 0;
            foreach(Reaccion reaccion in _reacciones)
            {
                if (reaccion.Tipo == Sistema.TipoReaccion.Like)
                {
                    aux++;
                }
            }
            return aux;
        }

        public int CantidadDislikes()
        {
            int aux = 0;
            foreach (Reaccion reaccion in _reacciones)
            {
                if (reaccion.Tipo == Sistema.TipoReaccion.Dislike)
                {
                    aux++;
                }
            }
            return aux;
        }
        public virtual int calcularVA()
        {
            return CantidadLikes() *5 - CantidadDislikes()*2;
        }

        public override bool Equals(object obj)
        {
            Publicacion publicacion = (Publicacion)obj;

            return publicacion != null && Titulo == publicacion.Titulo && Contenido == publicacion.Contenido;
        }
        public override string ToString()
        {
            string respuesta = "";
            respuesta += $"ID: {Id} \n ";
            respuesta += $"Autor: {Autor.Email} \n ";
            respuesta += $"Título: {Titulo} \n ";
            respuesta += $"Tipo: {this.GetType().Name} \n ";
            respuesta += $"Contenido: {Contenido} \n ";
            respuesta += $"Fecha: {Fecha} \n ";
            respuesta += $"Privacidad: {Privacidad} \n ";
            respuesta += $"Reacciones: {Reacciones.Count} \n ";
            return respuesta;
        }
    }
}