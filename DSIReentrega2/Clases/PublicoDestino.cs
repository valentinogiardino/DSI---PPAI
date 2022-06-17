using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class PublicoDestino
    {
        private string nombre;
        private string caracteristicas;

        public PublicoDestino(string nombre, string caracteristicas)
        {
            this.nombre = nombre;
            this.caracteristicas = caracteristicas;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Caracteristicas { get => caracteristicas; set => caracteristicas = value; }


        public string getPublicoDestino()                                                               //DEVUELVE EL NOMBRE DEL PUBLICO DESTINO
        {
            return nombre;
        }


    }
}
