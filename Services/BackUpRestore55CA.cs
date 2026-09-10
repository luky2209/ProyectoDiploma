using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BackUpRestore55CA
    {
        DALBackUpRestore55CA _dal = new DALBackUpRestore55CA();
        
        public void realizarBackUp(string ruta)
        {
            if (!Directory.Exists(ruta))
            {
                throw new Exception("El directorio seleccionado no existe.");
            }

            string nombreArchivo = $"Backup_Sistema_{DateTime.Now:yyyyMMdd_HHmm}.bak";
            string rutaCompleta = Path.Combine(ruta, nombreArchivo);

            _dal.realizarBackUp(rutaCompleta);
        }

        public void realizarRestore(string ruta)
        {
            if (!File.Exists(ruta))
            {
                throw new Exception("El archivo de backup seleccionado no existe o fue movido.");
            }
                

            if (Path.GetExtension(ruta).ToLower() != ".bak")
            {
                throw new Exception("El archivo seleccionado no tiene un formato válido de backup (.bak).");
            }
                

            _dal.realizarRestore(ruta);
        }
    }
}
