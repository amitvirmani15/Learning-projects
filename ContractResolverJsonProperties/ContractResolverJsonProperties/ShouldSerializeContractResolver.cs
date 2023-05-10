using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ContractResolverJsonProperties
{
    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        private IList<string> seralizableProperties;
        public ShouldSerializeContractResolver(IList<string> serilizableProperties)
        {
            this.seralizableProperties = serilizableProperties;
        }
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            properties.First().ValueProvider.GetValue()
            // only serializer properties that start with the specified character
            properties =
                properties.Where(x => x.PropertyName.Intersect(seralizableProperties.AsEnumerable()).ToList();

            return properties;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            
            return base.CreateProperty(member, memberSerialization);
        }

        protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
        {
            member.
            return base.CreateMemberValueProvider(member);
        }

        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            return base.GetSerializableMembers(objectType);
        }
    }
}
