using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Invitacion : IValidable, IAutomaticId
    {
        public int Id { get; private set; }
        private static int ultimoId;
        public Miembro MiembroSolicitante { get; set; }
        public Miembro MiembroSolicitado { get; set; }
        public Sistema.EstadoInvitacion Estado { get; set; }

        public Invitacion() {
            Id = ++ultimoId;
        }
        public Invitacion(Miembro miembroSolicitante, Miembro miembroSolicitado, Sistema.EstadoInvitacion estado)
        {
            Id = ++ultimoId;
            MiembroSolicitante = miembroSolicitante;
            MiembroSolicitado = miembroSolicitado;
            Estado = estado;
            Validar();

        }

        //VALIDACIONES
        public void Validar()
        {
            ValidarMiembroSolicitante();
            ValidarMiembroSolicitado();
            ValidarMiembrosDistintos();
        }
        //Validamos que los miembros no sean iguales
        private void ValidarMiembrosDistintos()
        {
            if (MiembroSolicitado == MiembroSolicitante)
            { throw new Exception("Un miembro no puede enviarse una solicitud de amistad a si mismo"); }
        }
        //Validaciones del miembro solicitante
        private void ValidarMiembroSolicitante()
        {
            ValidarMiembroSolicitanteNoNulo();
            ValidarMiembroSolicitanteNoBloqueado();

        }

        private void ValidarMiembroSolicitanteNoNulo()
        {
            if (MiembroSolicitante == null)
            {
                throw new Exception("El miembro solicitante no puede ser nulo o administrador");
            }
        }
        private void ValidarMiembroSolicitanteNoBloqueado()
        {
            if (MiembroSolicitante.Bloqueado == true)
            {
                throw new Exception("El miembro solicitante fué bloqueado por un administrador");
            }
        }

        //Validaciones del miembro solicitado
        private void ValidarMiembroSolicitado()
        {
            ValidarMiembroSolicitadoNoNulo();
            ValidarMiembroSolicitadoNoBloqueado();
        }



        private void ValidarMiembroSolicitadoNoNulo()
        {
            if (MiembroSolicitado == null)
            {
                throw new Exception("El miembro solicitado no puede ser nulo o administrador");
            }
        }

        private void ValidarMiembroSolicitadoNoBloqueado()
        {
            if (MiembroSolicitado.Bloqueado == true)
            {
                throw new Exception("El miembro solicitado fué bloqueado por un administrador");
            }
        }

        //Acepta una solicitud de amistad, cambia su estado
        public void AceptarSolicitud()
        {
            Estado = Sistema.EstadoInvitacion.APROBADA;
        }
        //Rechaza una solicitud de amistad, cambia su estado
        internal void RechazarSolicitud()
        {
            Estado = Sistema.EstadoInvitacion.RECHAZADA;
        }

        public override bool Equals(object obj)
        {
            Invitacion invitacion = (Invitacion)obj;


            return invitacion != null &&
                ((invitacion.MiembroSolicitante == MiembroSolicitante &&
                invitacion.MiembroSolicitado == MiembroSolicitado)
                || (
                invitacion.MiembroSolicitado == MiembroSolicitante &&
                 invitacion.MiembroSolicitante == MiembroSolicitado))
                ;
        }
        public override string ToString()
        {
            string respuesta = "";
            respuesta += $"ID: {Id} \n ";
            respuesta += $"Miembro Solicitante: {MiembroSolicitante.Email} \n ";
            respuesta += $"Miembro Solicitado: {MiembroSolicitado.Email} \n ";
            respuesta += $"Estado: {Estado} \n ";
            return respuesta;
        }

       
    }
}