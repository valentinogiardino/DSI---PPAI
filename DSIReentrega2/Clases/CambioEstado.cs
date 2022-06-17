using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class CambioEstado
    {
        private DateTime fechaHoraFin;
        private DateTime fechaHoraInicio;
        private Estado estado;

        public CambioEstado(DateTime fechaHoraFin, DateTime fechaHoraInicio, Estado estado)
        {
            this.fechaHoraFin = fechaHoraFin;
            this.fechaHoraInicio = fechaHoraInicio;
            this.estado = estado;
        }

        public DateTime FechaHoraFin { get => fechaHoraFin; set => fechaHoraFin = value; }
        public DateTime FechaHoraInicio { get => fechaHoraInicio; set => fechaHoraInicio = value; }
        internal Estado Estado { get => estado; set => estado = value; }

        public CambioEstado(Estado estado, DateTime fechaHoraInicioNuevo)                                               //CONSTRUCTOR
        {
            this.estado = estado;
            this.fechaHoraInicio = fechaHoraInicioNuevo;
        }


    }
}
