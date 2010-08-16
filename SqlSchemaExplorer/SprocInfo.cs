using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace SqlSchemaExplorer {
    public class SprocInfo {

        public static SprocInfo ScanSproc(StoredProcedure sproc, SprocResultScanner sprocResultScanner) {
            var sprocInfo = new SprocInfo();

            sprocInfo.name = sproc.Name;
            if (sproc.ExtendedProperties.Contains("MS_Description") &&
                sproc.ExtendedProperties["MS_Description"].Value != null)
                sprocInfo.description = sproc.ExtendedProperties["MS_Description"].Value.ToString();

            sprocInfo.parameters = new HashSet<SprocParameterInfo>();
            foreach (var param in sproc.Parameters.Cast<StoredProcedureParameter>()) {
                sprocInfo.parameters.Add(SprocParameterInfo.ScanParameter(param));
            }

            sprocInfo.results = sprocResultScanner.GetResults(sproc);

            return sprocInfo;
        }

        private SprocInfo() {
            description = string.Empty;
        }

        private string name;
        private string description;
        private HashSet<SprocParameterInfo> parameters;
        private HashSet<SprocResultInfo> results;
        
        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public IEnumerable<SprocParameterInfo> InParameters { get { return parameters.Where(x => !x.IsOutputParameter); } }
        public IEnumerable<SprocParameterInfo> OutParameters { get { return parameters.Where(x => x.IsOutputParameter); } }
        public IEnumerable<SprocResultInfo> Results { get { return results; } }
    }
}
