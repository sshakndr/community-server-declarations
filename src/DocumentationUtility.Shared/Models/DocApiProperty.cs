using DocumentationUtility.Shared.Models.Abstract;
using System.Reflection;

namespace DocumentationUtility.Shared.Models
{
    public class DocApiProperty : DocCSharpItem
    {
        public DocApiType Type { get; protected set; }

        public DocApiProperty(DocApiType type, PropertyInfo prop) : base(prop)
        {
            Type = type;

            ParseXml();
        }
    }
}
