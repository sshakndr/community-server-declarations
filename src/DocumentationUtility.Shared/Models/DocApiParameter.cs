using System.Reflection;

namespace DocumentationUtility.Shared.Models
{
    public class DocApiParameter
    {
        public DocApiMethod Method { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public DocApiType Type { get; protected set; }

        public DocApiParameter(DocApiMethod method, ParameterInfo param, string description)
        {
            Method = method;

            Name = param.Name;
            Description = description;

            Type = new DocApiType(param.ParameterType);
        }
    }
}
