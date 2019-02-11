using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FindReferences
{
    [XmlRoot(Namespace = null)]
    public class ProjectReference
    {
        [XmlAttribute]
        public  string Include { get; set; }

        [XmlElement(Namespace = "")]
        public string Project { get; set; }

        [XmlElement]
        public  string Name { get; set; }
    }
}
