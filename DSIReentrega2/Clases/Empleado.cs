using DSIReentrega.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Empleado
    {
        private string apellido;
        private string codigoValidacion;
        private string cuit;
        private string domicilio;
        private DateTime fechaIngreso;
        private DateTime fechaNacimiento;
        private string mail;
        private string nombre;
        private string sexo;
        private string telefono;
        private Cargo cargo;
        private List<HorarioEmpleado> horarioEmpleado;
        private Sede sede;

        public Empleado(string apellido, string codigoValidacion, string cuit, string domicilio, DateTime fechaIngreso, DateTime fechaNacimiento, string mail, string nombre, string sexo, string telefono, Cargo cargo, HorarioEmpleado horarioEmpleado)
        {
            this.apellido = apellido;
            this.codigoValidacion = codigoValidacion;
            this.cuit = cuit;
            this.domicilio = domicilio;
            this.fechaIngreso = fechaIngreso;
            this.fechaNacimiento = fechaNacimiento;
            this.mail = mail;
            this.nombre = nombre;
            this.sexo = sexo;
            this.telefono = telefono;
            this.cargo = cargo;
           
        }

        public string Apellido { get => apellido; set => apellido = value; }
        public string CodigoValidacion { get => codigoValidacion; set => codigoValidacion = value; }
        public string Cuit { get => cuit; set => cuit = value; }
        public string Domicilio { get => domicilio; set => domicilio = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public Sede Sede { get => sede; set => sede = value; }
        internal Cargo Cargo { get => cargo; set => cargo = value; }
        public List<HorarioEmpleado> HorarioEmpleado { get => horarioEmpleado; set => horarioEmpleado = value; }

        public Empleado() { }

        public Empleado(string cuit)
        {
            this.cuit = cuit;
        }


        public List<HorarioEmpleado> buscarHorario()
        {
            string cuit = this.cuit;
            ADEmpleado accesoEmpleado = new ADEmpleado();        //SE INSTANCIA A LA CLASE QUE SE COMUNICARA CON LA BD

            DataTable tabla = accesoEmpleado.buscarHorario(cuit);    //SE LE PIDE AL ACCESEXPOSICION QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA

            List<HorarioEmpleado> listaHorarios = new List<HorarioEmpleado>();


            foreach (DataRow row in tabla.Rows)
            {
                HorarioEmpleado horarioEmpleado = new HorarioEmpleado();
                horarioEmpleado.HoraIngreso = row["horaIngreso"].ToString();
                horarioEmpleado.HoraSalida = row["horaSalida"].ToString();

                listaHorarios.Add(horarioEmpleado);
            }


            return listaHorarios;
        }



        public bool getGuiaDispEnHorario(Sede sedeSeleccionada, DateTime fechaHoraReservaNueva, int duracionEstimada, List<AsignacionVisita> listaAsignacionesVisita) 
        {                                                                                                                                               //VERIFICA SI ES DE SEDE Y ESTA DISPONIBLE PARA LA FECHAHOARA DE LA RESERVA
            bool resultado = true;
            if (esDeSede(sedeSeleccionada) && this.cargo.esGuia() && estaDisponible(fechaHoraReservaNueva, duracionEstimada))
            {
                List<AsignacionVisita> listaAsignacionesEmpleado = obtenerAsignacionesVisitaEmpleado(listaAsignacionesVisita);
                foreach (AsignacionVisita asignacion in listaAsignacionesEmpleado)
                {
                    if (asignacion.esAsignadoParaFechaHora(fechaHoraReservaNueva, duracionEstimada))
                    {
                        resultado = false;
                        break;
                    }
                    
                }
      
            }
            else
            {
                resultado = false;
            }
            return resultado;
            

        }

        public bool estaDisponible(DateTime fechaHoraReservaNueva, int duracionEstimada)                                                                    //LLAMA A SUS HORARIOS PARA VERIFICAR SI ESTA DISPONIBLE
        {
            bool resultado = false;
            foreach (HorarioEmpleado horario in this.horarioEmpleado)
            {
                if (horario.dispEnFechaHoraReserva(fechaHoraReservaNueva, duracionEstimada))
                {
                    resultado = true;
                    break;
                }
                
            }
            return resultado;

        }


        public List<AsignacionVisita> obtenerAsignacionesVisitaEmpleado(List<AsignacionVisita> listaAsignacionesVisitas)                                    //DEVUELVE LAS ASIGNACIONES DEL EMPLEADO
        {
            List<AsignacionVisita> listaAsignacionesEmpleado = new List<AsignacionVisita>();
            foreach (AsignacionVisita asignacion in listaAsignacionesVisitas)
            {
                if (asignacion.esDeEmpleado(this.cuit))
                {
                    listaAsignacionesEmpleado.Add(asignacion);
                }

            }
            return listaAsignacionesEmpleado;
            

        }

        public bool esDeSede(Sede sedeSeleccionada)                                                                                                         //RESPONDE SI ES DE SEDE
        {
            bool resultado = false;
            if (sedeSeleccionada.mostrarNombre() == this.sede.mostrarNombre())
            {
                resultado = true;
            }
            return resultado;
        }


        public string mostrarNombre()                                                                                                                       //DEVUELVE SU NOMBRE
        {
            return this.nombre;
        }

        public bool sosEmpleado(string cuitEmpleado)                                                                                                      //RESPONDE SI ES EMPLEADO
        {
            
            bool resultado = false;
            if (this.cuit == cuitEmpleado)
            {
                resultado = true;
            }
            return resultado;
        }

        public string mostrarCuit()                                                                                                                          //DEVUELVE SU CUIT
        {
            return this.cuit;
        }



    }
}
