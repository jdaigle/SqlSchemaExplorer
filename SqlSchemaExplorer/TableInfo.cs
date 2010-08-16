using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using SqlSchemaExplorer.Utility;

namespace SqlSchemaExplorer {
    public class TableInfo {

        public static TableInfo ScanTable(Table table) {
            var tableInfo = new TableInfo();

            tableInfo.name = table.Name;
            if (table.ExtendedProperties.Contains("MS_Description") &&
                table.ExtendedProperties["MS_Description"].Value != null)
                tableInfo.description = table.ExtendedProperties["MS_Description"].Value.ToString();

            tableInfo.indexes = new HashSet<IndexInfo>();
            foreach (var index in table.Indexes.Cast<Index>()) {
                if (index.IsSystemObject)
                    continue;
                var indexInfo = IndexInfo.ScanIndex(index);
                tableInfo.indexes.Add(indexInfo);
                if (index.IndexKeyType == IndexKeyType.DriPrimaryKey)
                    tableInfo.primaryKey = indexInfo;
            }

            tableInfo.columns = new HashSet<ColumnInfo>();
            foreach (var column in table.Columns.Cast<Column>()) {
                tableInfo.columns.Add(ColumnInfo.ScanColumn(column));
            }

            return tableInfo;
        }

        private TableInfo() {
            description = string.Empty;
        }

        private string name;
        private string description;

        private HashSet<IndexInfo> indexes;
        private IndexInfo primaryKey;

        private HashSet<ColumnInfo> columns;

        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public IEnumerable<IndexInfo> Indexes { get { return indexes; } }
        public IndexInfo PrimaryKey { get { return primaryKey; } }

        public IEnumerable<ColumnInfo> Columns { get { return columns; } }
        public IEnumerable<ColumnInfo> ForeignKeys { get { return columns.Where(x => x.IsForeignKey); } }

        public string ReadableName() {
            var singular = Inflector.Singularize(Name) ?? Name;
            return (Inflector.Pascalize(singular) ?? singular).Replace(" ", "");
        }
    }
}
