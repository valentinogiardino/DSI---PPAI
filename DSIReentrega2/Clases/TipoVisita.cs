using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class TipoVisita
    {
        private string nombre;

        public TipoVisita(string nombre)
        {
            this.nombre = nombre;
        }

        public TipoVisita()
        {

        }

        public string mostrarNombre()                                         //SE RETORNA EL NOMBRE DEL TIPO VISITA
        {
            return Nombre;
        }
        public string Nombre { get => nombre; set => nombre = value; }


        public bool sosTipoVisita(string nombreTipoVisita)                    //EL TIPOVISITA RESPONDE SI ES POR EL QUE PREGUNTAN
        {
            bool resultado = false;
            if (nombreTipoVisita == this.mostrarNombre())
            {
                resultado = true;
            }
            return resultado;
        }

    }
}
