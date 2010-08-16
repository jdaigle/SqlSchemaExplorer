using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using SqlSchemaExplorer.Utility;

namespace SqlSchemaExplorer {
    public class IndexedColumnInfo {
        public static IndexedColumnInfo ScanIndexedColumn(IndexedColumn column) {
            var indexedColumnInfo = new IndexedColumnInfo();
            indexedColumnInfo.name = column.Name;
            return indexedColumnInfo;
        }

        private IndexedColumnInfo() { }

        private string name;

        public string Name { get { return name; } }

        public string ReadableName() {
            var singular = Inflector.Singularize(Name) ?? Name;
            return (Inflector.Pascalize(singular) ?? singular).Replace(" ", "");
        }
    }
}
