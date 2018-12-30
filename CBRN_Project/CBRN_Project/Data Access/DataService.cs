using System.Data;
using System.Data.SqlClient;

namespace CBRN_Project.Data_Access
{
    class DataService
    {
        private const string connectionString = "Server=localhost;Database=CBRN;Trusted_Connection=True;";

        public DataTable GetTable(string tableName)
        {
            DataTable table = new DataTable();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "select * from " + tableName;
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(table);
            dataAdapter.Dispose();

            connection.Close();
            return table;
        }
    }
}
