using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ContractResolverJsonProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var entity = new TenantEntity() { Name = "testName" };
            entity.SerailizableProperties = new List<string>() { "Name" };
            var ob = JsonConvert.SerializeObject(entity,
                new JsonSerializerSettings() {ContractResolver = new ShouldSerializeContractResolver()});
        }
    }
}
