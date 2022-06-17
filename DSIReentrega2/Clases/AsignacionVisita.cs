using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class AsignacionVisita
    {
        private DateTime fechaHoraFin;
        private DateTime fechaHoraInicio;
        private Empleado guiaAsignado;

        public AsignacionVisita(DateTime fechaHoraFin, DateTime fechaHoraInicio, Empleado guiaAsignado)
        {
            this.fechaHoraFin = fechaHoraFin;
            this.fechaHoraInicio = fechaHoraInicio;
            this.guiaAsignado = guiaAsignado;
        }

        public DateTime FechaHoraFin { get => fechaHoraFin; set => fechaHoraFin = value; }
        public DateTime FechaHoraInicio { get => fechaHoraInicio; set => fechaHoraInicio = value; }
        internal Empleado GuiaAsignado { get => guiaAsignado; set => guiaAsignado = value; }



        public AsignacionVisita()
        {

        }

        


        public bool esDeEmpleado(string cuit)                                                                                                       //LE PREGUNTA A SU GUIA SI ES EL EMPLEADO POR EL QUE SE PREGUNTA
        {
            bool resultado = this.guiaAsignado.sosEmpleado(cuit);
            
            return resultado;
        }


        public bool esAsignadoParaFechaHora(DateTime fechaHoraReservaNueva, int duracionEstimadaNuevaReserva)                                          //SE CALCULA SI LA ASIGNACION COMPRENDE LA FECHAHORA RESERVA             
        {
            bool resultado = true;
            DateTime fechahoraInicioAsignacion = this.fechaHoraInicio;
            DateTime fechaHoraFinAsignacion = this.fechaHoraFin;

            DateTime fechahoraInicioReserva = fechaHoraReservaNueva;
            DateTime fechaHoraFinReserva = fechahoraInicioReserva.AddMinutes(Convert.ToDouble(duracionEstimadaNuevaReserva));

            int comparacion1 = DateTime.Compare(fechahoraInicioReserva, fechaHoraFinAsignacion);                                                        //FUNCION QUE COMPARA FECHAS (-1  SI FECHA1 < FECHA2 -- )
                                                                                                                                                        //(0  SI FECHA1 = FECHA2 -- ) 
                                                                                                                                                        //(1  SI FECHA1 > FECHA2 -- ) 
            int comparacion2 = DateTime.Compare(fechaHoraFinReserva, fechahoraInicioAsignacion);


            if (comparacion1 >= 0 || comparacion2 <= 0)                                                                         //NO ES ASIGNADO SI SU ASIGNACION TERMINA ANTES DEL INICIO DE LA RESERVA O SI EMPIEZA DESPUES DEL FIN DE LA RESERVA
            {
                resultado = false;
            }

            return resultado;

        }

    }

}
