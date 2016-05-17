using System.Data.SqlClient;
using System.Configuration;

namespace ImportDataInParisCube
{
    public class DataRepository
    {
        string connectionString { get; }
        public DataRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        }

        public void ImportData()
        {
            var connectionString = string.Empty;
            var commandText = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandText, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
