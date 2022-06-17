using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class DetalleExposicion
    {
        private int lugarAsignado;
        private Obra obra;

        public DetalleExposicion(int lugarAsignado, Obra obra)
        {
            this.lugarAsignado = lugarAsignado;
            this.obra = obra;
        }

        public DetalleExposicion(Obra obra)
        {
            this.obra = obra;
        }

        public int LugarAsignado { get => lugarAsignado; set => lugarAsignado = value; }
        internal Obra Obra { get => obra; set => obra = value; }


        public int buscarDuracExtObras()                                            //LE PREGUNTA A SU OBRA POR SU DURACION EXTENDIDA
        {
            int duracionExtObra = this.Obra.getDuracionExtendida();
            return duracionExtObra;

        }

        public int buscarDuracResumObras()                                            //LE PREGUNTA A SU OBRA POR SU DURACION RESUMIDA
        {
            int duracionExtObra = this.Obra.getDuracionResumida();
            return duracionExtObra;

        }


    }
}
