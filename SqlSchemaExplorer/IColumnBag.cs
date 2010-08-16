using System.Collections.Generic;

namespace SqlSchemaExplorer {
    public interface IColumnBag {
        IEnumerable<ColumnInfo> Columns { get; }
        string Name { get; }
        TableOrView TableOrView { get; }
    }
}
