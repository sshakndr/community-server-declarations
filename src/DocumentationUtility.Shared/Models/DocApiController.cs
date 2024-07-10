using DocumentationUtility.Shared.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DocumentationUtility.Shared.Models
{
    public abstract class DocApiController : DocCSharpItem
    {
        public List<DocApiMethod> ApiMethods { get; protected set; } = new List<DocApiMethod>();

        public string Path { get; protected set; }

        public DocApiController(Type type) : base(type)
        {
            this.type = type;

            ParseXml();

            ParseReflection();
            ParseMethods();
        }

        protected abstract void ParseMethods();

        protected abstract void ParseReflection();

        protected override bool HandleElement(XElement element)
        {
            return base.HandleElement(element);
        }

        protected readonly Type type;
    }
}
