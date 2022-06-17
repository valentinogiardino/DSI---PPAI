using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Usuario
    {
        private DateTime caducidad;
        private string contraseña;
        private string nombre;
        private Empleado empleado;

        public Usuario(DateTime caducidad, string contraseña, string nombre, Empleado empleado)
        {
            this.caducidad = caducidad;
            this.contraseña = contraseña;
            this.nombre = nombre;
            this.empleado = empleado;
        }

        public Usuario()
        {

        }

        public DateTime Caducidad { get => caducidad; set => caducidad = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        internal Empleado Empleado { get => empleado; set => empleado = value; }


        public Empleado obtenerEmpleado()                                                           //OBTIENE EL EMPLEADO EN SESION
        {
            Empleado empleadoEnSesion = this.Empleado;
            return empleadoEnSesion;
        }

    }
}
