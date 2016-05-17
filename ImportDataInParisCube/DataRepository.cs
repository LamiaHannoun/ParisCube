using System.Collections.Generic;
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

        public void ImportData(IEnumerable<ZoneTemp> zones)
        {
           
        }
    }
}
