using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Product.BuildingBlocks.Application
{
    [DataContract]
    public class ApplicationQueryResultWrapper<TResult>
    {
        public ApplicationQueryResultWrapper()
        {
            Links = new List<Link>();
        }

        [DataMember(Name = "result")]
        public TResult Result { get; set; }

        [DataMember(Name = "_links")]
        public List<Link> Links { get; }

        [DataMember(Name = "canDo")]
        public bool CanVerify { get; set; }
    }
}
