using Services.Modelos.Idioma;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Services
{
    public class IdiomaManager
    {
        private List<IIdiomaObserver> _observers = new List<IIdiomaObserver>();
        private Dictionary<string, string> traducciones;

        public void Suscribir(IIdiomaObserver observer)
        {
            _observers.Add(observer);
        }

        public void Desuscribir(IIdiomaObserver observer)
        {
            _observers.Remove(observer);
        }

        private void Notificar()
        {
            foreach(var observer in _observers)
            {
                observer.actualizarIdioma();
            }
        }

        public void CargarIdioma(string codIdioma)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lenguajes", $"{codIdioma}.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"No se encontro el idioma '{codIdioma}'",path);
            }
                
            string json = File.ReadAllText(path);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            traducciones = serializer.Deserialize<Dictionary<string, string>>(json);

            Notificar();
        }

        public string Translate(string key)
        {
            return traducciones.TryGetValue(key, out string value) ? value : key; 
        }
    }
}
