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
        public IEnumerable<Usuario> ObtenerTodos()
        {

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Usuario>("SELECT * FROM Usuario").ToList();
            }
        }


        //poner usuario
        public Usuario ObtenerPorId(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuario WHERE IdUsuario = @Id", new { Id = id });
            }
        }



        //poner usuario
        public void Agregar(Usuario usuario)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Usuario (NombreUsuario, MailUsuario, IdUsuario, GoogleIdentificador) VALUES (@NombreUsuario, @MailUsuario, @IdUsuario, @GoogleIdentificador)";
                db.Execute(sql, usuario);
            }
        }

        
    }

}
