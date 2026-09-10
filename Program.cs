using Services;
using Services.Instalador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string csActual = ServicioConfiguracionConexion.ObtenerConnectionStringGuardada();
            bool necesitaConfiguracion = string.IsNullOrWhiteSpace(csActual);

            if (!necesitaConfiguracion)
            {
                var (exito, _) = ServicioConfiguracionConexion.ProbarConexion(csActual);
                necesitaConfiguracion = !exito;
            }

            if (necesitaConfiguracion)
            {
                using (var formConfig = new Configuracion_Inicial())
                {
                    if (formConfig.ShowDialog() != DialogResult.OK)
                        return;
                }
            }

            Application.Run(new Login());
        }
    }
}
