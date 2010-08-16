using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SqlSchemaExplorer.Utility;

namespace SqlSchemaExplorer {
    public class SprocResultColumnInfo {

        public static SprocResultColumnInfo ScanRow(DataRow row, int index) {
            var info = new SprocResultColumnInfo();
            info.name = row.IsNull("ColumnName") ? ("Column" + (index + 1)) : ((string)row["ColumnName"]);
            if (string.IsNullOrEmpty(info.name)) {
                info.name = "Column" + (index + 1);
            }
            info.sqlDbType = row.IsNull("ProviderType") ? SqlDbType.Variant : ((SqlDbType)row["ProviderType"]);
            info.size = row.IsNull("ColumnSize") ? 0 : ((int)row["ColumnSize"]);
            info.precision = row.IsNull("NumericPrecision") ? ((byte)0) : Convert.ToByte((short)row["NumericPrecision"]);
            info.scale = row.IsNull("NumericScale") ? 0 : Convert.ToInt32((short)row["NumericScale"]);
            info.allowDBNull = !row.IsNull("AllowDBNull") && ((bool)row["AllowDBNull"]);
            return info;
        }

        private SprocResultColumnInfo() { }

        private string name;
        private SqlDbType sqlDbType;
        private int size;
        private byte precision;
        private int scale;
        private bool allowDBNull;

        public string Name { get { return name; } }
        public SqlDbType SqlDbType { get { return sqlDbType; } }
        public DbType DbType { get { return DatabaseTypes.GetDbType(sqlDbType); } }
        public int Size { get { return size; } }
        public byte Precision { get { return precision; } }
        public int Scale { get { return scale; } }
        public bool AllowDBNull { get { return allowDBNull; } }

        public bool Matches(ColumnInfo column) {
            if (column.Name.ToUpper() != name.ToUpper())
                return false;
            if (column.DbType != DbType)
                return false;
            return true;
        }
    }
}
