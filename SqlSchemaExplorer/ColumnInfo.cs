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
            columnInfo.nativeType = column.DataType.SqlDataType.ToString();
            columnInfo.maximumLength = column.DataType.MaximumLength;
            columnInfo.numericPrecision = column.DataType.NumericPrecision;
            columnInfo.numericScale = column.DataType.NumericScale;
            columnInfo.defaultValue = column.Default;
            columnInfo.isPrimaryKey = column.InPrimaryKey;
            columnInfo.isIdentity = column.Identity;
            columnInfo.isForeignKey = column.IsForeignKey;

            return columnInfo;
        }

        protected ColumnInfo() {
            description = string.Empty;
            defaultValue = string.Empty;
        }

        protected string name;
        protected string description;
        protected bool isNullable;
        protected string nativeType;
        protected int maximumLength;
        protected int numericPrecision;
        protected int numericScale;
        protected string defaultValue;
        protected bool isPrimaryKey;
        protected bool isIdentity;
        protected bool isForeignKey;

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

        public bool HasLength() {
            switch (DbType) {
                case System.Data.DbType.AnsiString:
                case System.Data.DbType.AnsiStringFixedLength:
                case System.Data.DbType.Binary:
                case System.Data.DbType.String:
                case System.Data.DbType.StringFixedLength:
                case System.Data.DbType.VarNumeric:
                    return true;
                default: return false;
            }
        }

        public string GetNativeType() {
            string type = NativeType;
            if (HasLength())
                type += " (" + MaximumLength + ")";
            return type;
        }

        //public bool IsNumeric() {
        //    switch (sqlDataType) {
        //        case SqlDataType.TinyInt:
        //        case SqlDataType.SmallInt:
        //        case SqlDataType.Int:
        //        case SqlDataType.BigInt:
        //        case SqlDataType.SmallMoney:
        //        case SqlDataType.Money:
        //        case SqlDataType.Decimal:
        //        case SqlDataType.Numeric:
        //        case SqlDataType.Real:
        //        case SqlDataType.Float:
        //            return true;
        //        default: return false;
        //    }
        //}

        public string NativeType {
            get {
                return nativeType;
            }
        }

        public DbType DbType {
            get {
                return DatabaseTypes.GetDbType(NativeType);
            }
        }
    }
}
