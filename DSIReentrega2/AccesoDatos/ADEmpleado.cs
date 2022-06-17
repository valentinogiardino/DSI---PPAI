using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.AccesoDatos
{
    class ADEmpleado
    {

        public DataTable buscarEmpleados()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {

                SqlCommand cmd = new SqlCommand();


                string consulta = "SELECT e.*, c.nombre as 'nombreCargo', c.descripcion as 'descripcionCargo', h.horaIngreso, h.horaSalida, s.nombre as 'nombreSede' FROM Empleado e join Cargo c on(e.cargo = c.idCargo) join HorarioEmpleado h on(e.cuit = h.cuitEmpleado) join Sede s on(e.idSede = s.idSede)";

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
                throw;
            }
            finally
            {
                cn.Close();
            }

        }

        public DataTable buscarHorario(string cuit)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {

                SqlCommand cmd = new SqlCommand();


                string consulta = "select * from HorarioEmpleado h where h.cuitEmpleado = @cuit";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cuit", cuit);
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
