using heladeria.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace heladeria.Repositories
{
    public class UsuarioRepository
    {
        private string connectionString;

        public UsuarioRepository(string constr)
        {
            connectionString = constr;
                //ConfigurationManager.ConnectionStrings["MiCadenaDeConexion"].ConnectionString;
        }


        //poner usuario
        public IEnumerable<Producto> ObtenerTodos()
        {

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Producto>("SELECT * FROM Grupo6.Helado").ToList();
            }
        }


        //poner usuario
        public Producto ObtenerPorId(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QueryFirstOrDefault<Producto>("SELECT * FROM Grupo6.Helado WHERE IdHelado = @Id", new { Id = id });
            }
        }



        //poner usuario
        public void Agregar(Producto producto)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Grupo6.Helado (Descripcion, Kilos, IdUsuarioAlta, FechaAlta) VALUES (@Descripcion, @Kilos, @IdUsuarioAlta, @FechaAlta)";
                db.Execute(sql, producto);
            }
        }

        
    }

}
