using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.SqlServer.Management.Smo;

namespace SqlSchemaExplorer.Utility {
    public static class DatabaseTypes {

        public static Type GetClrType(SqlDbType sqlType) {
            switch (sqlType) {
                case SqlDbType.BigInt:
                    return typeof(long?);

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);

                case SqlDbType.Bit:
                    return typeof(bool?);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(string);

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime?);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal?);

                case SqlDbType.Float:
                    return typeof(double?);

                case SqlDbType.Int:
                    return typeof(int?);

                case SqlDbType.Real:
                    return typeof(float?);

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid?);

                case SqlDbType.SmallInt:
                    return typeof(short?);

                case SqlDbType.TinyInt:
                    return typeof(byte?);

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object);

                case SqlDbType.Structured:
                    return typeof(DataTable);

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset?);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }

        public static Type GetDotNotType(SqlDataType sqlDataType) {
            switch (sqlDataType) {
                case SqlDataType.Bit:
                    return typeof(Boolean);
                case SqlDataType.TinyInt:
                    return typeof(byte);
                case SqlDataType.SmallInt:
                    return typeof(Int16);
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

        internal static DbType GetDbType(SqlDataType sqlDataType) {
            switch (sqlDataType) {
                case SqlDataType.Bit:
                    return DbType.Boolean;
                case SqlDataType.SmallInt:
                    return DbType.Int16;
                case SqlDataType.TinyInt:
                    return DbType.Byte;
                case SqlDataType.Int:
                    return DbType.Int32;
                case SqlDataType.BigInt:
                    return DbType.Int64;
                case SqlDataType.SmallMoney:
                case SqlDataType.Money:                
                case SqlDataType.Numeric:
                    return DbType.Currency;
                case SqlDataType.Decimal:
                    return DbType.Decimal;
                case SqlDataType.Real:
                    return DbType.Single;
                case SqlDataType.Float:
                    return DbType.Double;
                case SqlDataType.Char:
                    return DbType.AnsiStringFixedLength;
                case SqlDataType.NChar:
                    return DbType.StringFixedLength;
                case SqlDataType.VarChar:
                    return DbType.AnsiString;
                case SqlDataType.NVarChar:
                    return DbType.String;
                case SqlDataType.Text:
                    return DbType.AnsiString;
                case SqlDataType.NText:
                    return DbType.String;
                case SqlDataType.VarCharMax:
                    return DbType.AnsiString;
                case SqlDataType.NVarCharMax:
                    return DbType.String;;
                case SqlDataType.UniqueIdentifier:
                    return DbType.Guid;
                case SqlDataType.Date:
                    return DbType.Date;
                case SqlDataType.SmallDateTime:
                case SqlDataType.DateTime:
                    return DbType.DateTime;
                case SqlDataType.DateTime2:
                    return DbType.DateTime2;
                case SqlDataType.DateTimeOffset:
                    return DbType.DateTimeOffset;
                case SqlDataType.Time:
                    return DbType.Time;
                case SqlDataType.Binary:                    
                case SqlDataType.VarBinary:
                case SqlDataType.VarBinaryMax:
                case SqlDataType.Image:
                case SqlDataType.Timestamp:
                    return DbType.Binary;
                case SqlDataType.Variant:
                    return DbType.Object;
                default:
                    throw new InvalidOperationException("Type not supported: " + sqlDataType.ToString());
            }
        }
        
        internal static DbType GetDbType(SqlDbType sqlDbType) {
            switch (sqlDbType) {
                case SqlDbType.BigInt:
                    return DbType.Int64;
                case SqlDbType.Binary:
                    return DbType.Binary;
                case SqlDbType.Bit:
                    return DbType.Boolean;
                case SqlDbType.Char:
                    return DbType.AnsiStringFixedLength;
                case SqlDbType.DateTime:
                    return DbType.DateTime;
                case SqlDbType.Decimal:
                    return DbType.Decimal;
                case SqlDbType.Float:
                    return DbType.Double;
                case SqlDbType.Image:
                    return DbType.Binary;
                case SqlDbType.Int:
                    return DbType.Int32;
                case SqlDbType.Money:
                    return DbType.Currency;
                case SqlDbType.NChar:
                    return DbType.StringFixedLength;
                case SqlDbType.NText:
                    return DbType.String;
                case SqlDbType.NVarChar:
                    return DbType.String;
                case SqlDbType.Real:
                    return DbType.Single;
                case SqlDbType.UniqueIdentifier:
                    return DbType.Guid;
                case SqlDbType.SmallDateTime:
                    return DbType.DateTime;
                case SqlDbType.SmallInt:
                    return DbType.Int16;
                case SqlDbType.SmallMoney:
                    return DbType.Currency;
                case SqlDbType.Text:
                    return DbType.AnsiString;
                case SqlDbType.Timestamp:
                    return DbType.Binary;
                case SqlDbType.TinyInt:
                    return DbType.Byte;
                case SqlDbType.VarBinary:
                    return DbType.Binary;
                case SqlDbType.VarChar:
                    return DbType.AnsiString;
                case SqlDbType.Variant:
                    return DbType.Object;
                case SqlDbType.Xml:
                    return DbType.Xml;
                case SqlDbType.Date:
                    return DbType.Date;
                case SqlDbType.Time:
                    return DbType.Time;
                case SqlDbType.DateTime2:
                    return DbType.DateTime2;
                case SqlDbType.DateTimeOffset:
                    return DbType.DateTimeOffset;
            }
            return DbType.Object;
        }


    }
}
