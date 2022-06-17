using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class TipoExposicion
    {
        private string nombre;
        private string descripcion;

        public TipoExposicion(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public TipoExposicion()
        {

        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }


        public bool esTemporal()                                                            //RESPONDE SI ES TEMPORAL
        {
            bool resultado = false;
            if (this.Nombre == "Temporal")
            {
                resultado = true;
            }

            return resultado;
        }



    }
}
