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
            var tables = databaseInfo.Tables.ToArray();

            foreach (var item in tables) {
                foreach (var col in item.Columns) {
                    if (col.DbType == System.Data.DbType.Object)
                        Console.Out.Write("Asf");
                    if (col.MaximumLength == 0)
                        Console.Out.Write("Asf");
                }
            }

            
            var views = databaseInfo.Views.ToArray();
            var columns = tables[2].Columns.ToArray();
            var indexes = tables[2].Indexes.ToArray();

            string mergedColumnNames = string.Empty;
            foreach (var item in indexes[0].IndexedColumns) {
                mergedColumnNames += item.ReadableName();
            }

            var foreignKeys = tables[2].ForeignKeys.ToArray();

            var tableNames = databaseInfo.Tables.Select(x => x.ReadableName()).ToArray();
            var viewNames = databaseInfo.Views.Select(x => x.ReadableName()).ToArray();

            var thingsWithColumns = tables.Cast<IColumnBag>().Union(views.Cast<IColumnBag>());

            foreach (var sproc in databaseInfo.Sprocs) {
                var @in = sproc.InParameters.ToArray();
                var @out = sproc.OutParameters.ToArray();
                var results = sproc.Results.ToArray();
                foreach (var result in results) {
                    if (thingsWithColumns.Any(x => result.MatchesColumns(x))) {
                        var thingWithColumns = thingsWithColumns.Single(x => result.MatchesColumns(x));
                        var match = string.Format("Sproc {2}: Found a matching {0} called {1}", thingWithColumns.TableOrView.ToString(), thingWithColumns.Name, sproc.Name);
                    }
                }
            }
        }
    }
}
