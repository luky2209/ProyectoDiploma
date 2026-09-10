using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALNadador13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        public DataTable obtenerTodos()
        {
            string query = @"SELECT DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH
                             FROM Nadador ORDER BY Apellido, Nombre";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        public DataRow obtenerPorDNI(string dni)
        {
            string query = @"SELECT DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH
                             FROM Nadador WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public int InsertarNadador(string dni, string nombre, string apellido, DateTime fechaNacimiento, int edad, string categoria, bool certificadoMedico, long dvh)
        {
            string query = @"INSERT INTO Nadador (DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH)
                             VALUES (@dni, @nombre, @apellido, @fechaNacimiento, @edad, @categoria, @certificado, @dvh)";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@nombre", nombre },
                { "@apellido", apellido },
                { "@fechaNacimiento", fechaNacimiento },
                { "@edad", edad },
                { "@categoria", categoria },
                { "@certificado", certificadoMedico },
                { "@dvh", dvh }
            };

            return acceso.executeNonQuery(query, parametros);
        }

        public void ModificarNadador(string dni, string nombre, string apellido, DateTime fechaNacimiento, int edad, string categoria, bool certificadoMedico)
        {
            string query = @"UPDATE Nadador
                             SET Nombre = @nombre,
                                 Apellido = @apellido,
                                 FechaNacimiento = @fechaNacimiento,
                                 Edad = @edad,
                                 Categoria = @categoria,
                                 CertificadoMedico = @certificado
                             WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@nombre", nombre },
                { "@apellido", apellido },
                { "@fechaNacimiento", fechaNacimiento },
                { "@edad", edad },
                { "@categoria", categoria },
                { "@certificado", certificadoMedico }
            };

            acceso.executeNonQuery(query, parametros);
        }

        public void EliminarNadador(string dni)
        {
            string query = "DELETE FROM Nadador WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni }
            };

            acceso.executeNonQuery(query, parametros);
        }

        public void ActualizarDVH(string dni, long dvh)
        {
            string query = @"UPDATE Nadador
                             SET DVH = @dvh
                             WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}