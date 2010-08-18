using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace SqlSchemaExplorer {
    public class SprocParameterInfo : ColumnInfo {
        public static SprocParameterInfo ScanParameter(StoredProcedureParameter param) {
            var sprocParameterInfo = new SprocParameterInfo();

            sprocParameterInfo.name = param.Name;
            if (param.ExtendedProperties.Contains("MS_Description") &&
                param.ExtendedProperties["MS_Description"].Value != null)
                sprocParameterInfo.description = param.ExtendedProperties["MS_Description"].Value.ToString();

            sprocParameterInfo.nativeType = param.DataType.SqlDataType.ToString();
            sprocParameterInfo.maximumLength = param.DataType.MaximumLength;
            sprocParameterInfo.numericPrecision = param.DataType.NumericPrecision;
            sprocParameterInfo.numericScale = param.DataType.NumericScale;
            sprocParameterInfo.defaultValue = param.DefaultValue;
            sprocParameterInfo.isOutputParameter = param.IsOutputParameter;

            return sprocParameterInfo;
        }

        private SprocParameterInfo() { }

        private bool isOutputParameter;

        public bool IsOutputParameter { get { return isOutputParameter; } }
    }
}
