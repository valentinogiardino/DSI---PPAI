using DSIReentrega.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.AccesoDatos
{
    public class ADGestor
    {
        public DataTable buscarEscuela()                                                                    //Hace una consulta a la base de datos y retorna una tabla conteniendo los datos obtenidos
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {

                SqlCommand cmd = new SqlCommand();


                string consulta = "Select * FROM Escuela";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                return tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }

        }

        

        public bool InsertarReserva(ReservaVisita reserva)                                              //Inserta la nueva reserva creada en la Base de Datos
        {

            int numeroReserva = reserva.NumeroReserva;                                                  
            DateTime fechaHoraReserva = reserva.FechaHoraReserva;
            DateTime fechaHoraCreacion = reserva.FechaHoraCreacion;
            int duracionEstimada = reserva.DuracionEstimada;
            int cantidadAlumnos = reserva.CantidadAlumnos;
            Empleado empleadoResponsable = reserva.EmpleadoResponsable;
            Escuela escuela = reserva.Escuela;
            List<Exposicion> exposiciones = reserva.Exposiciones;
            Sede sede = reserva.Sede;
            CambioEstado cambioEstado = reserva.CambioEstado;
            List<AsignacionVisita> listaAsignacionGuia = reserva.AsignacionGuia;

            int numSede = 0;
            if (sede.Nombre == "Sede1")
            {
                numSede = 1;
            }
            if (sede.Nombre == "Sede2")
            {
                numSede = 2;
            }
            if (sede.Nombre == "Sede3")
            {
                numSede = 3;
            }


            int idCambioEstado = this.ObtenerIdCambioEstado();                                  //Se buscan los ID necesarios para referencias las Claves Ajenas
            int idEscuela = this.ObtenerIdEscuela(escuela);
            

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlTransaction objTransaccion = null;
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {                                                                                   //Comienza la transacccion. Si se genera un error en algún paso, todo vuelve atrás.
                SqlCommand cmd = new SqlCommand();                                              //Primero insertamos la reserva
                string consultaReserva = "INSERT INTO ReservaVisita (numeroReserva, escuela, cantidadAlumnos, cantidadAlumnosConfirmada, fechaHoraCreacion, fechaHoraReserva, empleado, sede, cambioEstado, duracion) VALUES(@numeroReserva, @escuela,@cantidadAlumnos, @cantidadAlumnosConfirmada,@fechaHoraCreacion,@fechaHoraReserva,@empleado,@sede, @cambioEstado, @duracion)";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@numeroReserva", numeroReserva);
                cmd.Parameters.AddWithValue("@escuela", idEscuela);
                cmd.Parameters.AddWithValue("@cantidadAlumnos", cantidadAlumnos);
                cmd.Parameters.AddWithValue("@cantidadAlumnosConfirmada", 0);
                cmd.Parameters.AddWithValue("@fechaHoraCreacion", fechaHoraCreacion);
                cmd.Parameters.AddWithValue("@fechaHoraReserva", fechaHoraReserva);
                cmd.Parameters.AddWithValue("@empleado", empleadoResponsable.mostrarCuit());
                cmd.Parameters.AddWithValue("@sede", numSede);                
                cmd.Parameters.AddWithValue("@cambioEstado", idCambioEstado);
                cmd.Parameters.AddWithValue("@duracion", duracionEstimada);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consultaReserva;

                cn.Open();

                objTransaccion = cn.BeginTransaction("AltaDeCurso");

                cmd.Transaction = objTransaccion;

                cmd.Connection = cn;

                cmd.ExecuteNonQuery();

                                                                                                        //El siguiente paso es hacer las corresponidientes inserciones en la tabla CambioEstado
                string consultaCambioEstado = "INSERT INTO CambioEstado (fechaHoraInicio, estado, id) VALUES (@fechaHoraInicio,@estado, @id)";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@fechaHoraInicio", reserva.FechaHoraCreacion);
                cmd.Parameters.AddWithValue("@estado", 4);
                cmd.Parameters.AddWithValue("@id", idCambioEstado);

                cmd.CommandText = consultaCambioEstado;
                cmd.ExecuteNonQuery();

                foreach (AsignacionVisita asignacion in listaAsignacionGuia)                        //Se registran los guias asignados
                {
                    string consultaAsignacion = "INSERT INTO AsignacionVisita (cuitGuiaAsignado, fechaHoraInicio, fechaHoraFin, numeroReserva) VALUES (@cuitGuiaAsignado,@fechaHoraInicio,@fechaHoraFin,@numeroReserva)";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@cuitGuiaAsignado", asignacion.GuiaAsignado.mostrarCuit());
                    cmd.Parameters.AddWithValue("@fechaHoraInicio", asignacion.FechaHoraInicio);
                    cmd.Parameters.AddWithValue("@fechaHoraFin", asignacion.FechaHoraFin);
                    cmd.Parameters.AddWithValue("@numeroReserva", numeroReserva);
                    
                    cmd.CommandText = consultaAsignacion;
                    cmd.ExecuteNonQuery();

                }

                foreach (Exposicion exposicion in exposiciones)                                 //Se inserta el detalle reserva
                {
                    int idExpo = this.ObtenerIdExposicion(exposicion);
                    string consultaDetalleReserva = "INSERT INTO DetalleReserva (numeroreserva, idexposicion) VALUES (@numeroreserva,@idexposicion)";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@numeroreserva", numeroReserva);
                    cmd.Parameters.AddWithValue("@idexposicion", idExpo);
                    
                    cmd.CommandText = consultaDetalleReserva;
                    cmd.ExecuteNonQuery();
                }

                
                objTransaccion.Commit();
                return true;

            }
            catch (Exception ex)
            {
                objTransaccion.Rollback();                                          //En caso de error, toda la transaccion se de marcha atras.
                return false;
            }
            finally
            {
                cn.Close();
            }
        }

        private int ObtenerIdCambioEstado()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            int idCambioEstado;
            try
            {

                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT MAX(c.id) FROM CambioEstado c";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                idCambioEstado = (int)cmd.ExecuteScalar();
                idCambioEstado += 1;
                return idCambioEstado;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }
        private int ObtenerIdExposicion(Exposicion exposicion)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {

                SqlCommand cmd = new SqlCommand();
                string consulta4 = "SELECT e.idExposicion FROM Exposicion e where e.nombre = @nombre";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", exposicion.Nombre);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta4;

                cn.Open();
                cmd.Connection = cn;

                int idExpo = (int)cmd.ExecuteScalar();
                return idExpo;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }
        private int ObtenerIdEscuela(Escuela escuela)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {

                SqlCommand cmd = new SqlCommand();
                string consulta4 = "select e.idEscuela from Escuela e where e.nombre = @nombre";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", escuela.Nombre);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta4;

                cn.Open();
                cmd.Connection = cn;

                int idEscu = (int)cmd.ExecuteScalar();
                return idEscu;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
