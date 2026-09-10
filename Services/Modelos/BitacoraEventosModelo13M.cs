using BE.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class BitacoraEventosModelo13M
    {
        public int IdBitacora { get; set; }
        public string DNI { get; set; }
        public string Evento { get; set; }
        public int Criticidad { get; set; }
        public DateTime FechaHora { get; set; }
        public Modulos13M Modulo { get; set; }

        //ctor para bd
        public BitacoraEventosModelo13M(int idBitacora, string dNI, string evento, int criticidad, DateTime fechaHora, Modulos13M modulo)
        {
            IdBitacora = idBitacora;
            DNI = dNI;
            Evento = evento;
            Criticidad = criticidad;
            FechaHora = fechaHora;
            Modulo = modulo;
        }

        //ctor para hacer new
        public BitacoraEventosModelo13M(string dNI, string evento, int criticidad, DateTime fechaHora, Modulos13M modulo)
        {
            DNI = dNI;
            Evento = evento;
            Criticidad = criticidad;
            FechaHora = fechaHora;
            Modulo = modulo;
        }
    }
}
