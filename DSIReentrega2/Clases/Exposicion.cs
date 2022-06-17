using DSIReentrega.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Exposicion
    {
        private DateTime fechaFin;
        private DateTime fechaFinReplanificada;
        private DateTime fechaInicio;
        private DateTime fechaInicioReplanificada;
        private string horaApertura;
        private string horaCierre;
        private string nombre;
        private TipoExposicion tipoExposicion;
        //VER si es lista o asi solo para uno o muchos.
        private PublicoDestino publicoDestino;
        private Empleado empleado;
        private List<DetalleExposicion> detalleExposicion;

        public Exposicion(DateTime fechaFin, DateTime fechaFinReplanificada, DateTime fechaInicio, DateTime fechaInicioReplanificada, string horaApertura, string horaCierre, string nombre, TipoExposicion tipoExposicion, PublicoDestino publicoDestino, Empleado empleado, DetalleExposicion detalleExposicion)
        {
            this.fechaFin = fechaFin;
            this.fechaFinReplanificada = fechaFinReplanificada;
            this.fechaInicio = fechaInicio;
            this.fechaInicioReplanificada = fechaInicioReplanificada;
            this.horaApertura = horaApertura;
            this.horaCierre = horaCierre;
            this.nombre = nombre;
            this.tipoExposicion = tipoExposicion;
            this.publicoDestino = publicoDestino;
            this.empleado = empleado;
            
        }

        public Exposicion()
        {

        }


        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public DateTime FechaFinReplanificada { get => fechaFinReplanificada; set => fechaFinReplanificada = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaInicioReplanificada { get => fechaInicioReplanificada; set => fechaInicioReplanificada = value; }
        public string HoraApertura { get => horaApertura; set => horaApertura = value; }
        public string HoraCierre { get => horaCierre; set => horaCierre = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        internal TipoExposicion TipoExposicion { get => tipoExposicion; set => tipoExposicion = value; }
        internal PublicoDestino PublicoDestino { get => publicoDestino; set => publicoDestino = value; }
        internal Empleado Empleado { get => empleado; set => empleado = value; }
        public List<DetalleExposicion> DetalleExposicion { get => detalleExposicion; set => detalleExposicion = value; }

        public string obtenerPublicoDestino()
        {
            string nombrePublicoDestino  = this.publicoDestino.getPublicoDestino();
            return nombrePublicoDestino;

        }

        public List<String> getHorarioHabilitado()                                                                              //RETORNA EL HORARIO DE LA EMPOSICION
        {
            List<String> horarioHabilitado = new List<String>();
            string horaDesde = this.HoraApertura.ToString();
            string horaHasta = this.HoraCierre.ToString();
            horarioHabilitado.Add(horaDesde);
            horarioHabilitado.Add(horaHasta);
            return horarioHabilitado;
        }

        public bool esTempVigente(DateTime fechaHoraActual)                                                                     //RESPONDE SI ES TEMPORAL Y VIGENTE
        {
            bool resultado = false;
            bool vigente = esVigente(fechaHoraActual);
            
            if (this.tipoExposicion.esTemporal() && vigente)
            {
                resultado = true;
            }
            return resultado;
        }

        public bool esPermVigente(DateTime fechaHoraActual)                                                                     //RESPONDE SI ES PERMANENTE Y VIGENTE
        {
            bool resultado = false;
            bool vigente = esVigente(fechaHoraActual);

            if (this.tipoExposicion.esTemporal() != true && vigente)                                                            
            {
                resultado = true;
            }
            return resultado;
        }


        public bool esVigente(DateTime fechaHoraAcutal)                                                                         //VERIFICA SI ES VIGENTE 
                                                                                                                                //COMPLETARRRRRRRRRRRRRRRRRRRRRRRRRR COMENTARIOS
        {
            bool resultado = false;
            DateTime fecheActual = DateTime.Parse(fechaHoraAcutal.ToShortDateString());
            DateTime horaActual = DateTime.Parse(fechaHoraAcutal.ToShortTimeString());
            DateTime horaInicioConvertida = DateTime.Parse(this.horaApertura);
            DateTime horaFinConvertida = DateTime.Parse(this.horaCierre);
            DateTime fechaInicio = this.fechaInicio;
            DateTime fechaFin = this.FechaFin;
            int comparacionInicio = DateTime.Compare(fecheActual, fechaInicio);
            int comparacionCierre = DateTime.Compare(fecheActual, fechaFin);

            int comparacionHoraInicio = DateTime.Compare(horaActual, horaInicioConvertida);
            int comparacionHoraFin = DateTime.Compare(horaActual, horaFinConvertida);
            if (comparacionInicio > 0 && comparacionCierre < 0)
            {
                resultado = true;
            }
            else
            {
                if (comparacionInicio == 0)
                {
                    if (comparacionHoraInicio >= 0)
                    {
                        resultado = true;
                    }
                    
                }
                else
                {
                    if (comparacionCierre == 0)
                    {
                        if (comparacionHoraFin < 0)
                        {
                            resultado = true;
                        }
                    }
                }
            }
            
            return resultado;
        }

        public List<String> obtenerDatosExposicionVigente()                                                                            //RETORNA LOS DATOS DE LAS EXPOSICION
        {
            
            List<String> fila = new List<String>();
            List<String> horarios = this.getHorarioHabilitado();
            string nombreExposicion = this.mostrarNombre();
            string horaDesde = horarios[0];
            string horaHasta = horarios[1];
            string nombrePublicoDestino = this.publicoDestino.getPublicoDestino();

            fila.Add(nombreExposicion);
            fila.Add(horaDesde);
            fila.Add(horaHasta);
            fila.Add(nombrePublicoDestino);
            
            return fila;
        }

        public string mostrarNombre()                                                                                                   //DEVUELVE SU NOMBRE
        {
            return nombre;
        }

        public bool sosExposicion(string nombreExposicion)                                                                              //LA EXPOSICION RESPONDE SI ES POR LA QUE PREGUNTAN
        {
            bool resultado = false;
            if (nombreExposicion == this.mostrarNombre())
            {
                resultado = true;
            }
            return resultado;
        }


        public void buscarDetalleExposicion()
        {
            string nombreExpo = this.mostrarNombre();
            ADExposicion accesoExposciones = new ADExposicion();                                                                        //SE INSTANCIA A LA CLASE QUE SE COMUNICARA CON LA BD

            DataTable tabla = accesoExposciones.buscarDetalleExposiciones(nombreExpo);                                                  //SE LE PIDE AL ACCESEXPOSICION QUE HAGA LA CONEXION CON LA BD CON LO QUE RETORNA UNA TABLA

            List<DetalleExposicion> listaDetallesExposicion = new List<DetalleExposicion>();
            List<Exposicion> listaExposicionesTempVig = new List<Exposicion>();

            foreach (DataRow row in tabla.Rows)
            {
                Obra obra = new Obra();
                obra.NombreObra = row["nombreObra"].ToString();
                obra.Codigo = row["codigo"].ToString();
                obra.Alto = int.Parse(row["alto"].ToString());
                obra.Ancho = int.Parse(row["ancho"].ToString());
                obra.DuracionExtendida = int.Parse(row["duracionExtendida"].ToString());
                obra.DuracionResumida = int.Parse(row["duracionResumida"].ToString());
                obra.FechaCreacion = DateTime.Parse(row["fechaCreacion"].ToString());
                obra.FechaPrimerIngreso = DateTime.Parse(row["fechaPrimerIngreso"].ToString());
                obra.Peso = double.Parse(row["peso"].ToString());
                obra.Valuacion = double.Parse(row["valuacion"].ToString());
                obra.Descripcion = row["descripcion"].ToString();


                DetalleExposicion detalleExposicion = new DetalleExposicion(obra);
                detalleExposicion.LugarAsignado = int.Parse(row["lugarAsignado"].ToString());

                listaDetallesExposicion.Add(detalleExposicion);
            }

            this.DetalleExposicion = listaDetallesExposicion;
            /*return listaDetallesExposicion*/;
        }


        public int buscarDuracExtObras()                                                                                                  //LE DICE A CADA UNO DE SUS DETALLES QUE BUSQUE LA DURACION EXTENDIAD DE SUS OBRAS
        {   
            
            int duracionExtObra = 0;
            foreach (DetalleExposicion detalleExposicion in DetalleExposicion)
            {
                duracionExtObra +=  detalleExposicion.buscarDuracExtObras();
                
            }
            return duracionExtObra;
        }




        public int buscarDuracResumObras()                                                                                                  //LE DICE A CADA UNO DE SUS DETALLES QUE BUSQUE LA DURACION RESUMIDA DE SUS OBRAS
        {

            int duracionExtObra = 0;
            foreach (DetalleExposicion detalleExposicion in DetalleExposicion)
            {
                duracionExtObra += detalleExposicion.buscarDuracResumObras();

            }
            return duracionExtObra;
        }

    }
}
