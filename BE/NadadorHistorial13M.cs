using System;

namespace BE
{
    public class NadadorHistorial13M
    {
        public long IdHistorial { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Categoria { get; set; }
        public bool CertificadoMedico { get; set; }
        public long? DVH { get; set; }
        public int VersionNumero { get; set; }
        public DateTime FechaHora { get; set; }
        public string TipoOperacion { get; set; }
        public string UsuarioDNI { get; set; }
        public bool Activo { get; set; }
        public long? OrigenVersionId { get; set; }
    }
}
