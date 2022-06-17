using DSIReentrega.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Sede                                                                                   //SE DEFINE LA CLASE SEDE
    {
        private int cantMaximaPorGuia;
        private int cantMaximaVisitante;
        private string nombre;
        private List<Exposicion> exposiciones;



        public Sede(string nombre)                                                                      //CONSTRUCTOR
        {
            this.nombre = nombre;
        }

        public Sede()                                                                                   //CONSTRUCTOR
        {

        }
        
                //GETTERS Y SETTERS

        

        public int CantMaximaPorGuia { get => cantMaximaPorGuia; set => cantMaximaPorGuia = value; }
        public int CantMaximaVisitante { get => cantMaximaVisitante; set => cantMaximaVisitante = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public List<Exposicion> Exposiciones { get => exposiciones; set => exposiciones = value; }


        //OTROS METODOS DE SEDE


        public string mostrarNombre()
        {
            return this.nombre;
        }

        public bool sosSede(string nombreSede)                                                                              //LA SEDE RESPONDE SI ES POR LA QUE PREGUNTAN
        {
            bool resultado = false;
            if (nombreSede == this.mostrarNombre())
            {
                resultado = true;
            }
            return resultado;
        }



        public void buscarExposiciones()                                                //CREA OBJETOS EXPOSICION A PARTIR DE FILAS DE UNA TABLA Y LOS GUARDA EN UNA LISTA
        {
            string nombreSedeSeleccionada = this.nombre;
            ADExposicion accesoExposciones = new ADExposicion();                                                            //SE INSTANCIA A LA CLASE QUE SE COMUNICARA CON LA BD
            DataTable tabla = new DataTable();
            tabla = accesoExposciones.buscarExposiciones(nombreSedeSeleccionada);                                           //SE LE PIDE AL ACCESEXPOSICION QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA

            List<Exposicion> listaExposicionesSede = new List<Exposicion>();
            

            foreach (DataRow row in tabla.Rows)
            {
                Exposicion exposicion = new Exposicion();
                exposicion.FechaFin = DateTime.Parse(row["fechaFin"].ToString());
                exposicion.FechaFinReplanificada = DateTime.Parse(row["fechaFinReplanificada"].ToString());
                exposicion.FechaInicio = DateTime.Parse(row["fechaInicio"].ToString());
                exposicion.FechaInicioReplanificada = DateTime.Parse(row["fechaInicioReplanificada"].ToString());
                exposicion.HoraApertura = row["horaApertura"].ToString();
                exposicion.HoraCierre = row["horaCierre"].ToString();
                exposicion.Nombre = row["nombreExposicion"].ToString();
                string nombreTipo = row["nombreTipoExposicion"].ToString();
                string descripcion = row["descripcion"].ToString();
                string nombrePublico = row["nombrePublicoDestino"].ToString();
                string caracteristicas = row["caracteristicas"].ToString();
                TipoExposicion tipoExposicion = new TipoExposicion(nombreTipo, descripcion);
                exposicion.TipoExposicion = tipoExposicion;
                PublicoDestino publico = new PublicoDestino(nombrePublico, caracteristicas);
                exposicion.PublicoDestino = publico;
                

                listaExposicionesSede.Add(exposicion);
            }
            foreach (Exposicion exposicion1 in listaExposicionesSede)
            {
                exposicion1.buscarDetalleExposicion();                                                  //BUSCA LOS DETALLES DE CADA EXPOSICION

                //this.exposiciones.Add(exposicion1);                                                     //SETEA LAS EXPOSICIONES DE LA SEDE
            }
            this.exposiciones = listaExposicionesSede;

        }

        public List<Exposicion> buscarExposicionesTempVigentes(DateTime fechaHoraActual)                //BUSCA LAS EXPOSICIONES TEMPORALES VIGENTES
        {
            buscarExposiciones();                                                                       //LA SEDE BUSCA TODAS SUS EXPOSICIONES
            List<Exposicion> listaExposicionesTempVig = new List<Exposicion>();
            foreach (Exposicion exposicion1 in this.exposiciones)
            {
                bool valida = false;
                valida = exposicion1.esTempVigente(fechaHoraActual);                                    //VALIDA QUE LA EXPOSICION SEA TEMPORTAL VIGENTE
                if (valida)
                {
                    listaExposicionesTempVig.Add(exposicion1);
                }
            }
            return listaExposicionesTempVig;
        }


        public List<Exposicion> buscarExposicionesPermVigentes(DateTime fechaHoraActual)                //BUSCA LAS EXPOSICIONES PERMANENTES VIGENTES
        {
            List<Exposicion> listaExposicionesTempVig = new List<Exposicion>();
            foreach (Exposicion exposicion1 in this.exposiciones)
            {
                bool valida = false;
                valida = exposicion1.esPermVigente(fechaHoraActual);                                    //VALIDA QUE LA EXPOSICION SEA PERMANENTE VIGENTE
                if (valida)
                {
                    listaExposicionesTempVig.Add(exposicion1);
                }
            }
            return listaExposicionesTempVig;
        }

        public DataTable obtenerDatosExposicionVigente(List<Exposicion> listaExposiciones)                                               //SE CREA UNA TABLA CON LOS DATOS DE LAS EXPOSICIONES RECIBIDAS POR PARAMETRO
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("HoraDesde");
            tabla.Columns.Add("HoraHasta");
            tabla.Columns.Add("Publico");
            foreach (Exposicion exposicion in listaExposiciones)
            {
                List<String> datos = exposicion.obtenerDatosExposicionVigente();
                tabla.Rows.Add(datos[0], datos[1], datos[2], datos[3]);
            }
            return tabla;
        }

        public int buscarDuracExtObras(List<Exposicion> listaExposicionesSeleccionadas)                                         //SE OBTIENE LA DURACION ESTIMADA DE LAS EXPOSICIONES SELECCIONADAS.
        {
            int duracionExposicionSeleccionadas = 0;
            foreach (Exposicion exposicion in listaExposicionesSeleccionadas)
            {
                duracionExposicionSeleccionadas += exposicion.buscarDuracExtObras();                                                   //CADA EXPOSICION DEVUELVE SU DURACION EXTENDIDA
            }
            return duracionExposicionSeleccionadas;
        }


        public int buscarDuracResumObras(List<Exposicion> listaExposicionesSeleccionadas)                                         //SE OBTIENE LA DURACION ESTIMADA DE LAS EXPOSICIONES SELECCIONADAS.
        {
            int duracionExposicionSeleccionadas = 0;
            foreach (Exposicion exposicion in listaExposicionesSeleccionadas)
            {
                duracionExposicionSeleccionadas += exposicion.buscarDuracResumObras();                                                   //CADA EXPOSICION DEVUELVE SU DURACION RESUMIDA
            }
            return duracionExposicionSeleccionadas;
        }


        public int buscarReservaParaFechaHora(DateTime fechaHoraReservaNueva, int duracionEstimadaReservaNueva)                        //SE OBTIENE LA CANTIDAD DE ALUMNUNOS QUE HAY DURANTE LA DURACION DE LA RESERVA
        {
            int cantidadAlumnos = 0;
            List<ReservaVisita> listaReservasSede = buscarReservas(this.mostrarNombre());
            foreach (ReservaVisita reserva in listaReservasSede)
            {
                cantidadAlumnos += reserva.obtenerAlumnosEnReserva(fechaHoraReservaNueva, duracionEstimadaReservaNueva);                //CADA RESERVA OBTIENE SUS ALUMNOS
            }

            return cantidadAlumnos;
        }

        public bool verificarCantidadMaxVisitantes(int cantidadOtrasReservas, int cantidadNuevaReserva)                                 //SE VERIFICA QUE NO SE SOBREPASE LA CAPACIDAD MAXIMA
        {
            bool resultado = true;
            if ((cantidadNuevaReserva + cantidadOtrasReservas) >= this.cantMaximaVisitante)
            {
                resultado = false;
            }
            return resultado;
        }

        public List<ReservaVisita> buscarReservas(string nombreSede)                                                                    //CREA OBJETOS RESERVAS A PARTIR DE FILAS DE UNA TABLA Y LOS GUARDA EN UNA LISTA DE RESREVAS
        {
            ADReservaVisita accesoGestor = new ADReservaVisita();                                                                       //INSTANCIA LA CLASE DE ACCESO A DATOS
            DataTable tabla = new DataTable();
            tabla = accesoGestor.buscarReservas(nombreSede);                                                                            //SE LE PIDE AL ACCESOGESTOR QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA

            List<ReservaVisita> listaReservas = new List<ReservaVisita>();

            foreach (DataRow row in tabla.Rows)
            {
                ReservaVisita reserva = new ReservaVisita();
                reserva.CantidadAlumnos = int.Parse(row["cantidadAlumnos"].ToString());
                reserva.CantidadAlumnosConfirmada = int.Parse(row["cantidadAlumnosConfirmada"].ToString());
                reserva.DuracionEstimada = int.Parse(row["duracion"].ToString());
                reserva.FechaHoraReserva = DateTime.Parse(row["fechaHoraReserva"].ToString());
                reserva.NumeroReserva = int.Parse(row["numeroReserva"].ToString());

                listaReservas.Add(reserva);
            }

            return listaReservas;
        }



        public int getCantMaxPorGuia()                                                                                                //SE OBTIENE LA CANTIDAD MAXIMA POR GUIA
        {
            return this.cantMaximaPorGuia;
        }



        

    }
}
