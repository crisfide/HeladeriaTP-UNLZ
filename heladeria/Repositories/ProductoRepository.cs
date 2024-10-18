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
    public class ProductoRepository
    {
        private string connectionString;

        public ProductoRepository(string constr)
        {
            connectionString = constr;
                //ConfigurationManager.ConnectionStrings["MiCadenaDeConexion"].ConnectionString;
        }



        public IEnumerable<Producto> ObtenerTodos()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Producto>("SELECT * FROM Grupo6.Helado").ToList();
            }
        }

        public Producto ObtenerPorId(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QueryFirstOrDefault<Producto>("SELECT * FROM Grupo6.Helado WHERE Id = @Id", new { Id = id });
            }
        }

        public void Agregar(Producto producto)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Grupo6.Helado (Descripcion, Kilos, IdUsuarioAlta, FechaAlta) VALUES (@Descripcion, @Kilos, @IdUsuarioAlta, @FechaAlta)";
                db.Execute(sql, producto);
            }
        }

        public void Actualizar(Producto producto)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var sql = "UPDATE Productos SET Nombre = @Nombre, Precio = @Precio, Cantidad = @Cantidad WHERE Id = @Id";
                db.Execute(sql, producto);
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var sql = "DELETE FROM Productos WHERE Id = @Id";
                db.Execute(sql, new { Id = id });
            }
        }
    }

}
