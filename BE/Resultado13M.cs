using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Resultado13M
    {
        public int IdResultado { get; set; }
        public int NumeroInscripcion { get; set; }
        public string DNINadador { get; set; }
        public int IdPrueba { get; set; }
        public int Minutos { get; set; }
        public int Segundos { get; set; }
        public int Centesimas { get; set; }
        public bool Descalificado { get; set; }

        // un nadador descalificado no tiene puesto, por eso la posicion puede quedar vacia
        public int? Posicion { get; set; }
        public string Premio { get; set; }
        public long DVH { get; set; }

        // estos cinco son solo para mostrar en el dgv, no estan guardados asi en la tabla
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Categoria { get; set; }
        public string DescripcionPrueba { get; set; }

        // este es el estado de pago de la inscripcion, tambien es solo para mostrar
        public string Estado { get; set; }

        // el tiempo oficial como se muestra en el ranking, tipo 1'23"45
        public string TiempoOficial
        {
            get { return Minutos + "'" + Segundos.ToString("00") + "\"" + Centesimas.ToString("00"); }
        }

        // el tiempo pasado a centesimas, sirve para ordenar de menor a mayor
        public int TiempoEnCentesimas
        {
            get { return (Minutos * 60 + Segundos) * 100 + Centesimas; }
        }
    }
}
