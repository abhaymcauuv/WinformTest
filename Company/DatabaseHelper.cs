using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Company
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CompanyDb"].ConnectionString;

        public static async Task<DataTable> ExecuteQueryAsync(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    await connection.OpenAsync();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => adapter.Fill(dataTable)); // Run on a separate thread
                        return dataTable;
                    }
                }
            }
        }

        public static async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
