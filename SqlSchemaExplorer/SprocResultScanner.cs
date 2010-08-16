using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace SqlSchemaExplorer {
    public class SprocResultScanner {

        private readonly SqlConnection connection;

        public SprocResultScanner(SqlConnection connection) {
            this.connection = connection;
        }

        public HashSet<SprocResultInfo> GetResults(StoredProcedure sproc) {
            using (transaction = connection.BeginTransaction()) {
                var reader = ExecuteSqlReader(BuildFMTQuery(sproc));
                var results = new HashSet<SprocResultInfo>();
                if (!reader.IsClosed) {
                    do {
                        var schemaTable = reader.GetSchemaTable();
                        if (schemaTable != null)
                            results.Add(SprocResultInfo.ScanSchema(schemaTable));
                    } while (reader.NextResult());
                    reader.Close();
                }
                transaction.Rollback();
                return results;
            }
        }

        private static string BuildFMTQuery(StoredProcedure sproc) {
            StringBuilder builder = new StringBuilder();
            builder.Append("SET FMTONLY ON\r\n");
            builder.AppendFormat("EXEC [{0}].[{1}]\r\n", sproc.Owner, sproc.Name);
            var @params = sproc.Parameters.Cast<StoredProcedureParameter>().Where(x => !x.IsOutputParameter);
            int count = 0;
            foreach (var param in @params) {
                builder.Append("\tNULL");
                if (count < @params.Count() - 1) {
                    builder.Append(",");
                }
                count++;
            }
            builder.Append("\r\n");
            builder.Append("SET FMTONLY OFF\r\n");
            return builder.ToString();
        }

        private SqlTransaction transaction;

        private SqlDataReader ExecuteSqlReader(string sql) {
            using (var command = new SqlCommand()) {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                command.CommandText = sql;
                command.Connection = this.connection;
                command.CommandType = CommandType.Text;
                if (transaction != null)
                    command.Transaction = transaction;
                CommandBehavior behavior = CommandBehavior.Default;
                return command.ExecuteReader(behavior);
            }
        }

    }
}
