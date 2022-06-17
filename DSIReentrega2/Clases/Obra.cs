using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSIReentrega.Clases
{
    public class Obra
    {
        private int alto;
        private int ancho;
        private string codigo;
        private string descripcion;
        private int duracionExtendida;
        private int duracionResumida;
        private DateTime fechaCreacion;
        private DateTime fechaPrimerIngreso;
        private string nombreObra;
        private double peso;
        private double valuacion;

        public Obra(int alto, int ancho, string codigo, string descripcion, int duracionExtendida, int duracionResumida, DateTime fechaCreacion, DateTime fechaPrimerIngreso, string nombreObra, double peso, double valuacion)
        {
            this.alto = alto;
            this.ancho = ancho;
            this.codigo = codigo;
            this.descripcion = descripcion;
            this.DuracionExtendida = duracionExtendida;
            this.duracionResumida = duracionResumida;
            this.fechaCreacion = fechaCreacion;
            this.fechaPrimerIngreso = fechaPrimerIngreso;
            this.nombreObra = nombreObra;
            this.peso = peso;
            this.valuacion = valuacion;
        }

        public Obra()
        {

        }

        public int Alto { get => alto; set => alto = value; }
        public int Ancho { get => ancho; set => ancho = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        
        public int DuracionResumida { get => duracionResumida; set => duracionResumida = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public DateTime FechaPrimerIngreso { get => fechaPrimerIngreso; set => fechaPrimerIngreso = value; }
        public string NombreObra { get => nombreObra; set => nombreObra = value; }
        public double Peso { get => peso; set => peso = value; }
        public double Valuacion { get => valuacion; set => valuacion = value; }
        public int DuracionExtendida { get => duracionExtendida; set => duracionExtendida = value; }

        public int getDuracionExtendida()                                                               //DEVUELVE LA DURACION EXTENDIDA
        {
            return duracionExtendida;
        }



        public int getDuracionResumida()                                                               //DEVUELVE LA DURACION RESUMIDA
        {
            return duracionResumida;
        }

    }
}
