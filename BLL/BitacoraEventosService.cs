using DAL;
using BE.Enum;
using Services.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BitacoraEventosService
    {
        DALBitacora13M dal = new DALBitacora13M();
        public void registrarEvento(string dni, string evento, Criticidad13M criticidad, Modulos13M modulo)
        { 
            dal.insertarLog(dni, evento, (int)criticidad, (int)modulo, DateTime.Now);
        }


        public DataTable obtenerUltimos3Dias()
        {
            DateTime desde = DateTime.Now.AddDays(-3);
            DateTime hasta = DateTime.Now;

            return dal.obtenerBitacora(desde, hasta);
        }

        public DataTable obtenerBitacora(DateTime desde, DateTime hasta, int? moduloFiltro = null)
        {
            return dal.obtenerBitacora(desde, hasta, moduloFiltro);
        }
    }
}
