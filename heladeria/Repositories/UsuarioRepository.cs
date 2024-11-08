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


        public IEnumerable<Usuario> ObtenerTodos()
        {

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Usuario>("SELECT * FROM Usuario").ToList();
            }
        }


        public Usuario ObtenerPorId(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuario WHERE IdUsuario = @Id", new { Id = id });
            }
        }

        public Usuario ObtenerPorGoogle(string GId)
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuario WHERE GoogleIdentificador = @GoogleIdentificador", new { GoogleIdentificador = GId });
            }
        }



        public void Agregar(Usuario usuario)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Usuario (NombreUsuario, MailUsuario, GoogleIdentificador) VALUES (@NombreUsuario, @MailUsuario, @GoogleIdentificador)";
                db.Execute(sql, usuario);
            }
        }

        
    }

}
