using DSIReentrega.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace DSIReentrega
{
    public partial class PantallaReservaDeVisita : Form
    {

        private GestorReservaVisita gestor;                                                     //DENTRO DE LA CLASE PANTALLA SE AGREGA UN PUNTERO AL GESTOR

        public GestorReservaVisita Gestor { get => gestor; set => gestor = value; }




        //OTROS METODOS DE LA PANTALLA

        public PantallaReservaDeVisita()                                                        //METODO CONSTRUCTOR DE PANTALLA. EN EL MISMO SE INSTANCIA AL GESTOR MANDANDOLE COMO PARAMETRO LA PROPIA PANTALLA
        {
            InitializeComponent();
            gestor = new GestorReservaVisita(this);
        }



        public void tomarOpcReservaVisita()                                                     //SE LLAMA AL METODO HABILITAR VENTANA
        {
            habilitarVentana();
            
        }
        public void habilitarVentana()                                                          //HABILITA LA PANTALLA
        {
            this.ShowDialog();
            
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PantallaReservaDeVisita_Load(object sender, EventArgs e)                  //EVENTO QUE SUCEDE AL INICIARSE LA PANTALLA
        {
            ocultarElementos();            
            this.gestor.tomarRegReservaVisita();                                                //SE LLAMA AL METODO DEL GESTOR PARA QUE TOME EL REGISTRO DE LA VISITA
        }



        public void mostrarEscuela(List<String> listaNombresEscuelas)                           //RECIBE UNA LISTA DE ESCUELAS Y LAS USA PARA CARGAR EL COMBOBOX
        {
            foreach (String nombre in listaNombresEscuelas)
            {
                cmbEscuelas.Items.Add(nombre);
            }
        }



        public void solicitarSeleccionEscuela()                                                 //SOLICITA LA SELECCION DE UN CAMPO HABILITANDO EL MISMO
        {
            cmbEscuelas.Enabled = true;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cmbEscuelas_SelectionChangeCommitted(object sender, EventArgs e)              //AL SELECCIONARSE UN ITEM DEL COMOBOX, SE LLAMA AL GESTOR PARA QUE TOME LA SELECCION
        {
            string seleccionEscuela = tomarSeleccionEscuela();
            gestor.tomarEscuela(seleccionEscuela);
        }



        public string tomarSeleccionEscuela()                                                       //GUARDA EL ITEM SELECCIONADO EN EL COMBOBOX
        {
            string seleccionEscuela = cmbEscuelas.SelectedItem.ToString();
            return seleccionEscuela;
        }


    
        public void solicitarCantVisitantes()                                                       //SOLICITA LA SELECCION DE UN CAMPO HABILITANDO EL MISMO
        {
            txtCantidadVisitantes.Enabled = true;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtCantidadVisitantes_KeyPress_1(object sender, KeyPressEventArgs e)           //AL INGRESARSE UNA CANTIDAD DE VISITANTES, SE LLAMA AL GESTOR PARA QUE TOME LA CANTIDAD INGRESADA
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))                                            //SE CONFIRMA EL INGRESO PULSANDO LA TECLA ENTER
            {
                int cantidadVisitantes = tomarCantVisitantes();                
                gestor.tomarCantVisitantes(cantidadVisitantes); ;
                e.Handled = true;
            }
        }



        public int tomarCantVisitantes()                                                           //GUARDA EL CONTENIDO INGRESADO EN EL TEXTBOX
        {
            int cantidadVisitantes = int.Parse(txtCantidadVisitantes.Text);
            return cantidadVisitantes;
        }



        public void mostrarSedes(List<String> listaNombreSede)                                     //RECIBE UNA LISTA DE SEDES Y LAS USA PARA CARGAR EL COMBOBOX
        {
            cmbSedes.Items.Clear();
            foreach (String nombre in listaNombreSede)
            {
                cmbSedes.Items.Add(nombre);
            }
        }



        public void solicitarSeleccionSede()                                                        //SOLICITA LA SELECCION DE UN CAMPO HABILITANDO EL MISMO
        {
            cmbSedes.Enabled = true;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cmbSedes_SelectionChangeCommitted(object sender, EventArgs e)                 //AL SELECCIONARSE UN ITEM DEL COMOBOX, SE LLAMA AL GESTOR PARA QUE TOME LA SELECCION
        {
            string seleccionSede = tomarSeleccionSede();
            cmbTipoVisita.Items.Clear();
            grdExposiciones.Rows.Clear();
            grdExposicionesSeleccionadas.Rows.Clear();
            btnAgregar.Enabled = false;
            btnTomarExpos.Enabled = false;
            gestor.tomarSede(seleccionSede);
        }



        public string tomarSeleccionSede()                                                          //GUARDA EL ITEM SELECCIONADO EN EL COMBOBOX
        {
            string seleccionSede = cmbSedes.SelectedItem.ToString();
            return seleccionSede;
        }



        public void mostrarTiposVisita(List<String> listaNombreTipoVisita)                          //RECIBE UNA LISTA DE TIPOVISITA Y LAS USA PARA CARGAR EL COMBOBOX
        {
            foreach (String nombre in listaNombreTipoVisita)
            {
                cmbTipoVisita.Items.Add(nombre);
            }
        }



        public void pedirSelecTipoVisita()                                                           //SOLICITA LA SELECCION DE UN CAMPO HABILITANDO EL MISMO
        {
            cmbTipoVisita.Enabled = true;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cmbTipoVisita_SelectionChangeCommitted(object sender, EventArgs e)             //AL SELECCIONARSE UN ITEM DEL COMOBOX, SE LLAMA AL GESTOR PARA QUE TOME LA SELECCION
        {
            string seleccionTipoVisita = tomarSelectTipoVisita();
            grdExposiciones.Rows.Clear();
            grdExposicionesSeleccionadas.Rows.Clear();
            btnAgregar.Enabled = false;
            btnRemoverExposicion.Enabled = false;
            btnTomarExpos.Enabled = false;
            gestor.tomarTipoVisita(seleccionTipoVisita);
        }



        public string tomarSelectTipoVisita()                                                       //GUARDA EL ITEM SELECCIONADO EN EL COMBOBOX
        {
            string seleccionTipoVisita = cmbTipoVisita.SelectedItem.ToString();
            return seleccionTipoVisita;
        }



        public void mostrarDatosExpoTempVigentes(DataTable datosExposiciones)                       //RECIBE LOS DATOS DE LAS EXPOSICIONES TEMPORALES VIGENTES Y LAS CARGA EN LA GRILLA
        {
            reestablecerElementos();                                                                //SE LLAMA A UN METODO QUE RETORNA LOS ELEMENTOS A SU POSICION ORIGINAL
            if (datosExposiciones.Rows.Count.Equals(0))
            {
                MessageBox.Show("No hay exposiciones disponibles");
            }
            else
            {           
                for (int i = 0; i < datosExposiciones.Rows.Count; i++)
                {
                    string nombreExposicion = datosExposiciones.Rows[i][0].ToString();
                    string horaApertura = datosExposiciones.Rows[i][1].ToString();
                    string horaCierre = datosExposiciones.Rows[i][2].ToString();
                    string nombrePublicoDestino = datosExposiciones.Rows[i][3].ToString();
                    grdExposiciones.Rows.Add(nombreExposicion, horaApertura, horaCierre, nombrePublicoDestino);
                }
                btnAgregar.Enabled = true;                                                              //SE HABILITA EL BOTON PARA PERMITIR SELECCIONAR LAS EXPOSICONES A VISITAR
            }
        }



        public void mostrarDatosExpoTempPermVigentes(DataTable datosExposiciones)                       //RECIBE LOS DATOS DE LAS EXPOSICIONES TEMPORALES Y PERMANENTES VIGENTES Y LAS CARGA EN LAS DOS GRILLAS
        {
            moverElementos();                                                                           //SE LLAMA A UN METODO QUE MUEVE LOS ELEMENTOS MOSTRADOS.
            if (datosExposiciones.Rows.Count.Equals(0))
            {
                MessageBox.Show("No hay exposiciones disponibles");
            }
            else
            {               
                for (int i = 0; i < datosExposiciones.Rows.Count; i++)
                {
                    string nombreExposicion = datosExposiciones.Rows[i][0].ToString();
                    string horaApertura = datosExposiciones.Rows[i][1].ToString();
                    string horaCierre = datosExposiciones.Rows[i][2].ToString();
                    string nombrePublicoDestino = datosExposiciones.Rows[i][3].ToString();
                    grdExposiciones.Rows.Add(nombreExposicion, horaApertura, horaCierre, nombrePublicoDestino);
                    grdExposicionesSeleccionadas.Rows.Add(nombreExposicion, horaApertura, horaCierre, nombrePublicoDestino);
                }
                btnAgregar.Enabled = false;                                                             //SE DESABILITA EL BOTON PARA SELECCIONAR EXPOSICIONES, YA QUE SE VISITARAN TODAS LAS VIGENTES DEL MUSEO
                btnTomarExpos.Enabled = true;                                                           //SE HABILITA EL BOTON PARA TOMAR LAS EXPOSICIONES Y ASI PASAR A LA SIGUIENTE INTERACCION
            }
        }


        private void moverElementos()                                                               //CAMBIA LA POSICION DE LOS ELEMENTOS PARA ADECUARSE AL CONTENIDO A MOSTRAR
        {
            label3.Hide();
            grdExposiciones.Hide();
            label7.Hide();
            btnAgregar.Hide();
            label13.Hide();
            btnRemoverExposicion.Hide();
            grdExposicionesSeleccionadas.Location = new Point(270, 167);
            btnTomarExpos.Location = new Point(680, 303);
            label6.Location = new Point(270, 144);
        }
        private void reestablecerElementos()                                                        //CAMBIA LA POSICION DE LOS ELEMENTOS PARA ADECUARSE AL CONTENIDO A MOSTRAR
        {
            label3.Show();
            grdExposiciones.Show();
            label7.Show();
            btnAgregar.Show();
            label13.Show();
            btnRemoverExposicion.Show();
            grdExposicionesSeleccionadas.Location = new Point(579, 167);
            btnTomarExpos.Location = new Point(1000, 303);
            label6.Location = new Point(584, 144);
        }

                                                                        //METODOS PARA AJUSTAR LA INTERACCION CON LA INTERFAZ GRAFICA...
        private void ocultarElementos()
        {
            label5.Hide();
            txtFechaYHora.Hide();
            btnDisponibilidad.Hide();
            lblCantGuiasNecesarios.Hide();
            lblCantidadGuias.Hide();
            label2.Hide();
            grdGuias.Hide();
            label8.Hide();
            grdGuiasSeleccionados.Hide();
            label4.Hide();
            btnAgregarGuia.Hide();
            label9.Hide();
            btnRemoverGuia.Hide();
           
        }

        private void mostarFechaHoraReserva()
        {
            label5.Show();
            txtFechaYHora.Show();
            btnDisponibilidad.Show();
        }

        private void mostrarCantGuias()
        {
            lblCantGuiasNecesarios.Show();
            lblCantidadGuias.Show();
        }

        private void mostrarGrillasGuias()
        {
            grdGuias.Rows.Clear();
            grdGuiasSeleccionados.Rows.Clear();
            label2.Show();
            grdGuias.Show();
            label8.Show();
            grdGuiasSeleccionados.Show();
            label4.Show();
            btnAgregarGuia.Show();
            label9.Show();
            btnRemoverGuia.Show();
        }

        private void ocultarGrillasGuias()
        {
            grdGuias.Rows.Clear();
            grdGuiasSeleccionados.Rows.Clear();
            label2.Hide();
            grdGuias.Hide();
            label8.Hide();
            grdGuiasSeleccionados.Hide();
            label4.Hide();
            btnAgregarGuia.Hide();
            label9.Hide();
            btnRemoverGuia.Hide();
            btnConfirmarReserva.Enabled = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnAgregar_Click(object sender, EventArgs e)                                  //AL HACER CLICK EN EL BOTON AGREGAR SE PASAN LAS EXPOSICIONES SELECCIONADAS A OTRA GRILLA
        {
            int i = grdExposiciones.CurrentRow.Index;
            int a = grdExposicionesSeleccionadas.Rows.Count;
            grdExposicionesSeleccionadas.Rows.Add();
            grdExposicionesSeleccionadas.Rows[a].Cells[0].Value = grdExposiciones.Rows[i].Cells[0].Value;
            grdExposicionesSeleccionadas.Rows[a].Cells[1].Value = grdExposiciones.Rows[i].Cells[1].Value;
            grdExposicionesSeleccionadas.Rows[a].Cells[2].Value = grdExposiciones.Rows[i].Cells[2].Value;
            grdExposicionesSeleccionadas.Rows[a].Cells[3].Value = grdExposiciones.Rows[i].Cells[3].Value;
            grdExposiciones.Rows.RemoveAt(i);
            btnRemoverExposicion.Enabled = true;
            btnTomarExpos.Enabled = true;
        }

        private void btnRemoverExposicion_Click(object sender, EventArgs e)                      //AL HACER CLICK EN EL BOTON REMOVER SE DEVUELVEN LAS EXPOSICIONES SELECCIONADAS A OTRA GRILLA                     
        {
            int i = grdExposicionesSeleccionadas.CurrentRow.Index;
            int a = grdExposiciones.Rows.Count;
            grdExposiciones.Rows.Add();
            grdExposiciones.Rows[a].Cells[0].Value = grdExposicionesSeleccionadas.Rows[i].Cells[0].Value;
            grdExposiciones.Rows[a].Cells[1].Value = grdExposicionesSeleccionadas.Rows[i].Cells[1].Value;
            grdExposiciones.Rows[a].Cells[2].Value = grdExposicionesSeleccionadas.Rows[i].Cells[2].Value;
            grdExposicionesSeleccionadas.Rows.RemoveAt(i);
            btnTomarExpos.Enabled = true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnTomarExpos_Click(object sender, EventArgs e)                                //AL HACER CLICK EN EL BOTON SE TERMINA LA SELECCION DE EXPOSICIONES Y SE PROCEDE A TOMAR SU SELECCION
        {
            if (grdExposicionesSeleccionadas.Rows.Count <= 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna exposicion");
            }
            else
            {
                tomarSelecExposicion();
                mostarFechaHoraReserva();
                btnDisponibilidad.Enabled = true;
            }
            
        }



        public void tomarSelecExposicion()                                                          //SE TOMAN LAS EXPOSICIONES SELECCIONADAS Y SE LAS PASA AL GESTOR PARA QUE LAS GUARDE
        {
            List<String> nombreExposicionesSeleccionadas = new List<string>();
            for (int i = 0; i < grdExposicionesSeleccionadas.Rows.Count; i++)
            {
                nombreExposicionesSeleccionadas.Add(grdExposicionesSeleccionadas.Rows[i].Cells[0].Value.ToString());
            }
            gestor.tomarExposiciones(nombreExposicionesSeleccionadas);
        }



        public void solicitarFechaHoraReserva()                                                     //SE HABILITA EL TXT FECHA Y HORA PARA CONTINUAR CON LA SIGUIENTE INTERACCION
        {
            txtFechaYHora.Enabled = true;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void tomarFechaHoraReserva(object sender, EventArgs e)                              //AL HACER CLICK EN EL BOTON VER DISPONIBILIDAD, SE TOMA LA FECHA Y HORA INGRESADA Y SE LA PASA AL GESTOR
        {
            DateTime fechaHoraReserva = DateTime.Parse(txtFechaYHora.Text);
            mostrarCantGuias();
            
            gestor.tomarFechaHoraReserva(fechaHoraReserva);
        }



        public void mostrarCantGuiasNecesarios(int cantidadGuias)                                  //MUESTRA LA CANTIDAD DE GUIAS NECESARIOS HABILITANDO UN LABEL
        {
            lblCantidadGuias.Visible = true;
            lblCantGuiasNecesarios.Visible = true;
            lblCantidadGuias.Text = cantidadGuias.ToString();
        }



        public void notificarSobrepaso()                                                           // --ALTERNATIVA 4 -- MUESTRA UN MENSAJE INFORMANDO QUE SE SOBREPARA LA CANTIDADA MAXIMA DE VISITANTES POR SEDE
        {
            MessageBox.Show("Se excede la capacidad máxima de visitantes para la sede.");
        }
        
        

        public void mostrarGuiasDisponibles(DataTable datosGuias, bool hayGuiasSuficientes)       //SI HAY GUIAS DISPONIBLES LOS MUESTRA, CASO CONTRARIO INFORMA SITUACION
        {
            if (hayGuiasSuficientes == false)
            {
                MessageBox.Show("No hay suficientes guias disponibles para esa cantidad de visitantes en la fecha y hora ingresada");
                ocultarGrillasGuias();
            }
            else
            {
                mostrarGrillasGuias();
                for (int i = 0; i < datosGuias.Rows.Count; i++)
                {
                    string nombreGuia = datosGuias.Rows[i][0].ToString();
                    string apellido = datosGuias.Rows[i][1].ToString();
                    string cuit = datosGuias.Rows[i][2].ToString();

                    grdGuias.Rows.Add(nombreGuia, apellido, cuit);
                }

                btnAgregarGuia.Enabled = true;
            }
           
        }



        public void solicitarSelecGuias()                                                           //SOLICITA LA SELECCION DE GUIAS HABILITANDO EL BOTON AGREGAR
        {
            btnAgregarGuia.Enabled = true;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnAgregarGuia_Click(object sender, EventArgs e)                              //AGREGA LOS GUIAS SELECCIONADOS A OTRA GRILLA
        {
           
            int i = grdGuias.CurrentRow.Index;
            int a = grdGuiasSeleccionados.Rows.Count;
            grdGuiasSeleccionados.Rows.Add();
            grdGuiasSeleccionados.Rows[a].Cells[0].Value = grdGuias.Rows[i].Cells[0].Value;
            grdGuiasSeleccionados.Rows[a].Cells[1].Value = grdGuias.Rows[i].Cells[1].Value;
            grdGuiasSeleccionados.Rows[a].Cells[2].Value = grdGuias.Rows[i].Cells[2].Value;
            grdGuias.Rows.RemoveAt(i);
            btnRemoverGuia.Enabled = true;
            btnConfirmarReserva.Enabled = true;
        }

        private void btnRemoverGuia_Click(object sender, EventArgs e)                             //REMUEVE LOS GUIAS SELECCIONADOS DE LA GRILLA
        {
            int i = grdGuiasSeleccionados.CurrentRow.Index;
            int a = grdGuias.Rows.Count;
            grdGuias.Rows.Add();
            grdGuias.Rows[a].Cells[0].Value = grdGuiasSeleccionados.Rows[i].Cells[0].Value;
            grdGuias.Rows[a].Cells[1].Value = grdGuiasSeleccionados.Rows[i].Cells[1].Value;
            grdGuias.Rows[a].Cells[2].Value = grdGuiasSeleccionados.Rows[i].Cells[2].Value;
            grdGuiasSeleccionados.Rows.RemoveAt(i);
            btnConfirmarReserva.Enabled = true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void btnConfirmarReserva_Click(object sender, EventArgs e)                      //AL HACER CLIC EN EL BOTON CONFIRMAR SE PROCEDE A REGISTRAR LA RESERVA
        {
            tomarSeleccionGuias();                                                              
        }



        public void tomarSeleccionGuias()                                                      //SE TOMAN LOS GUIAS SELECCIONADOS Y SE LOS PASA AL GESTOR PARA QUE LOS GUARDE
        {
            List<String> cuitGuiasSeleccionados = new List<string>();
            for (int i = 0; i < grdGuiasSeleccionados.Rows.Count; i++)
            {
                cuitGuiasSeleccionados.Add(grdGuiasSeleccionados.Rows[i].Cells[2].Value.ToString());
            }
            if (cuitGuiasSeleccionados.Count() < int.Parse(lblCantidadGuias.Text.ToString()))
            {
                MessageBox.Show("Es necesario seleccionar mas guias");
            }
            else
            {
                gestor.tomarGuias(cuitGuiasSeleccionados);
            }
            
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void informarRegistroDeReserva(bool resultado)                                               //AL INTENTAR REGISTRARSE LA RESERVA SE MUESTRA UN MENSAJE INFORMANDO LA SITUACION
        {
            if (resultado)
            {
                MessageBox.Show("Reserva Registrada con Exito!");
            }
            else
            {
                MessageBox.Show("Hubo un error, la reserva no pudo ser registrada");
            }
        }


        public void mostrarReserva(ReservaVisita reserva)                                                   //DESPLEGA UN MESSAGE BOX CONTENIENDO LOS DATOS CARGADOS PARA LA NUEVA RESERVA.
        {
            String contenedor = "************************************************************************";
            String encabezado = "////////////////////////////////////////////////////////////////////////////////////////////";
            String separador = "---------------------------------------------------------------------------------";
            var sb = new StringBuilder();
            String linea = contenedor + "\n" + "                                               Reserva N°: " + reserva.NumeroReserva.ToString() + "\n" + encabezado + "\n" + "Sede: " + reserva.Sede.mostrarNombre() + "\n" + 
                "Empleado Responsable: " + reserva.EmpleadoResponsable.mostrarNombre() + "\n" 
                + "Escuela: " + reserva.Escuela.Nombre + "\n" + "Cantidad de alumnos: " + reserva.CantidadAlumnos.ToString() + "\n" + "Fecha y hora de la reserva: " + reserva.FechaHoraReserva.ToString() + "\n" +
                "Duración: " + reserva.DuracionEstimada.ToString() + " minutos" + "\n" + "Estado de la Reserva: " + reserva.CambioEstado.Estado.Nombre;
            sb.AppendLine(linea);
            AsignacionVisita asign = reserva.AsignacionGuia.First();
            sb.AppendLine(encabezado + "\n" + "GUIAS ASIGNADOS: "+ "\n" + "   - Inicio Asignacion: " + asign.FechaHoraInicio.ToString() + Environment.NewLine + "   - Fin Asignacion: " + asign.FechaHoraFin.ToString());
            foreach (AsignacionVisita asignacion in reserva.AsignacionGuia)
            {
                sb.AppendLine ("        * " + asignacion.GuiaAsignado.mostrarNombre() +  " (" + asignacion.GuiaAsignado.Cuit.ToString() + ")");
            }
            sb.AppendLine(encabezado);
            sb.AppendLine("EXPOSICIONES A VISITAR: ");
            foreach (Exposicion expo in reserva.Exposiciones)
            {
                sb.AppendLine(separador);
                sb.AppendLine("                                           -" + expo.mostrarNombre());
            }
            sb.AppendLine(contenedor);
            MessageBox.Show(sb.ToString());
        }

        private void btnBorrarCampos_Click(object sender, EventArgs e)                                              //Reestablece los campos
        {
            Reiniciar();
        }

        private void Reiniciar()                                                //SE REESTABLECEN LOS CAMPOS DE LA PANTALLA. LA MISMA VUELVE A SU CONFIGURACION INICIAL.
        {
            label5.Hide();
            txtFechaYHora.Clear();
            txtFechaYHora.Hide();
            btnDisponibilidad.Hide();
            lblCantGuiasNecesarios.Hide();
            lblCantidadGuias.Text = "";
            lblCantidadGuias.Hide();
            label2.Hide();
            grdGuias.Hide();
            label8.Hide();
            grdGuiasSeleccionados.Hide();
            label4.Hide();
            btnAgregarGuia.Hide();
            label9.Hide();
            btnRemoverGuia.Hide();
            reestablecerElementos();

            cmbEscuelas.SelectedIndex = -1;
            txtCantidadVisitantes.Clear();
            txtCantidadVisitantes.Enabled = false;
            cmbSedes.SelectedIndex = -1;
            cmbSedes.Enabled = false;
            cmbTipoVisita.SelectedIndex = -1;
            cmbTipoVisita.Enabled = false;
            grdExposiciones.Rows.Clear();
            grdExposicionesSeleccionadas.Rows.Clear();
            btnTomarExpos.Enabled = false;
            btnAgregar.Enabled = false;
            btnRemoverExposicion.Enabled = false;

            
            
        }

        

    
    }
}
