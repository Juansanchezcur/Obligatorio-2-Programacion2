using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   
    public class Comentario : Publicacion, IValidable
    {
        public Post Post { get; set; }

        public Comentario() { }
        public Comentario(string titulo, string contenido, Miembro autor, Post post, Sistema.TipoPrivacidad privacidad) : base(titulo, contenido, autor, privacidad)
        {
            Post = post;

        }
        public override bool Equals(object obj)
        {
            Comentario comentario = (Comentario)obj;

            return comentario != null && Titulo == comentario.Titulo && Contenido == comentario.Contenido && comentario.Post == Post;
        }
    }

}
