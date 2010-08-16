using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;

namespace SqlSchemaExplorer {
    public class DatabaseInfo {

        public static DatabaseInfo Load(SqlConnection connection) {
            return new DatabaseInfo(connection);
        }

        private readonly Database database;
        private bool doneScanning;
        private HashSet<TableInfo> tables;

        public DatabaseInfo(SqlConnection connection) {
            var serverConnection = new ServerConnection(connection);
            var server = new Server(serverConnection);
            database = server.Databases[connection.Database];
        }

        public void Scan() {
            tables = new HashSet<TableInfo>();
            foreach (var table in database.Tables.Cast<Table>()) {
                if (table.IsSystemObject)
                    continue;
                tables.Add(TableInfo.ScanTable(table));
            }
            doneScanning = true;
        }

        private void EnsureDoneScanning() {
            if (!doneScanning)
                Scan();
        }

        public IEnumerable<TableInfo> TableInfos {
            get {
                EnsureDoneScanning();
                return tables;
            }
        }
    }
}
