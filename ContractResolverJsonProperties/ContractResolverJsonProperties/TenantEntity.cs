using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ContractResolverJsonProperties
{
    
    [Serializable()]
    public class TenantEntity : BaseEntity
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonConverter(BitConverter)]
        public string Name { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Type { get; set; }
    }
}
