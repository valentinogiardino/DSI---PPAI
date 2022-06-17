using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class ReservaVisita
    {
        private int cantidadAlumnos;
        private int cantidadAlumnosConfirmada;
        private int duracionEstimada;
        private DateTime fechaHoraCreacion;
        private DateTime fechaHoraReserva;
        private string horaFinReal;
        private string horaInicioReal;
        private int numeroReserva;
        private CambioEstado cambioEstado;
        private Escuela escuela;
        private List<AsignacionVisita> asignacionGuia;
        private List<Exposicion> exposiciones;
        private Empleado empleadoResponsable;
        private Sede sede;

        

        public ReservaVisita(Estado estadoNuevaReserva, int numeroNuevaReserva, Empleado empleado, DateTime fechaHoraReserva, int duracionEstimada, List<Exposicion> listaExposicones, Escuela escuela, int cantidadVisitantes, List<Empleado> guiasAsignados, DateTime fechaHoraCreacion, DateTime fechaHoraFin, Sede sede)
        {
            
            this.numeroReserva = numeroNuevaReserva;
            this.fechaHoraReserva = fechaHoraReserva;
            this.fechaHoraCreacion = fechaHoraCreacion;
            this.duracionEstimada = duracionEstimada;
            this.cantidadAlumnos = cantidadVisitantes;
            this.empleadoResponsable = empleado;
            this.escuela = escuela;
            this.Exposiciones = listaExposicones;
            this.sede = sede;
            this.cambioEstado = crearCambioEstado(estadoNuevaReserva, fechaHoraCreacion);
            this.asignacionGuia = crearAsignacionGuia(fechaHoraReserva, fechaHoraFin, guiasAsignados);
        }

        public CambioEstado crearCambioEstado(Estado estadoNuevaReserva, DateTime fechaHoraCreacion)                                                                            //SE CREA EL CAMBIO DE ESTADO
        {
            CambioEstado nuevoCambioEstado = new CambioEstado(estadoNuevaReserva, fechaHoraCreacion);
            return nuevoCambioEstado;
        }

        public List<AsignacionVisita> crearAsignacionGuia(DateTime fechaHoraInicio, DateTime fechaHoraFin, List<Empleado> listaGuiasAsignados)                                  //SE CREAN LAS ASIGNACIONES VISITAS
        {
            List<AsignacionVisita> listaAsignaciones = new List<AsignacionVisita>();
            foreach (Empleado guia in listaGuiasAsignados)
            {
                AsignacionVisita asignacionVisita = new AsignacionVisita(fechaHoraFin, fechaHoraInicio, guia);
                listaAsignaciones.Add(asignacionVisita);
            }
            return listaAsignaciones;
        }


        public int CantidadAlumnos { get => cantidadAlumnos; set => cantidadAlumnos = value; }
        public int CantidadAlumnosConfirmada { get => cantidadAlumnosConfirmada; set => cantidadAlumnosConfirmada = value; }
        
        public DateTime FechaHoraCreacion { get => fechaHoraCreacion; set => fechaHoraCreacion = value; }
        public DateTime FechaHoraReserva { get => fechaHoraReserva; set => fechaHoraReserva = value; }
        public string HoraFinReal { get => horaFinReal; set => horaFinReal = value; }
        public string HoraInicioReal { get => horaInicioReal; set => horaInicioReal = value; }
        public int NumeroReserva { get => numeroReserva; set => numeroReserva = value; }
        internal CambioEstado CambioEstado { get => cambioEstado; set => cambioEstado = value; }
        internal Escuela Escuela { get => escuela; set => escuela = value; }
        
        public int DuracionEstimada { get => duracionEstimada; set => duracionEstimada = value; }
        public List<AsignacionVisita> AsignacionGuia { get => asignacionGuia; set => asignacionGuia = value; }
        public List<Exposicion> Exposiciones { get => exposiciones; set => exposiciones = value; }
        public Empleado EmpleadoResponsable { get => empleadoResponsable; set => empleadoResponsable = value; }
        public Sede Sede { get => sede; set => sede = value; }

        public ReservaVisita()
        {

        }

        public int obtenerAlumnosEnReserva(DateTime fechaHoraReservaNueva, int duracionEstimadaNuevaReserva)                                                        //SI SU HORARIO COINCIDE CON LO DE LA NUEVA RESERVA, DEVUELVE SU CANTIDAD DE ALUMNOS
        {
            

            DateTime fechaHoraFinReservaNueva = fechaHoraReservaNueva.AddMinutes(Convert.ToDouble(duracionEstimadaNuevaReserva)); //Fecha fin reserva a crear
            DateTime fechaHoraFinReservaActual = this.fechaHoraReserva.AddMinutes(Convert.ToDouble(this.duracionEstimada));  //fecha fin reserva ya creada

            int comparacion1 = DateTime.Compare(fechaHoraReservaNueva, this.fechaHoraReserva);
            int comparacion2 = DateTime.Compare(fechaHoraFinReservaNueva, this.fechaHoraReserva);

            int comparacion3 = DateTime.Compare(fechaHoraReservaNueva, fechaHoraFinReservaActual);
            int comparacion4 = DateTime.Compare(fechaHoraFinReservaNueva, fechaHoraFinReservaActual);

            int cantidadAlumnos = 0;
            if (comparacion1 <= 0 && comparacion2 >= 0)
            {
                cantidadAlumnos = getCantidadAlumnos();
            }
            else
            {
                if (comparacion3 <= 0 && comparacion4 >=0)
                {
                    cantidadAlumnos = getCantidadAlumnos();
                }
            }

            return cantidadAlumnos;
          
        }



        public DateTime getFechaHoraReserva()
        {
            return this.fechaHoraReserva;
        }

        public int getCantidadAlumnos()                                                                                                                                    //RETORNA LA CANTIDAD DE ALUMNOS
        {
            if (this.cantidadAlumnosConfirmada > 0)
            {
                return this.cantidadAlumnosConfirmada;
            }
            else
            {
                return this.cantidadAlumnos;
            }
                
        }


    }
}
