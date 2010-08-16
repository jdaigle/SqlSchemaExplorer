using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SqlSchemaExplorer.Utility;

namespace SqlSchemaExplorer {
    public class SprocResultInfo {
        public static SprocResultInfo ScanSchema(DataTable schemaTable) {
            var info = new SprocResultInfo();
            info.columns = new HashSet<SprocResultColumnInfo>();
            for (int i = 0; i < schemaTable.Rows.Count; i++) {
                info.columns.Add(SprocResultColumnInfo.ScanRow(schemaTable.Rows[i], i));
            }
            return info;
        }

        private SprocResultInfo() { }

        private HashSet<SprocResultColumnInfo> columns;

        public IEnumerable<SprocResultColumnInfo> Columns { get { return columns; } }

        public bool MatchesColumns(IColumnBag thingWithColumns) {
            // every column in the column bag must be present in our columns
            foreach (var column in thingWithColumns.Columns) {
                // Look for any column that is not in our result set
                if (!columns.Any(x => x.Matches(column)))
                    return false;
            }
            // every column in our result set should also be in the column bag
            foreach (var column in columns) {
                // Look for any column that is not in our result set
                if (!thingWithColumns.Columns.Any(x => column.Matches(x)))
                    return false;
            }
            return true;
        }       
    }
}
