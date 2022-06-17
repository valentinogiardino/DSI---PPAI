using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases.Strategy
{
    public class EstrategiaPorExposicion : EstrategiaTipoVisita
    {
        public EstrategiaPorExposicion()
        {
        }

        public List<Exposicion> buscarExposiciones(DateTime fechaHoraActual, Sede sedeSeleccionada)                                                 //IMPLEMENTA EL METODO POLIMORFICO PARA BUSCAR LAS EXPOSICIONES DE
        {                                                                                                                                           //LA SEDE SEGUN EL TIPO DE VISITA SELECCIONADO. EN ESTE CASO 
                                                                                                                                                    //LE DICE A LA SEDE QUE BUSQUE LAS EXPOSICIONES TEMPORALES VIGENTES
            List<Exposicion> listaExposiciones = sedeSeleccionada.buscarExposicionesTempVigentes(fechaHoraActual);
            return listaExposiciones;
        }

        public void mostrarExposiciones(List<Exposicion> listaExposiciones, Sede sedeSeleccionada, GestorReservaVisita gestorReservaVisita)          //IMPLEMENTA EL METODO POLIMORFICO PARA MOSTRAR LAS EXPOSICIONES
        {                                                                                                                                          //DE LA SEDE SEGUN EL TIPO DE VISITA SELECCIONADO. EN ESTE CASO
                                                                                                                                          //INVOCA AL METODO DEL GESTOR PARA MOSTRAR DATOS DE EXPOSICIONES DE VISITA POR EXPOSICON
            DataTable datosExposiciones = sedeSeleccionada.obtenerDatosExposicionVigente(listaExposiciones);                            //CREA UNA TABLA CON LOS DATOS DE LAS EXPOSICIONES SELECCIONADAS
            gestorReservaVisita.mostrarDatosExpoPorExposicion(datosExposiciones);                                                      
        }

        public int buscarDuracionExposiciones(List<Exposicion> listaExposicionesSeleccionadas, Sede sedeSeleccionada)                       //IMPLEMNTA EL METODO POLIMORFICO PARA CALCULAR LA DURACION ESTIMADA DE LA RESERVA
        {                                                                                                                                   //SEGUN EL TIPO DE VISITA SELECCINADO. EN ESTE CASO INVOCA EL METODO DE LA SEDE QUE 
            int duracionEstimadaReserva = sedeSeleccionada.buscarDuracExtObras(listaExposicionesSeleccionadas);                             //OBTIENE LA DURACION EXTENDIDA DE LAS OBRAS
            return duracionEstimadaReserva;
        }
    }
}
