using System.Collections.Generic;

namespace Product.BuildingBlocks.Domain
{
    public interface IFields
    {
        string EntityLogicalName { get; }

        List<string> FieldsList{ get; }
    }
}