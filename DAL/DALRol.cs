using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALRol
    {
        DALAcceso55CA _dal = new DALAcceso55CA();

        public DataTable obtenerTodos()
        {
            string query = "SELECT * FROM Rol";

            DataTable dt = _dal.executeDataTable(query);

            return dt;
        }

        public DataTable obtenerPorNombre(string nombre)
        {
            string query = "SELECT Id, Nombre FROM Rol WHERE Nombre = @nombre";

            var parametros = new Dictionary<string, object>
            {
                {"@nombre", nombre }
            };

            return _dal.executeDataTable(query, parametros);
        }

        public int asignarFamiliaARol(int idFamilia, int idRol)
        {
            string query = "INSERT INTO Rol_Familia (IdRol, IdFamilia) VALUES (@idRol, @idFamilia)";

            var parametros = new Dictionary<string, object>
            {
                {"@idRol", idRol },
                {"@idFamilia", idFamilia }
            };

            int resultado = _dal.executeNonQuery(query, parametros);

            return resultado;
        }

        public int asignarPatenteARol(int idPatente, int idRol)
        {
            string query = "INSERT INTO Rol_Patente (IdRol, IdPatente) VALUES (@idRol, @idPatente)";

            var parametros = new Dictionary<string, object>
            {
                {"@idRol", idRol },
                {"@idPatente", idPatente }
            };

            int resultado = _dal.executeNonQuery(query, parametros);

            return resultado;
        }

        public int insertarRol(string nombre)
        {
            string query = "INSERT INTO Rol (Nombre) VALUES (@nombre); SELECT SCOPE_IDENTITY();";

            var parametros = new Dictionary<string, object>
            {
                {"@nombre", nombre }
            };

            int idGenerado = Convert.ToInt32(_dal.executeScalar(query, parametros));

            return idGenerado;
        }

        public void eliminarRol(int idRol)
        {
            string query = "DELETE FROM Rol WHERE Id = @idRol";

            var parametros = new Dictionary<string, object>
            {
                { "@idRol", idRol }
            };

            _dal.executeNonQuery(query, parametros);
        }

        public bool tieneUsuariosAsignados(int idRol)
        {
            string query = "SELECT COUNT(*) FROM Usuario WHERE IdRol = @idRol";

            var parametros = new Dictionary<string, object>
            {
                { "@idRol", idRol }
            };

            // Usas executeScalar para traer el conteo
            int cantidad = Convert.ToInt32(_dal.executeScalar(query, parametros));

            return cantidad > 0;
        }

        public int quitarPatenteDeRol(int idRol, int idPatente)
        {
            string query = "DELETE FROM Rol_Patente WHERE IdRol = @idRol AND IdPatente = @idPatente";
            var parametros = new Dictionary<string, object>
            {
                { "@idRol", idRol },
                { "@idPatente", idPatente }
            };
            return _dal.executeNonQuery(query, parametros);
        }

        public int quitarFamiliaDeRol(int idRol, int idFamilia)
        {
            string query = "DELETE FROM Rol_Familia WHERE IdRol = @idRol AND IdFamilia = @idFamilia";
            var parametros = new Dictionary<string, object>
            {
                { "@idRol", idRol },
                { "@idFamilia", idFamilia }
            };
            return _dal.executeNonQuery(query, parametros);
        }

        public DataTable obtenerRelacionesRolPatente()
        {
            string query = "SELECT IdRol, IdPatente FROM Rol_Patente";

            return _dal.executeDataTable(query);
        }
        public DataTable obtenerRelacionesRolFamilia()
        {
            string query = "SELECT IdRol, IdFamilia FROM Rol_Familia";

            return _dal.executeDataTable(query);
        }

        public void ActualizarDVH(int id, long dvh)
        {
            string query = "UPDATE Rol SET DVH = @dvh WHERE Id = @id";
            var parametros = new Dictionary<string, object>
             {
                { "@dvh", dvh },
                { "@id", id }
             };
            _dal.executeNonQuery(query, parametros);
        }
    }
}
