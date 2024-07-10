using System;

namespace DocumentationUtility.Shared.Models
{
    public class DocApiReturn
    {
        public DocApiMethod Method { get; protected set; }

        public string Description { get; protected set; }

        public DocApiType Type { get; protected set; }

        public DocApiReturn(DocApiMethod method, string description, Type type)
        {
            Method = method;
            Type = new DocApiType(type);

            Description = description;
        }
    }
}
