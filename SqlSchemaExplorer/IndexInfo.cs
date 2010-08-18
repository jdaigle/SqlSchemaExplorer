using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using SqlSchemaExplorer.Utility;

namespace SqlSchemaExplorer {
    public class IndexInfo {
        public static IndexInfo ScanIndex(Index index, IEnumerable<ColumnInfo> tableColumns) {
            var indexInfo = new IndexInfo();

            indexInfo.name = index.Name;
            if (index.ExtendedProperties.Contains("MS_Description") &&
                index.ExtendedProperties["MS_Description"].Value != null)
                indexInfo.description = index.ExtendedProperties["MS_Description"].Value.ToString();

            indexInfo.columns = new HashSet<ColumnInfo>();
            foreach (var column in index.IndexedColumns.Cast<IndexedColumn>()) {
                if (column.IsIncluded)
                    continue;
                indexInfo.columns.Add(tableColumns.Single(x => x.Name == column.Name));
            }

            return indexInfo;
        }

        private IndexInfo() {
            description = string.Empty;
        }

        private string name;
        private string description;
        private HashSet<ColumnInfo> columns;

        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public IEnumerable<ColumnInfo> IndexedColumns { get { return columns; } }

        public string ReadableName() {
            var singular = Inflector.Singularize(Name) ?? Name;
            return (Inflector.Pascalize(singular) ?? singular).Replace(" ", "");
        }
    }
}
