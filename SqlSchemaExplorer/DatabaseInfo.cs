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
        private HashSet<ViewInfo> views;
        private HashSet<SprocInfo> sprocs;

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

            views = new HashSet<ViewInfo>();
            foreach (var view in database.Views.Cast<View>()) {
                if (view.IsSystemObject)
                    continue;
                views.Add(ViewInfo.ScanView(view));
            }

            sprocs = new HashSet<SprocInfo>();
            foreach (var sproc in database.StoredProcedures.Cast<StoredProcedure>()) {
                if (sproc.IsSystemObject)
                    continue;
                sprocs.Add(SprocInfo.ScanSproc(sproc, new SprocResultScanner(database.Parent.ConnectionContext.SqlConnectionObject)));
            }


            doneScanning = true;
        }

        private void EnsureDoneScanning() {
            if (!doneScanning)
                Scan();
        }

        public IEnumerable<TableInfo> Tables {
            get {
                EnsureDoneScanning();
                return tables;
            }
        }

        public IEnumerable<ViewInfo> Views {
            get {
                EnsureDoneScanning();
                return views;
            }
        }

        public IEnumerable<SprocInfo> Sprocs {
            get {
                EnsureDoneScanning();
                return sprocs;
            }
        }
    }
}
