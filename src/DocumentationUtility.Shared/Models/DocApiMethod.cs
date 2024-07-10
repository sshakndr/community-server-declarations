using DocumentationUtility.Shared.Models.Abstract;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace DocumentationUtility.Shared.Models
{
    public abstract class DocApiMethod : DocCSharpItem
    {
        public DocApiController Controller { get; protected set; }

        public string Category { get; protected set; }

        public string ShortDescription { get; protected set; }

        public string Method { get; protected set; }

        public string Path { get; protected set; }

        public List<DocApiParameter> Parameters { get; protected set; } = new List<DocApiParameter>();
        public DocApiReturn Returns { get; protected set; }

        public DocApiMethod(DocApiController controller, MethodInfo type) : base(type)
        {
            this.type = type;

            Controller = controller;

            ParseReflection();

            ParseXml();
        }

        protected override bool HandleElement(XElement element)
        {
            return base.HandleElement(element);
        }

        protected abstract void ParseReflection();

        protected readonly MethodInfo type;
    }
}
