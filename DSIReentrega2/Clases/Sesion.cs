using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Sesion
    {
        private DateTime fechaFin;
        private DateTime fechaInicio;
        private string horaFin;
        private string horaInicio;
        private Usuario usuario;

        public Sesion(DateTime fechaFin, DateTime fechaInicio, string horaFin, string horaInicio, Usuario usuario)
        {
            this.fechaFin = fechaFin;
            this.fechaInicio = fechaInicio;
            this.horaFin = horaFin;
            this.horaInicio = horaInicio;
            this.usuario = usuario;
        }

        public Sesion()
        {

        }

        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public string HoraFin { get => horaFin; set => horaFin = value; }
        public string HoraInicio { get => horaInicio; set => horaInicio = value; }
        internal Usuario Usuario { get => usuario; set => usuario = value; }


        public Empleado getEmpleadoEnSesion()                                                                       //LE PIDE A SU USUARIO QUE OBTENGA SU EMPLEADO ASOCIADO
        {
            Empleado empleadoEnSesion = this.usuario.obtenerEmpleado();
            return empleadoEnSesion;
        }


    }
}
