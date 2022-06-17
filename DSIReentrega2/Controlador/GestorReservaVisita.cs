using DSIReentrega.AccesoDatos;
using DSIReentrega.Clases.Strategy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class GestorReservaVisita       //DEFINO LA CLASE GESTOR
    {
        private int cantidadVisitantes;
        private Escuela escuelaSeleccionada;
        private List<Exposicion> exposicionesSeleccionadas;
        private DateTime fechaHoraReserva;
        private List<Empleado> guiaSeleccionados;
        private Sede sedeSeleccionada;
        private PantallaReservaDeVisita pantallaReservaDeVisita;
        private TipoVisita tipoVisitaSeleccionado;
        private List<Exposicion> exposicionesSedeSeleccionada;
        private List<Escuela> listaEscuelas;
        private Sesion sesionActual;
        private int duracionEstimadaReserva;
        private EstrategiaTipoVisita estrategia;



        //GETTERS Y SETTERS

        public int CantidadVisitantes { get => cantidadVisitantes; set => cantidadVisitantes = value; }
        public DateTime FechaHoraReserva { get => fechaHoraReserva; set => fechaHoraReserva = value; }
        public PantallaReservaDeVisita PantallaReservaDeVisita { get => pantallaReservaDeVisita; set => pantallaReservaDeVisita = value; }
       
        internal List<Exposicion> ExposicionesSeleccionadas { get => exposicionesSeleccionadas; set => exposicionesSeleccionadas = value; }
        internal List<Empleado> GuiaSeleccionados { get => guiaSeleccionados; set => guiaSeleccionados = value; }
        public Escuela EscuelaSeleccionada { get => escuelaSeleccionada; set => escuelaSeleccionada = value; }
        public Sede SedeSeleccionada { get => sedeSeleccionada; set => sedeSeleccionada = value; }
        public TipoVisita TipoVisitaSeleccionado { get => tipoVisitaSeleccionado; set => tipoVisitaSeleccionado = value; }
        public List<Exposicion> ExposicionesSedeSeleccionada { get => exposicionesSedeSeleccionada; set => exposicionesSedeSeleccionada = value; }
        public Sesion SesionActual { get => sesionActual; set => sesionActual = value; }
        public int DuracionEstimadaReserva { get => duracionEstimadaReserva; set => duracionEstimadaReserva = value; }
        public List<Escuela> ListaEscuelas { get => listaEscuelas; set => listaEscuelas = value; }
        public EstrategiaTipoVisita Estrategia { get => estrategia; set => estrategia = value; }



        //OTROS METODOS DEL GESTOR



        public GestorReservaVisita(PantallaReservaDeVisita pantalla)                                                    //SETEA EL PUNTERO A PANTALLA CON LA PANTALLA QUE RECIBE COMO PARAMETRO
        {
            pantallaReservaDeVisita = pantalla;  

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarRegReservaVisita()                                                                             //METODO QUE INICIA LA BUSQUEDA DE ESCUELAS EXISTENTES
        {
            this.sesionActual = iniciarSesion();
            this.listaEscuelas = buscarEscuelas();
            List<String> listaNombreEscuelas = obtenerNombresEscuelas(this.listaEscuelas);
            pantallaReservaDeVisita.mostrarEscuela(listaNombreEscuelas);                                                //SE LLAMA A METODOS DE LA PANTALLA PARA INICIAR UNA NUEVA INTERACCION
            pantallaReservaDeVisita.solicitarSeleccionEscuela();

        }



        public List<Escuela> buscarEscuelas()                                                                           //CREA OBJETOS ESCUELA A PARTIR DE FILAS DE UNA TABLA Y LOS AGREGA A UNA LISTA DE ESCUELA
        {
            ADGestor accesoGestor = new ADGestor();                                                                     //INSTANCIA LA CLASE DE ACCESO A DATOS
            DataTable tabla = new DataTable();
            tabla = accesoGestor.buscarEscuela();                                                                       //SE LE PIDE AL ACCESOGESTOR QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA
            
            List<Escuela> listaEscuelas = new List<Escuela>();

            foreach (DataRow row in tabla.Rows)
            {
                Escuela escuela = new Escuela();
                escuela.Domicilio = row["domicilio"].ToString();
                escuela.Mail = row["mail"].ToString();
                escuela.Nombre = row["nombre"].ToString();
                escuela.TelefonoCelular = row["telefonoCelular"].ToString();
                escuela.TelefonoFijo = row["telefonoFijo"].ToString();

                listaEscuelas.Add(escuela);
            }
            
            return listaEscuelas;
        }
        


        public List<String> obtenerNombresEscuelas(List<Escuela> listaEscuelas)                                         //LE PIDE A CADA ESCUELA QUE MUESTRE SU NOMBRE Y LOS GUARDA EN UNA LISTA
        {
            List<String> listaNombreEscuelas = new List<String>();
            foreach (Escuela escuela in listaEscuelas)
            {
                listaNombreEscuelas.Add(escuela.mostrarNombre());
            }
            return listaNombreEscuelas; 
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarEscuela(string seleccionEscuela)                                                               //GUARDA LA ESCUELA SELECCIONADA POR EL USUARIO Y LE PIDE A LA
                                                                                                                        //PANTALLA QUE SOLICITE LA CANTIDAD DE VISITANTES
        {            
            foreach (Escuela escuela in this.listaEscuelas)
            {                
                if (escuela.sosEscuela(seleccionEscuela))                                                               //ES LA ESCUELA QUIEN RESUELVE SI ES LA ESCUELA SELECCIONADA (PATRON EXPERTO)
                {
                    this.escuelaSeleccionada = escuela;
                    break;
                }
                else
                {
                    this.escuelaSeleccionada = null;
                }
            }
            pantallaReservaDeVisita.solicitarCantVisitantes();                                                           //SE INVOCA EL METODO DE LA PANTALLA PARA PASAR A LA SIGUIENTE INTERACCION
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarCantVisitantes(int cantidadVisitantes)                                                         //GUARDA LA CANTIDAD DE VISITANTES INGREADOS.
                                                                                                                        //BUSCA LAS SEDES EXISTENTES Y LE PIDE A LA PANTALLA QUE LAS MUESTRE Y SOLICITE SELECCION
        {
            this.cantidadVisitantes = cantidadVisitantes;
            List<Sede> listaSedes = buscarSedes();
            List<String> listaNombreSedes = obtenerNombreSede(listaSedes);
            pantallaReservaDeVisita.mostrarSedes(listaNombreSedes);                                                     //SE LLAMA A METODOS DE LA PANTALLA
            pantallaReservaDeVisita.solicitarSeleccionSede();
        }



        public List<Sede> buscarSedes()                                                                                 //CREA OBJETOS SEDE A PARTIR DE FILAS DE UNA TABLA Y LOS AGREGA A UNA LISTA DE SEDES
        {
            ADSede accesoSede = new ADSede();
            DataTable tabla = new DataTable();
            tabla = accesoSede.buscarSede();

            List<Sede> listaSedes = new List<Sede>();

            foreach (DataRow row in tabla.Rows)
            {
                Sede sede = new Sede();
                sede.CantMaximaVisitante = int.Parse(row["cantMaximaVisitante"].ToString());
                sede.CantMaximaPorGuia = int.Parse(row["cantMaximaPorGuia"].ToString());
                sede.Nombre = row["nombre"].ToString();

                listaSedes.Add(sede);
            }
            return listaSedes;
        }



        public List<String> obtenerNombreSede(List<Sede> listaSedes)                                                    //LE PIDE A CADA SEDE QUE MUESTRE SU NOMBRE Y LOS GUARDA EN UNA LISTA
        {
            List<String> listaNombreSede = new List<String>();
            foreach (Sede sede in listaSedes)
            {
                listaNombreSede.Add(sede.mostrarNombre());
            }
            return listaNombreSede;
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarSede(string seleccionSede)                                                                     //GUARDA LA SEDE SELECCIONADA.
                                                                                                                        //BUSCA LOS TIPOS DE VISITA Y LE PIDE A LA PANTALLA QUE LOS MUESTRE Y SOLICITE SELECCION
        {
            List<Sede> listaSede = this.buscarSedes();
            foreach (Sede sede in listaSede)
            {
                if (sede.sosSede(seleccionSede))                                                                        //ES LA SEDE QUIEN RESUELVE SI ES POR QUIEN PREGUNTAN (PATRON EXPERTO)
                {
                    this.SedeSeleccionada = sede;
                    break;
                }
                else
                {
                    this.sedeSeleccionada = null;
                }
            }         

            List<TipoVisita> listaTipoVisita = buscarTiposVisita();
            List<String> listaNombreTipoVisita = obtenerNombreTipoVisita(listaTipoVisita);
            pantallaReservaDeVisita.mostrarTiposVisita(listaNombreTipoVisita);                                          //LLAMA A METODOS DE LA PANTALLA
            pantallaReservaDeVisita.pedirSelecTipoVisita();                          
        }



        public List<TipoVisita> buscarTiposVisita()                                                                     //CREA OBJETOS TIPOVISITA A PARTIR DE FILAS DE UNA TABLA Y LOS AGREGA A UNA LISTA DE TIPOSVISITA
        {
            ADTIpoVisita accesoTipoVisita = new ADTIpoVisita();
            DataTable tabla = new DataTable();
            tabla = accesoTipoVisita.buscarTipoVisita();

            List<TipoVisita> listaTipoVisita = new List<TipoVisita>();

            foreach (DataRow row in tabla.Rows)
            {
                TipoVisita tipoVisita = new TipoVisita();
                tipoVisita.Nombre = row["nombre"].ToString();
                listaTipoVisita.Add(tipoVisita);
            }
            return listaTipoVisita;
        }


        public List<String> obtenerNombreTipoVisita(List<TipoVisita> listoTipoVisita)                                   //LE PIDE A CADA TIPOVISITA QUE MUESTRE SU NOMBRE Y LOS GUARDA EN UNA LISTA
        {
            List<String> listaNombreTipoVisita = new List<String>();
            foreach (TipoVisita tipoVisita in listoTipoVisita)
            {
                listaNombreTipoVisita.Add(tipoVisita.mostrarNombre());
            }
            return listaNombreTipoVisita;
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarTipoVisita(string seleccionTipoVisita)                                                         //GUARDA EL TIPOVISITA SELECCIONADO.                     
        {
            List<TipoVisita> listaTipoVisita = this.buscarTiposVisita();
            foreach (TipoVisita tipoVisita in listaTipoVisita)
            {
                if (tipoVisita.sosTipoVisita(seleccionTipoVisita))                                                      //ES EL TIPOVISITA QUIEN RESUELVE SI ES EL TIPOVISITA SELECCIONADO (PATRON EXPERTO)
                {
                    this.tipoVisitaSeleccionado = tipoVisita;
                    break;
                }
                else
                {
                    this.tipoVisitaSeleccionado = null;
                }
            }
            DateTime fechaHoraActual = tomarFechaHoraActual();                                                          //BUSCA FECHAHORA ACTUAL
            crearEstrategia(this.tipoVisitaSeleccionado.Nombre);                                                        //SEGUN EL TIPO DE VISITA SELECCIONADO CREA LA ESTRATEGIA CONCRETA CORRESPONDIENTE
            this.exposicionesSedeSeleccionada = buscarExposiciones(fechaHoraActual);                                    //BUSCA Y GUARDA LAS EXPOSICIONES DE LA SEDE SELECCIONADA SEGUN EL TIPO DE VISITA ELEGIDO
            this.estrategia.mostrarExposiciones(this.exposicionesSedeSeleccionada, this.sedeSeleccionada, this);
            //DataTable datosExposiciones = this.sedeSeleccionada.obtenerDatosExposicionVigente(this.exposicionesSedeSeleccionada);  //CREA UNA TABLA CON LOS DATOS DE LAS EXPOSICIONES SELECCIONADAS
            //pantallaReservaDeVisita.mostrarDatosExpoTempVigentes(datosExposiciones);                                                //LLAMA A LA PANTALLA PARA QUE MUESTRE LAS EXPOSICIONES
            
        }



        public DateTime tomarFechaHoraActual()                                                                          //OBTIENE FECHA Y HORA ACTUAL
        {
            DateTime fechaHoraActual = DateTime.Now;
            return fechaHoraActual;
        }

        public void crearEstrategia(string tipoVisitaSeleccionado)                                                  //INSTANCIA UNA ESTRATEGIA CONCRETA SEGÚN EL TIPO DE VISITA SELECCIONADO.
        {
            if (tipoVisitaSeleccionado == "Por Exposicion")                                 
            {
                this.estrategia = new EstrategiaPorExposicion();                                                    //SETEA LA ESTRATEGIA CREADA
            }
            if (tipoVisitaSeleccionado == "Completa")
            {
                this.estrategia = new EstrategiaCompleta();
            }
        }

        public List<Exposicion> buscarExposiciones(DateTime fechaHoraActual)
        {
            return this.estrategia.buscarExposiciones(fechaHoraActual, this.sedeSeleccionada);                       //SE DELEGA A LA ESTRATEGIA LA RESPONSABILIDAD DE BUSCAR LAS EXPOSICIONES DE LA SEDE SEGUN EL TIPO
        }                                                                                                           //DE VISITA SELECCIONADO

        public void mostrarDatosExpoPorExposicion(DataTable datosExposiciones)
        {
            pantallaReservaDeVisita.mostrarDatosExpoTempVigentes(datosExposiciones);                                //LLAMA A LA PANTALLA PARA QUE MUESTRE EXPOSICIONES TEMPORALES VIGENTES
        }

        public void mostrarDatosExpoCompleta(DataTable datosExposiciones)
        {
            pantallaReservaDeVisita.mostrarDatosExpoTempPermVigentes(datosExposiciones);                            //LLAMA A LA PANTALLA PARA QUE MUESTRE EXPOSICIONES TEMPORALES Y PERMANENTES VIGENTES
        }

        //public List<Exposicion> buscarExposicionesTempVig(DateTime fechaHoraActual)                                     //LE DICE A LA SEDE QUE BUSQUE SUS EXPOSICIONES TEMPORALES VIGENTES
        //{
        //    return sedeSeleccionada.buscarExposiciones(fechaHoraActual);
        //}



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarExposicionesVIEJO(List<String> nombreExposicionesSeleccionadas)                                     //METODO VIEJO!!!!! REVISAR
        {
            List<Exposicion> auxiliar = new List<Exposicion>();
            foreach (Exposicion exposicion in this.exposicionesSedeSeleccionada)
            {
                for (int i = 0; i < nombreExposicionesSeleccionadas.Count; i++)
                {
                    if (nombreExposicionesSeleccionadas[i].Equals(exposicion.mostrarNombre()))
                    {
                        auxiliar.Add(exposicion);
                    }
                }
            }
            this.exposicionesSeleccionadas = auxiliar;
            pantallaReservaDeVisita.solicitarFechaHoraReserva();                                                        //SE INVOCA EL METODO DE LA PANTALLA
        }



        public void tomarExposiciones(List<String> nombreExposicionesSeleccionadas)                                    //MEJORA DEL METODO PARA tomarExposicionesVIEJO(). SE GUARDAN LAS EXPOSICIONES SELECCIONADAS. 
        {
            this.exposicionesSeleccionadas = new List<Exposicion>();
            foreach (Exposicion exposicion in this.exposicionesSedeSeleccionada)
            {
                for (int i = 0; i < nombreExposicionesSeleccionadas.Count; i++)
                {
                    if (exposicion.sosExposicion(nombreExposicionesSeleccionadas[i]))                                   //ES LA EXPOSICION QUIEN RESUELVE SI ES LA EXPOSICION SELECCIONADA (PATRON EXPERTO) 
                    {
                        this.exposicionesSeleccionadas.Add(exposicion);
                    }
                }
            }
            pantallaReservaDeVisita.solicitarFechaHoraReserva();                                                        //SE INVOCA EL METODO DE LA PANTALLA PARA PASAR A LA PROXIMA INTERACCION
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarFechaHoraReserva(DateTime fechaHoraReserva)
        {
            this.fechaHoraReserva = fechaHoraReserva;                                                                   //SE GUARDA LA FECHAHORA DE LA RESERVA
            this.duracionEstimadaReserva = calcularDuracionEstimadaReserva(this.ExposicionesSeleccionadas);             //SE CALCULA LA DURACION ESTIMADA DE LA RESERVA
            
            
            
            bool enCapacidadPermitida = calcularSobrePasoCapMax(this.fechaHoraReserva, this.duracionEstimadaReserva);   //VERIFICA QUE NO SE SOBREPASE LA CAPACIDAD MAXIMA
            if (enCapacidadPermitida == false)
            {
                pantallaReservaDeVisita.notificarSobrepaso();
            }
            
            int cantidadGuiasNecesarios = calcularCantGuiasNecesarios();                                                 //CALCULA LA CANTIDAD DE GUIAS NECESARIOS
            List<Empleado> listaEmpleados = cargarEmpleados();                                                                  
            List<AsignacionVisita> listaAsignaciones = cargarAsignaciones();


            List<Empleado> listaGuiasDisponibles = buscarGuiasDispFechaReserva(this.sedeSeleccionada, this.fechaHoraReserva, this.duracionEstimadaReserva, listaAsignaciones, listaEmpleados);  //BUSCA GUIAS DISPONIBLES
            DataTable datosEmpleados = obtenerDatosEmpleadoDisponible(listaGuiasDisponibles);                                                                                                   //CREA UNA TABLA CON LOS DATOS DE GUIS DISPONIBLES
            pantallaReservaDeVisita.mostrarCantGuiasNecesarios(cantidadGuiasNecesarios);                                //LLAMA A METODOS DE LA PANTALLA PARA PASAR A LA SIGUIENTE INTERACCION
            pantallaReservaDeVisita.solicitarSelecGuias();
            bool existenGuiasSuficientes = hayGuiasSuficientes(cantidadGuiasNecesarios, listaGuiasDisponibles);         //CALCULA QUE HAYAN GUIAS SUFICIENTES PARA LA VISITA
            pantallaReservaDeVisita.mostrarGuiasDisponibles(datosEmpleados, existenGuiasSuficientes);
        }



        public int calcularDuracionEstimadaReserva(List<Exposicion> listaExposicionesSeleccionadas)                                          //SE LE DELEGA A LA ESTRATEGIA CONCRETA 
                                                                                                                                             //LA RESPONSABILIDAD DE OBTENER LA DURACION DE LAS EXPOSICIONES SELECCIONADAS
        {
            int duracionEstimadaReserva = this.estrategia.buscarDuracionExposiciones(listaExposicionesSeleccionadas, this.sedeSeleccionada);
            return duracionEstimadaReserva;

        }



        public bool calcularSobrePasoCapMax(DateTime fechaHoraReservaNueva, int duracionEstimadaReservaNueva)                               //LE DICE A LA SEDE QUE VERIFIQUE QUE NO SE SOBREPASE SU CAPACIDAD         
        {
            int cantidadAlumnosEnOtrasReservas = this.sedeSeleccionada.buscarReservaParaFechaHora(fechaHoraReservaNueva, duracionEstimadaReservaNueva);
            bool resultado = this.sedeSeleccionada.verificarCantidadMaxVisitantes(cantidadAlumnosEnOtrasReservas, this.cantidadVisitantes);
            return resultado;
        }



        public int calcularCantGuiasNecesarios()                                                                                             //CALCULA LOS GUIAS NECESARIOS
        {
            int cantidadMaximaPorGuia = this.sedeSeleccionada.getCantMaxPorGuia();
            int cantidadGuiaNecesarios = this.cantidadVisitantes / cantidadMaximaPorGuia;
            int guiaFaltante = this.cantidadVisitantes % cantidadMaximaPorGuia;

            if (guiaFaltante > 0)
            {
                cantidadGuiaNecesarios += 1;
            }

            return cantidadGuiaNecesarios;
        }

        

        public List<Empleado> cargarEmpleados()                                                                                         //CREA OBJETOS EMPLEADOS A PARTIR DE FILAS DE UNA TABLA Y LOS AGREGA A UNA LISTA DE EMPLEADOS                                                
        {
            
            ADEmpleado accesoEmpleado = new ADEmpleado();                                                                               //SE INSTANCIA A LA CLASE QUE SE COMUNICARA CON LA BD

            DataTable tabla = accesoEmpleado.buscarEmpleados();                                                                         //SE LE PIDE AL ACCESEMOLEADO QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA

            List<Empleado> listaEmpleados = new List<Empleado>();
            

            foreach (DataRow row in tabla.Rows)
            {
                Empleado empleado = new Empleado();
                empleado.Cuit = row["cuit"].ToString();
                empleado.Nombre = row["nombre"].ToString();
                empleado.Apellido = row["apellido"].ToString();
                empleado.Domicilio = row["domicilio"].ToString();
                empleado.Mail = row["mail"].ToString();
                empleado.CodigoValidacion = row["codigoValidacion"].ToString();
                empleado.FechaNacimiento = DateTime.Parse(row["fechaNacimiento"].ToString());
                empleado.Sexo = row["sexo"].ToString();
                empleado.Telefono = row["telefono"].ToString();
                empleado.FechaIngreso = DateTime.Parse(row["fechaIngreso"].ToString());
                string nombreCargo = row["nombreCargo"].ToString();
                string descCargo = row["descripcionCargo"].ToString();
                string nombreSede = row["nombreSede"].ToString();
                Cargo cargo = new Cargo(nombreCargo, descCargo);
                Sede sede = new Sede(nombreSede);
                empleado.Cargo = cargo;
                empleado.Sede = sede;
               

                listaEmpleados.Add(empleado);
            }
            foreach (Empleado empleado in listaEmpleados)
            {
                List < HorarioEmpleado > listaHorarios = new List<HorarioEmpleado>();
                listaHorarios = empleado.buscarHorario();                                                                              //A CADA EMPLEADO LE PIDE BUSCAR SUS HORARIOS
                empleado.HorarioEmpleado = listaHorarios;
            }

            return listaEmpleados;
        }



        public List<AsignacionVisita> cargarAsignaciones()                                                                            //CREA OBJETOS ASIGNACIOES A PARTIR DE FILAS DE UNA TABLA Y LOS AGREGA A UNA LISTA DE ASIGNACIONESVISITA
        {

            ADAsignacionVisita accesoAsignacion = new ADAsignacionVisita();                                                          //SE INSTANCIA A LA CLASE QUE SE COMUNICARA CON LA BD

            DataTable tabla = accesoAsignacion.buscarAsignacion();                                                                   //SE LE PIDE AL ACCESEXPOSICION QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA

            List<AsignacionVisita> listaAsignacionesVisitas = new List<AsignacionVisita>();


            foreach (DataRow row in tabla.Rows)
            {
                AsignacionVisita asignacion = new AsignacionVisita();
                string cuitEmpleado = row["cuitGuiaAsignado"].ToString();
                asignacion.FechaHoraInicio = DateTime.Parse(row["fechaHoraInicio"].ToString());
                asignacion.FechaHoraFin = DateTime.Parse(row["fechaHoraFin"].ToString());
                
                Empleado empleado = new Empleado(cuitEmpleado);
                asignacion.GuiaAsignado = empleado;
                
                listaAsignacionesVisitas.Add(asignacion);
            }
            
            return listaAsignacionesVisitas;
        }



        public List<Empleado> buscarGuiasDispFechaReserva(Sede sedeSeleccionada, DateTime fechaHoraReservaNueva, int duracionEstimada, List<AsignacionVisita> listaAsignacionesVisita, List<Empleado> listaEmpleados)       
        {
            List<Empleado> listaGuiasDisponibles = new List<Empleado>();                                                                          //BUSCA GUIAS DISPONIBLES
            foreach (Empleado empleado in listaEmpleados)
            {
                if (empleado.getGuiaDispEnHorario(sedeSeleccionada, fechaHoraReservaNueva, duracionEstimada, listaAsignacionesVisita))            //CADA EMPLEADO CONTESTA SI ESTA DISPONIBLE
                {
                    listaGuiasDisponibles.Add(empleado);
                }
            }
            return listaGuiasDisponibles;
        }



        public DataTable obtenerDatosEmpleadoDisponible(List<Empleado> listaEmpleadoDisponibles)                                                   //CREA UNA TABLA CON LOS DATOS DE LOS EMPLEADOS DISPONIBLES
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Apellido");
            tabla.Columns.Add("Cuit");

            foreach (Empleado empleado in listaEmpleadoDisponibles)
            {
                tabla.Rows.Add(empleado.mostrarNombre(), empleado.Apellido, empleado.Cuit);
            }
            return tabla;
        }



        public bool hayGuiasSuficientes(int cantidadGuiasNecesarios, List<Empleado> listaGuiaDisponibles)                                          //VERIFICA QUE HAYAN SUFICIENTES GUIAS PARA LLEVAR A CABO LA VISITA
        {
            bool resultado = false;
            if (cantidadGuiasNecesarios <= listaGuiaDisponibles.Count())
            {
                resultado = true;
            }
            return resultado;
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarGuias(List<string> cuitGuiaSeleccionado)                                                               
        {
            List<Empleado> listaEmpleados = cargarEmpleados();                                                                                      //SE GUARDAN LOS GUIAS SELECCIONADOS
            this.guiaSeleccionados = new List<Empleado>();
            List<ReservaVisita> listaReservas = cargarReservas();
            List<Estado> listaEstado = cargarEstados();

            foreach (Empleado empleado in listaEmpleados)
            {
                for (int i = 0; i < cuitGuiaSeleccionado.Count; i++)
                {
                    if (cuitGuiaSeleccionado[i].Equals(empleado.mostrarCuit()))
                    {
                        this.guiaSeleccionados.Add(empleado);
                    }
                }
            }

            Empleado empleadoLogueado = buscarEmpleadoLogueado();
            int ultNumReserva = buscarUltimoNroReserva(listaReservas);
            Estado estadoNuevaReserva = buscarEstadoReserva(listaEstado);
            ReservaVisita nuevaReserva = registrarReserva(estadoNuevaReserva, ultNumReserva, empleadoLogueado, this.fechaHoraReserva, this.duracionEstimadaReserva, this.exposicionesSeleccionadas, this.escuelaSeleccionada, this.cantidadVisitantes, this.guiaSeleccionados);
            ADGestor accesoGestor = new ADGestor();
            pantallaReservaDeVisita.mostrarReserva(nuevaReserva);
                   
            //finCU(accesoGestor.InsertarReserva(nuevaReserva));



        }

        

        
        public List<ReservaVisita> cargarReservas()                                                 //CREA OBJETOS RESERVAS VISITA A PARTIR DE FILAS DE UNA TABLA Y LOS AGREGA A UNA LISTA DE RESERVASVISITAS
        {
            ADReservaVisita accesoGestor = new ADReservaVisita();                                               //INSTANCIA LA CLASE DE ACCESO A DATOS
            DataTable tabla = new DataTable();
            tabla = accesoGestor.cargarReserva();                                                   //SE LE PIDE AL ACCESOGESTOR QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA

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



        public List<Estado> cargarEstados()                                                             //CREA OBJETOS ESTADOS A PARTIR DE FILAS DE UNA TABLA Y LOS AGREGA A UNA LISTA DE ESTADOS
        {
            ADEstados accesoGestor = new ADEstados();                                                    //INSTANCIA LA CLASE DE ACCESO A DATOS
            DataTable tabla = new DataTable();
            tabla = accesoGestor.buscarEstado();                                                         //SE LE PIDE AL ACCESOGESTOR QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA

            List<Estado> listaEstado = new List<Estado>();

            foreach (DataRow row in tabla.Rows)
            {
                Estado estado = new Estado();
                estado.Ambito = (row["ambito"].ToString());
                estado.Nombre = (row["nombre"].ToString());


                listaEstado.Add(estado);
            }

            return listaEstado;
        }



        public Empleado buscarEmpleadoLogueado()                                                       //LE PIDE A LA SESION QUE OBTENGA SU EMPLEADO
        {
            Empleado empleadoLogueado = this.sesionActual.getEmpleadoEnSesion();
            return empleadoLogueado;
        }



        public int buscarUltimoNroReserva(List<ReservaVisita> listaReservas)                            //BUSCA EL ULTIMO NUMERO DE LA RESERVA
        {
            int ultNumReserva = 0;
            foreach (ReservaVisita reserva in listaReservas)
            {
                if (reserva.NumeroReserva > ultNumReserva)
                {
                    ultNumReserva = reserva.NumeroReserva;
                }
            }
            ultNumReserva = ultNumReserva + 1;

            return ultNumReserva;
        }



        public Estado buscarEstadoReserva(List<Estado> listaEstados)                                    //BUSCA EL ESTADO A ASIGNAR AL NUEVO CAMBIOESTADO RESERVA
        {
            Estado estadoNuevaReserva = new Estado();

            foreach (Estado estado in listaEstados)
            {
                if (estado.esAmbitoReserva() && estado.esPendienteDeConfirmacion())                     //CADA ESTADO RESPONDE SI ES AMBITO RESERVA Y PENDIENTE DE CONFIRMACION
                {
                    estadoNuevaReserva = estado;
                    break;
                }
            }
           
            return estadoNuevaReserva;
        }

        

        public ReservaVisita registrarReserva(Estado estadoNuevaReserva, int numeroNuevaReserva, Empleado empleado, DateTime fechaHoraReserva, int duracionEstimada, List<Exposicion> listaExposicones, Escuela escuela, int cantidadVisitantes, List<Empleado> guiasAsignados)
        {
                                                                                                        //CREA UNA NUEVA RESERVA
            DateTime fechaHoraActual = DateTime.Now;
            DateTime fechaHoraFinReserva = fechaHoraReserva.AddMinutes(Convert.ToDouble(duracionEstimada));
            ReservaVisita nuevaReservaVisita = new ReservaVisita(estadoNuevaReserva, numeroNuevaReserva, empleado, fechaHoraReserva, duracionEstimada, listaExposicones, escuela, cantidadVisitantes, guiasAsignados, fechaHoraActual, fechaHoraFinReserva, this.sedeSeleccionada);        
            return nuevaReservaVisita;

        }

        


        public Sesion iniciarSesion()
        {
    
            Empleado empleadoDeUsuario = new Empleado();
            empleadoDeUsuario.Nombre = "Valentino";
            empleadoDeUsuario.Cuit = "27-43337940-9";
            empleadoDeUsuario.FechaNacimiento = DateTime.Parse("17/04/2001");
            empleadoDeUsuario.Apellido = "Giardino";

            Usuario usuarioEnSesion = new Usuario();
            usuarioEnSesion.Nombre = "Valentino";
            usuarioEnSesion.Empleado = empleadoDeUsuario;

            Sesion nuevaSesion = new Sesion();
            nuevaSesion.FechaInicio = DateTime.Parse(DateTime.Now.ToShortDateString());
            nuevaSesion.HoraInicio = DateTime.Now.ToShortTimeString();
            nuevaSesion.Usuario = usuarioEnSesion;

            return nuevaSesion;
        }

        

        public void finCU(bool resultado)                                                                           //SE FINALIZA EL REGISTRO DE LA RESERVA Y LLAMA A LA PANTALLA PARA QUE LO INFORME
        {
            pantallaReservaDeVisita.informarRegistroDeReserva(resultado);
        }

    }
}
