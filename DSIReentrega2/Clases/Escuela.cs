using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Escuela
    {
        private string domicilio;
        private string mail;
        private string nombre;
        private string telefonoCelular;
        private string telefonoFijo;

        public Escuela(string domicilio, string mail, string nombre, string telefonoCelular, string telefonoFijo)
        {
            this.domicilio = domicilio;
            this.mail = mail;
            this.nombre = nombre;
            this.telefonoCelular = telefonoCelular;
            this.telefonoFijo = telefonoFijo;
        }

        public Escuela()
        {

        }

        public string Domicilio { get => domicilio; set => domicilio = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string TelefonoCelular { get => telefonoCelular; set => telefonoCelular = value; }
        public string TelefonoFijo { get => telefonoFijo; set => telefonoFijo = value; }


        public string mostrarNombre()
        {
            return nombre;
        }

        public bool sosEscuela(string nombreEscuela)                    //LA ESCUELA RESPONDE SI ES POR LA QUE PREGUNTAN
        {
            bool resultado = false;
            if (nombreEscuela == this.mostrarNombre())
            {
                resultado = true;
            }
            return resultado;
        }
    }
}
