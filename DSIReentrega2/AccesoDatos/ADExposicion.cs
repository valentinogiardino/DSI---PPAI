using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.AccesoDatos
{
    public class ADExposicion
    {
        public ADExposicion()
        {

        }
        public DataTable buscarExposiciones(string nombreSedeSeleccionada)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {

                SqlCommand cmd = new SqlCommand();


                string consulta = "select e.fechaFin, e.fechaFinReplanificada, e.fechaInicio, e.fechaInicioReplanificada, e.horaApertura, e.horaCierre, e.nombre as 'nombreExposicion', e.tipoExposicion, e.publicoDestino, s.nombre as 'nombreSede', t.nombre as 'nombreTipoExposicion', t.descripcion, p.nombre as 'nombrePublicoDestino', p.caracteristicas from exposicion e join sede s on (e.idSede = s.idSede) join TipoExposicion t on (e.tipoExposicion = t.id) join PublicoDestino p on (e.publicoDestino = p.id) where s.nombre = @nombreSede";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombreSede", nombreSedeSeleccionada);
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
                throw;
            }
            finally
            {
                cn.Close();
            }

        }


        public DataTable buscarDetalleExposiciones(string nombreExposicion)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {

                SqlCommand cmd = new SqlCommand();


                string consulta = "select d.lugarAsignado, o.nombreObra,o.codigo, o.alto, o.ancho,o.duracionExtendida, o.duracionResumida, o.fechaCreacion, o.fechaPrimerIngreso, o.peso, o.valuacion, o.descripcion from DetalleExposicion d join Exposicion e on (d.idExposicion = e.idExposicion) join Obra o on (d.codigoObra = o.codigo) where e.nombre = @nombreExpo";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombreExpo", nombreExposicion);
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
                throw;
            }
            finally
            {
                cn.Close();
            }

        }





    }
}
