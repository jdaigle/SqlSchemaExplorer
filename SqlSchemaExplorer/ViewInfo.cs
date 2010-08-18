using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using SqlSchemaExplorer.Utility;

namespace SqlSchemaExplorer {
    public class ViewInfo : IColumnBag {
        public static ViewInfo ScanView(View view) {
            var viewInfo = new ViewInfo();

            viewInfo.name = view.Name;
            if (view.ExtendedProperties.Contains("MS_Description") &&
                view.ExtendedProperties["MS_Description"].Value != null)
                viewInfo.description = view.ExtendedProperties["MS_Description"].Value.ToString();

            viewInfo.columns = new HashSet<ColumnInfo>();
            foreach (var column in view.Columns.Cast<Column>()) {
                viewInfo.columns.Add(ColumnInfo.ScanColumn(column));
            }

            viewInfo.indexes = new HashSet<IndexInfo>();
            foreach (var index in view.Indexes.Cast<Index>()) {
                if (index.IsSystemObject)
                    continue;
                var indexInfo = IndexInfo.ScanIndex(index, viewInfo.columns);
                viewInfo.indexes.Add(indexInfo);
                if (index.IndexKeyType == IndexKeyType.DriPrimaryKey)
                    viewInfo.primaryKey = indexInfo;
            }

            return viewInfo;
        }

        private ViewInfo() {
            description = string.Empty;
        }

        private string name;
        private string description;

        private HashSet<IndexInfo> indexes;
        private IndexInfo primaryKey;

        private HashSet<ColumnInfo> columns;

        public TableOrView TableOrView { get { return TableOrView.View; } }

        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public IEnumerable<IndexInfo> Indexes { get { return indexes; } }
        public IEnumerable<ColumnInfo> Columns { get { return columns; } }

        public string ReadableName() {
            var singular = Inflector.Singularize(Name) ?? Name;
            return (Inflector.Pascalize(singular) ?? singular).Replace(" ", "");
        }
    }
}
