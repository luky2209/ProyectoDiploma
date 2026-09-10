using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALFamilia
    {
        DALAcceso55CA dal = new DALAcceso55CA();

        public int asignarPatenteAFamilia(int idPatente, int idFamilia)
        {
            string query = @"IF NOT EXISTS(SELECT * FROM Familia_Patente WHERE IdFamilia = @idFamilia AND IdPatente = @idPatente)
                     BEGIN
                         INSERT INTO Familia_Patente(IdFamilia, IdPatente) VALUES(@idFamilia, @idPatente)
                     END";
            var parametros = new Dictionary<string, object>
            {
                {"@idPatente", idPatente },
                {"@idFamilia", idFamilia }
            };

            return dal.executeNonQuery(query, parametros);
        }

        public DataTable obtenerRelacionesFamiliaPatente()
        {
            string query = "SELECT IdFamilia, IdPatente FROM Familia_Patente";

            return dal.executeDataTable(query);
        }
        public DataTable obtenerRelacionesFamiliaFamilia()
        {
            string query = "SELECT IdFamiliaPadre, IdFamiliaHija FROM Familia_Familia";

            return dal.executeDataTable(query);
        }
        public DataTable obtenerTodos()
        {
            string query = "SELECT * FROM Familia";

            return dal.executeDataTable(query);
        }

        public DataTable obtenerPorNombre(string nombre)
        {
            string query = "SELECT Id, Nombre FROM Familia WHERE Nombre = @nombre";

            var parametros = new Dictionary<string, object>
            {
                {"@nombre", nombre }
            };

            return dal.executeDataTable(query, parametros);
        }
        public int asignarFamiliaAFamilia(int idPadre, int idHija)
        {
            string query = "INSERT INTO Familia_Familia (IdFamiliaPadre, IdFamiliaHija) VALUES (@idPadre, @idHija)";

            var parametros = new Dictionary<string, object>
            {
                {"@idPadre", idPadre },
                {"@idHija", idHija }
            };

            int resultado = dal.executeNonQuery(query, parametros);

            return resultado;
        }

        public bool tieneDependencias(int idFamilia)
        {
            string query = @"
                        IF EXISTS (SELECT 1 FROM Rol_Familia WHERE IdFamilia = @id)
                           OR EXISTS (SELECT 1 FROM Familia_Familia WHERE IdFamiliaHija = @id)
                            SELECT 1;
                        ELSE
                            SELECT 0;";

            var parametros = new Dictionary<string, object>
            {
                {"@id", idFamilia }
            };

            int resultado = Convert.ToInt32(dal.executeScalar(query, parametros));

            return resultado == 1;
        }

        public int eliminarFamilia(int idFamilia)
        {
            string query = "DELETE FROM Familia WHERE Id = @id";

            var parametros = new Dictionary<string, object>
            {
                {"id", idFamilia }
            };

            int resultado = dal.executeNonQuery(query, parametros);

            return resultado;
        }

        public int insertarFamilia(string nombre)
        {
            string query = "INSERT INTO Familia (Nombre) VALUES (@nombre); SELECT SCOPE_IDENTITY();";

            var parametros = new Dictionary<string, object>
            {
                {"@nombre", nombre }
            };

            int idGenerado = Convert.ToInt32(dal.executeScalar(query, parametros));

            return idGenerado;
        }

        public void ActualizarDVH(int id, long dvh)
        {
            string query = "UPDATE Familia SET DVH = @dvh WHERE Id = @id";
            var parametros = new Dictionary<string, object>
            {
                { "@dvh", dvh },
                { "@id", id }
            };
            dal.executeNonQuery(query, parametros);
        }

    }
}
