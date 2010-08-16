using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SqlSchemaExplorer.Runner {
    class Program {
        static void Main(string[] args) {
            var connectionString = @"Server=.\SQLEXPRESS;Database=Northwind;Trusted_Connection=True;";

            var databaseInfo = DatabaseInfo.Load(new SqlConnection(connectionString));
            var tables = databaseInfo.TableInfos.ToArray();
            var columns = tables[2].Columns.ToArray();
            var indexes = tables[2].Indexes.ToArray();
            var foreignKeys = tables[2].ForeignKeys.ToArray();
        }
    }
}
