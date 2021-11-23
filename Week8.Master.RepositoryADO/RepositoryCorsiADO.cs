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
    public class RepositoryCorsiADO : IRepositoryCorsi //mi dice di mettere la referenza e di implementare l'interfaccia
                                                       //questo ora si collega al db
                                                       //questi metodi li deve fare per il db
    {
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CorsiMaster;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Corso Add(Corso item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into Corso values (@c,@n,@d)";

                command.Parameters.AddWithValue("@c", item.CorsoCodice);
                command.Parameters.AddWithValue("@n", item.Nome);
                command.Parameters.AddWithValue("@d", item.Descrizione);

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

        public bool Delete(Corso item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "delete Corso where CorsoCodice=@c";

                command.Parameters.AddWithValue("@c", item.CorsoCodice);
                
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

        public List<Corso> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Corso";
                SqlDataReader reader = command.ExecuteReader();
                List<Corso> corsi = new List<Corso>();

                while (reader.Read())
                {
                    var corsoCodice = reader["CorsoCodice"];
                    var nome = reader["Nome"];
                    var descrizione = reader["Descrizione"];

                    Corso nuovoCorso = new Corso()
                    {
                        CorsoCodice = (string)corsoCodice,
                        Nome = (string)nome,
                        Descrizione = (string)descrizione
                    };
                    corsi.Add(nuovoCorso);
                }
                connection.Close();
                return corsi;
            }
        }

        public Corso GetByCode(string codice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Corso";
                SqlDataReader reader = command.ExecuteReader();

                Corso nuovoCorso = new Corso();

                while (reader.Read())
                {
                    var corsoCodice = reader["CorsoCodice"];
                    var nome = reader["Nome"];
                    var descrizione = reader["Descrizione"];

                    if ((string)corsoCodice == codice)
                    {
                        nuovoCorso.CorsoCodice = (string)corsoCodice;
                        nuovoCorso.Nome = (string)nome;
                        nuovoCorso.Descrizione = (string)descrizione;

                        connection.Close();
                        return nuovoCorso;
                    }                   
                }
                connection.Close();
                return null;
            }
        }

        public Corso Update(Corso item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "update Corso set Nome=@n, Descrizione=@d where CorsoCodice=@c";

                command.Parameters.AddWithValue("@c", item.CorsoCodice);
                command.Parameters.AddWithValue("@n", item.Nome);
                command.Parameters.AddWithValue("@d", item.Descrizione);

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
