using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using System.Data;
using SqlSchemaExplorer.Utility;

namespace SqlSchemaExplorer {
    public class ColumnInfo {
        public static ColumnInfo ScanColumn(Column column) {
            var columnInfo = new ColumnInfo();

            columnInfo.name = column.Name;
            if (column.ExtendedProperties.Contains("MS_Description") &&
                column.ExtendedProperties["MS_Description"].Value != null)
                columnInfo.description = column.ExtendedProperties["MS_Description"].Value.ToString();

            columnInfo.isNullable = column.Nullable;
            columnInfo.sqlDataType = column.DataType.SqlDataType;
            columnInfo.maximumLength = column.DataType.MaximumLength;
            columnInfo.numericPrecision = column.DataType.NumericPrecision;
            columnInfo.numericScale = column.DataType.NumericScale;
            columnInfo.defaultValue = column.Default;
            columnInfo.isPrimaryKey = column.InPrimaryKey;
            columnInfo.isIdentity = column.Identity;
            columnInfo.isForeignKey = column.IsForeignKey;

            return columnInfo;
        }

        public ColumnInfo() {
            description = string.Empty;
            defaultValue = string.Empty;
        }

        private string name;
        private string description;
        private bool isNullable;
        private SqlDataType sqlDataType;
        private int maximumLength;
        private int numericPrecision;
        private int numericScale;
        private string defaultValue;
        private bool isPrimaryKey;
        private bool isIdentity;
        private bool isForeignKey;

        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public bool IsNullable { get { return isNullable; } }

        public int MaximumLength { get { return maximumLength; } }        
        public int NumericPrecision { get { return numericPrecision; } }
        public int NumericScale { get { return numericScale; } }

        public bool IsPrimaryKey { get { return isPrimaryKey; } }
        public bool IsIdentity { get { return isIdentity; } }
        public bool IsReadOnly { get { return isIdentity; } }
        public bool IsForeignKey { get { return isForeignKey; } }

        public string ReadableName() {
            var singular = Inflector.Singularize(Name) ?? Name;
            return (Inflector.Pascalize(singular) ?? singular).Replace(" ", "");
        }

        public bool IsNumeric() {
            switch (sqlDataType) {
                case SqlDataType.TinyInt:
                case SqlDataType.SmallInt:
                case SqlDataType.Int:
                case SqlDataType.BigInt:
                case SqlDataType.SmallMoney:
                case SqlDataType.Money:
                case SqlDataType.Decimal:
                case SqlDataType.Numeric:
                case SqlDataType.Real:
                case SqlDataType.Float:
                    return true;
                default: return false;
            }
        }

        public DbType GetDbType() {
            throw new NotImplementedException();
        }

        public Type GetDotNotType() {
            switch (sqlDataType) {
                case SqlDataType.Bit:
                    return typeof(Boolean);
                case SqlDataType.TinyInt:
                    return typeof(Int16);
                case SqlDataType.SmallInt:
                case SqlDataType.Int:
                    return typeof(Int32);
                case SqlDataType.BigInt:
                    return typeof(Int64);
                case SqlDataType.SmallMoney:
                case SqlDataType.Money:
                case SqlDataType.Decimal:
                case SqlDataType.Numeric:
                    return typeof(Decimal);
                case SqlDataType.Real:
                    return typeof(Single);
                case SqlDataType.Float:
                    return typeof(Double);
                case SqlDataType.Char:
                case SqlDataType.NChar:
                case SqlDataType.VarChar:
                case SqlDataType.NVarChar:
                case SqlDataType.Text:
                case SqlDataType.NText:
                case SqlDataType.VarCharMax:
                case SqlDataType.NVarCharMax:
                    return typeof(String);
                case SqlDataType.UniqueIdentifier:
                    return typeof(Guid);
                case SqlDataType.Date:
                case SqlDataType.SmallDateTime:
                case SqlDataType.DateTime:
                case SqlDataType.DateTime2:
                    return typeof(DateTime);
                case SqlDataType.DateTimeOffset:
                    return typeof(DateTimeOffset);
                case SqlDataType.Time:
                    return typeof(TimeZone);
                case SqlDataType.Binary:
                case SqlDataType.VarBinary:
                case SqlDataType.VarBinaryMax:
                case SqlDataType.Image:
                case SqlDataType.Timestamp:
                    return typeof(Byte[]);
                case SqlDataType.Variant:
                    return typeof(Object);
                default:
                    throw new InvalidOperationException("Type not supported: " + sqlDataType.ToString());
            }
        }
    }
}
