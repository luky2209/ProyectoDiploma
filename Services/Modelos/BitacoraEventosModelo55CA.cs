using BE.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class BitacoraEventosModelo55CA
    {
        public int IdBitacora { get; set; }
        public string DNI { get; set; }
        public string Evento { get; set; }
        public int Criticidad { get; set; }
        public DateTime FechaHora { get; set; }
        public Modulos55CA Modulo { get; set; }

        //ctor para bd
        public BitacoraEventosModelo55CA(int idBitacora, string dNI, string evento, int criticidad, DateTime fechaHora, Modulos55CA modulo)
        {
            IdBitacora = idBitacora;
            DNI = dNI;
            Evento = evento;
            Criticidad = criticidad;
            FechaHora = fechaHora;
            Modulo = modulo;
        }

        //ctor para hacer new
        public BitacoraEventosModelo55CA(string dNI, string evento, int criticidad, DateTime fechaHora, Modulos55CA modulo)
        {
            DNI = dNI;
            Evento = evento;
            Criticidad = criticidad;
            FechaHora = fechaHora;
            Modulo = modulo;
        }
    }
}
