using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FindReferences
{
    public class Project
    {
        [XmlElement("ItemGroup")]
        public List<ItemGroup> ItemGroup { get; set; }

        [XmlElement("PropertyGroup")]
        public dynamic PropertyGroup { get; set; }
    }

    
    public class ItemGroup
    {
        [XmlElement("Reference")]
        public List<Reference> ProjectReference { get; set; }
    }
}
