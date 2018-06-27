using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.Domain
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteDataTable(string procedureName, string name, int value)
        {
            var parameter = new SqlParameter(name, SqlDbType.Int);
            parameter.Value = value;

            return ExecuteDataTable(procedureName, new [] { parameter });
        }

        public DataTable ExecuteDataTable(string procedureName)
        {
            return ExecuteDataTable(procedureName, Enumerable.Empty<SqlParameter>());
        }

        public DataTable ExecuteDataTable(string procedureName, IEnumerable<SqlParameter> parameters)
        {
            var table = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(procedureName, connection))
                {
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters.ToArray());
                        adapter.Fill(table);

                        return table;
                    }
                }
            }
        }

        public async Task<DataTable> ExecuteDataTableAsync(string procedureName, IEnumerable<SqlParameter> parameters)
        {
            var table = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    var time = System.DateTime.Now.ToString();
                    Debug.WriteLine($"Calling: {procedureName} {time}");

                    var reader = await cmd.ExecuteReaderAsync();
                    table.Load(reader);

                    return table;
                }
            }
        }
    }
}
