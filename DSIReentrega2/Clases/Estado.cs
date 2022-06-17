using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Estado
    {
        private string nombre;
        private string ambito;
        private string descripcion;

        public Estado(string nombre, string ambito, string descripcion)
        {
            this.nombre = nombre;
            this.ambito = ambito;
            this.descripcion = descripcion;
        }
        public Estado()
        {

        }

        public bool esAmbitoReserva()                                                   //EL PROPIO ESTADO RESUELVE SI ES DEL AMBITO RESERVA
        {
            bool resultado = false;
            if (this.ambito == "Reserva")
            {
                resultado = true;
            }
            return resultado;

        }

        public bool esPendienteDeConfirmacion()                                     //EL PROPIO ESTADO RESUELVE SI ES PENDIENTE DE CONFIRMACION
        {
            bool resultado = false;
            if (this.nombre == "Pendiente de confirmación")
            {
                resultado = true;
            }
            return resultado;

        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Ambito { get => ambito; set => ambito = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
