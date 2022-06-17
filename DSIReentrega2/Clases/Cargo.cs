using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Cargo
    {
        private string nombre;
        private string descripcion;

        public Cargo(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public Cargo() { }


        public bool esGuia()                                                                //RESPONDE SI ES GUIA
        {
            bool resultado = false;
            if (this.nombre == "Guia")
            {
                resultado = true;
            }
            return resultado;
        }



    }
}
