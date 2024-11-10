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
        public IEnumerable<Pedido> ObtenerTodos()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Pedido>("SELECT * FROM Pedido").ToList();
            }
        }

        //pedido
        public IEnumerable<Pedido> ObtenerPropios(int idUsuario)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Pedido>("SELECT * FROM Grupo6.Pedido WHERE IdUsuarioAlta = @Id", new { Id = idUsuario }).ToList();
            }
        }

        //PEDIDO
        public Pedido ObtenerPorId(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QueryFirstOrDefault<Pedido>("SELECT * FROM Pedido WHERE IdPedido = @Id", new { Id = id });
            }
        }


        //PEDIDO
        public void Agregar(Pedido pedido)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Pedido (IdHelado, Kilos, IdUsuarioAlta) VALUES (@IdHelado, @Kilos, @IdUsuarioAlta)";
                db.Execute(sql, pedido);
            }
        }

        
    }

}
