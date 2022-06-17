using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases.Strategy
{
    public interface EstrategiaTipoVisita                   
    {
        List<Exposicion> buscarExposiciones(DateTime fechaHoraActual, Sede sedeSeleccionada);
        int buscarDuracionExposiciones(List<Exposicion> listaExposicionesSeleccionadas, Sede sedeSeleccionada);

        void mostrarExposiciones(List<Exposicion> listaExposiciones, Sede sedeSeleccionada, GestorReservaVisita gestorReservaVisita);
    }
}
