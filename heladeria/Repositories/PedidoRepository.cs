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
    public class PedidoRepository
    {
        private string connectionString;

        public PedidoRepository(string constr)
        {
            connectionString = constr;
                //ConfigurationManager.ConnectionStrings["MiCadenaDeConexion"].ConnectionString;
        }


        //PEDIDO
        public IEnumerable<Producto> ObtenerTodos()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Producto>("SELECT * FROM Grupo6.Helado").ToList();
            }
        }

        //pedido
        public IEnumerable<Producto> ObtenerPropios(Usuario usuario)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Producto>("SELECT * FROM Grupo6.Pedido WHERE").ToList();
            }
        }

        //PEDIDO
        public Producto ObtenerPorId(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QueryFirstOrDefault<Producto>("SELECT * FROM Grupo6.Helado WHERE IdHelado = @Id", new { Id = id });
            }
        }


        //PEDIDO
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
