using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases.Strategy
{
    public class EstrategiaCompleta : EstrategiaTipoVisita
    {
        public EstrategiaCompleta()
        {
        }

        public List<Exposicion> buscarExposiciones(DateTime fechaHoraActual, Sede sedeSeleccionada)
        {

            List<Exposicion> listaExposicionesTemporales = sedeSeleccionada.buscarExposicionesTempVigentes(fechaHoraActual);              //IMPLEMENTA EL METODO POLIMORFICO PARA BUSCAR LAS EXPOSICIONES DE
            List<Exposicion> listaExposicionesPermanentes = sedeSeleccionada.buscarExposicionesPermVigentes(fechaHoraActual);             //LA SEDE SEGUN EL TIPO DE VISITA SELECCIONADO. EN ESTE CASO 
            foreach (var exposicionPermanente in listaExposicionesPermanentes)                                                            //LE DICE A LA SEDE QUE BUSQUE LAS EXPOSICIONES TEMPORALES Y PERMANENTES VIGENTES
            {
                listaExposicionesTemporales.Add(exposicionPermanente);
            }                                                                                                                                 
            return listaExposicionesTemporales;                                                                                           
        }

        public void mostrarExposiciones(List<Exposicion> listaExposiciones, Sede sedeSeleccionada, GestorReservaVisita gestorReservaVisita)       //LE DICE A LA SEDE QUE BUSQUE LAS EXPOSICIONES TEMPORALES VIGENTES
                                                                                                                                                //DE LA SEDE SEGUN EL TIPO DE VISITA SELECCIONADO. EN ESTE CASO
        {                                                                                                                                       //INVOCA AL METODO DEL GESTOR PARA MOSTRAR DATOS DE EXPOSICIONES DE VISITA COMPLETA
            DataTable datosExposiciones = sedeSeleccionada.obtenerDatosExposicionVigente(listaExposiciones);                    //CREA UNA TABLA CON LOS DATOS DE LAS EXPOSICIONES SELECCIONADAS
            gestorReservaVisita.mostrarDatosExpoCompleta(datosExposiciones);                                                    //LLAMA A LA PANTALLA PARA QUE MUESTRE LAS EXPOSICIONES
        }

        public int buscarDuracionExposiciones(List<Exposicion> listaExposicionesSeleccionadas, Sede sedeSeleccionada)                           //IMPLEMENTA EL METODO POLIMORFICO PARA CALCULAR LA DURACION ESTIMADA DE LA RESERVA
                                                                                                                                                //SEGUN EL TIPO DE VISITA SELECCINADO. EN ESTE CASO INVOCA EL METODO DE LA SEDE QUE
        {                                                                                                                                       //OBTIENE LA DURACION RESUMIDA DE LAS OBRAS
            int duracionEstimadaReserva = sedeSeleccionada.buscarDuracResumObras(listaExposicionesSeleccionadas);
            return duracionEstimadaReserva;
        }
    }
}
