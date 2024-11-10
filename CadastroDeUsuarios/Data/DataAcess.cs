using CadastroDeUsuarios.Models;
using System.Data;
using System.Data.SqlClient;

namespace CadastroDeUsuarios.Data
{
    public class DataAcess
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString("DefaultConnection");

        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_usuarios]";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Sobrenome = reader["Sobrenome"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Cargo = reader["Cargo"].ToString();

                    usuarios.Add(usuario);
                }

                _connection.Close();
            }
            
            return usuarios;


        }

        public bool Cadastrar(Usuario usuario)
        {
            int id = 0;

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType= CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[inserir_usuario]";

                _command.Parameters.AddWithValue("@Nome", usuario.Nome);
                _command.Parameters.AddWithValue("@Sobrenome", usuario.Sobrenome);
                _command.Parameters.AddWithValue("@Email", usuario.Email);
                _command.Parameters.AddWithValue("@Cargo", usuario.Cargo);

                _connection.Open();

                id = _command.ExecuteNonQuery();

                _connection.Close();
            }
            
            return id > 0 ? true : false;
        }

        public Usuario BuscarUsuarioPorId(int id)
        {
            Usuario usuario = new Usuario();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_usuario_id]";
                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Sobrenome = reader["Sobrenome"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Cargo = reader["Cargo"].ToString();
                }

                _connection.Close();
            }

            return usuario;
        }

        public bool Editar(Usuario usuario)
        {
            var id = 0;

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType= CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[editar_usuario]";

                _command.Parameters.AddWithValue("@Id", usuario.Id);
                _command.Parameters.AddWithValue("@Nome", usuario.Nome);
                _command.Parameters.AddWithValue("@Sobrenome", usuario.Sobrenome);
                _command.Parameters.AddWithValue("@Email", usuario.Email);
                _command.Parameters.AddWithValue("@Cargo", usuario.Cargo);

                _connection.Open();

                id = _command.ExecuteNonQuery();

                _connection.Close();
            }

            return id > 0 ? true : false;
        }

        public bool Remover (int id)
        {
            var result = 0;

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[remover_usuario]";

                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();

                result = _command.ExecuteNonQuery();

                _connection.Close();
            }

            return result > 0 ? true : false;
        }
    }
}
