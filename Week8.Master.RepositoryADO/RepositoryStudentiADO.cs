using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week8.Master.Core.Entities;
using Week8.Master.Core.InterfaceRepositories;

namespace Week8.Master.RepositoryADO
{
    public class RepositoryStudentiADO : IRepositoryStudenti
    {
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CorsiMaster;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Studente Add(Studente item)
        {
            //facciamo questo così popoliamo
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into Studente values (@n,@c,@e,@t,@d,@cor)";

                command.Parameters.AddWithValue("@n", item.Nome);
                command.Parameters.AddWithValue("@c", item.Cognome);
                command.Parameters.AddWithValue("@e", item.Email);
                command.Parameters.AddWithValue("@t", item.TitoloStudio);
                command.Parameters.AddWithValue("@d", item.DataNascita);
                command.Parameters.AddWithValue("@cor", item.CorsoCodice);

                int numRighe = command.ExecuteNonQuery();
                if (numRighe == 1)
                {
                    connection.Close();
                    return item;
                }
                connection.Close();
                return null;
            }
        }

        public bool Delete(Studente item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "delete Studente where ID=@c";
                command.Parameters.AddWithValue("@c", item.Id);

                int numRighe = command.ExecuteNonQuery();
                if (numRighe == 1)
                {
                    connection.Close();
                    return true;
                }
                connection.Close();
                return false;
            }
        }

        public List<Studente> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Studente";
                SqlDataReader reader = command.ExecuteReader();
                List<Studente> studenti = new List<Studente>();

                while (reader.Read())
                {
                    var id = reader["ID"];
                    var nome = reader["Nome"];
                    var cognome = reader["Cognome"];
                    var email = reader["Email"];
                    var titolo = reader["TitoloStudio"];
                    var dataNascita = reader["DataNascita"];
                    var corsoCodice = reader["CorsoCodice"];

                    Studente nuovoStudente = new Studente((int)id, (string)nome, (string)cognome, (string)email, (string)titolo, (DateTime)dataNascita, (string)corsoCodice);
                    studenti.Add(nuovoStudente);
                }
                connection.Close();
                return studenti;
            }
        }

        public Studente GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Studente where ID=@id";
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                Studente nuovoStudente = new Studente();

                while (reader.Read())
                {
                    var Id = reader["ID"];
                    var nome = reader["Nome"];
                    var cognome = reader["Cognome"];
                    var email = reader["Email"];
                    var titolo = reader["TitoloStudio"];
                    var dataNascita = reader["DataNascita"];
                    var corsoCodice = reader["CorsoCodice"];

                    nuovoStudente.Id = (int)Id;
                    nuovoStudente.Nome = (string)nome;
                    nuovoStudente.Cognome = (string)cognome;
                    nuovoStudente.Email = (string)email;
                    nuovoStudente.TitoloStudio = (string)titolo;
                    nuovoStudente.DataNascita = (DateTime)dataNascita;
                    nuovoStudente.CorsoCodice = (string)corsoCodice;                   
                }
                if(nuovoStudente.Id != 0)
                {
                    connection.Close();
                    return nuovoStudente;
                }
                return null;             
            }
        }

        public Studente Update(Studente item)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "update Studente set Email=@e where ID=@i";

                command.Parameters.AddWithValue("@i", item.Id);
                command.Parameters.AddWithValue("@e", item.Email);

                int numRighe = command.ExecuteNonQuery();
                if (numRighe == 1)
                {
                    connection.Close();
                    return item;
                }
                connection.Close();
                return null;
            }

        }
    }
}
