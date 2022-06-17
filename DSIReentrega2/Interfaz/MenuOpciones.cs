using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSIReentrega.Clases
{
    public partial class MenuOpciones : Form
    {
        public MenuOpciones()
        {
            InitializeComponent();
        }

        private void btnRegReservaVisita_Click(object sender, EventArgs e)          //AL HACER CLICK EN EL BOTON REGISTRAR RESERVA VISITA, SE INVOCA UN METODO
                                                                                    //DE LA PANTALLARESERVAVISITA PARA QUE SE INICIE EL REGISTRO
        {
            PantallaReservaDeVisita pantallaReservaDeVisita = new PantallaReservaDeVisita();
            pantallaReservaDeVisita.tomarOpcReservaVisita();
            this.Hide();
        }
    }
}
