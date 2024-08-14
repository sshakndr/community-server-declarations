using DocumentationUtility.Shared.XmlDocs;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace DocumentationUtility.Shared.Models.Abstract
{
    public abstract class DocCSharpItem : DocItem
    {
        public string Description { get; protected set; }
        public string Remarks { get; protected set; }
        public string Example { get; protected set; }

        [JsonIgnore]
        public bool IsVisible { get; protected set; } = true;

        public DocCSharpItem(MemberInfo type)
        {
            this.type = type;

            Name = type.Name;
        }

        protected void ParseXml()
        {
            var xml = type.GetDocumentation();
            if (xml == null) return;
            using (var xmlReader = XmlReader.Create(new StringReader(xml), xmlReaderSettings))
            {
                while (true)
                {
                    var readResult = true;
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            XElement el = XNode.ReadFrom(xmlReader) as XElement;
                            HandleElement(el);
                            break;

                        default:
                            readResult = xmlReader.Read();
                            break;
                    }
                    if (!readResult) break;
                }
            }
        }

        protected virtual bool HandleElement(XElement element)
        {
            switch (element.Name.LocalName)
            {
                case "summary":
                    Description = element.Value.Trim();
                    return true;

                case "name":
                    Name = element.Value.Trim();
                    return true;

                case "remarks":
                    Remarks = element.Value.Trim();
                    return true;

                case "example":
                    Example = element.Value.Trim();
                    return true;

                case "visible":
                    IsVisible = bool.Parse(element.Value.Trim());
                    return true;
            }

            string item = type.Module.Name + ":" + Name;
            if (GetType().Name != "PortalApiMethod") Statistics.Statistics.CountUnhandled(element.Name.ToString(), item);
            return false;
        }

        public override string ToString()
        {
            return Name;
        }

        private readonly MemberInfo type;
        private readonly XmlReaderSettings xmlReaderSettings = new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment, IgnoreWhitespace = true };
    }
}