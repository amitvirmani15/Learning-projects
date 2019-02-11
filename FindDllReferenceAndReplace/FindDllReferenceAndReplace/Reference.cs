using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FindReferences
{
    public class Reference
    {
        [XmlAttribute]
        public  string Include { get; set; }

        [XmlElement]
        public bool specificVersion { get; set; }

        [XmlElement]
        public string HintPath { get; set; }
    }
}
