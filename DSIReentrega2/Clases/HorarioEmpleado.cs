using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class HorarioEmpleado
    {
        private string horaIngreso;
        private string horaSalida;

        public HorarioEmpleado(string horaIngreso, string horaSalida)
        {
            this.horaIngreso = horaIngreso;
            this.horaSalida = horaSalida;
        }

        public string HoraIngreso { get => horaIngreso; set => horaIngreso = value; }
        public string HoraSalida { get => horaSalida; set => horaSalida = value; }


        public HorarioEmpleado()
        {

        }


        public bool dispEnFechaHoraReserva(DateTime fechaHoraReservaNueva, int duracionEstimadaNuevaReserva)                                            //SE CALCULA SI EL HORARIO COMPRENDE LA FECHAHORA RESERVA
        {
            bool resultado = false;
            DateTime horaIngresoEmpleado = DateTime.Parse(this.horaIngreso);                                                                            
            DateTime horaEgresoEmpleado = DateTime.Parse(this.horaSalida);

            DateTime horaInicioReserva = DateTime.Parse(fechaHoraReservaNueva.ToShortTimeString());
            DateTime HoraFinReserva = horaInicioReserva.AddMinutes(Convert.ToDouble(duracionEstimadaNuevaReserva));                                     //SE CALCULA LA HORA FIN SUMANDOLE LA DURACION A LA HORA INICIO


            int comparacion1 = DateTime.Compare(horaIngresoEmpleado, horaInicioReserva);                                                                //FUNCION QUE COMPARA FECHAS (-1  SI FECHA1 < FECHA2 -- )
            int comparacion2 = DateTime.Compare(horaEgresoEmpleado,HoraFinReserva);                                                                                                //(0  SI FECHA1 = FECHA2 -- )   
                                                                                                                                                                                   //(1  SI FECHA1 > FECHA2 -- ) 


            if (comparacion1 <= 0 && comparacion2 >= 0)                                                                                                 //EL INGRESO DEL EMPLEADO DEBE SER ANTERIOR O IGUAL AL INICIO DE LA RESERVA
                                                                                                                                                        //EL EGRESO DEL EMPLEADO DEBE SER DESPUES O IGUAL AL FIN DE LA RESERVA
            {
                resultado = true;
            }
        
            return resultado;

        }



        
    }
}
