using DocumentationUtility.Shared.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DocumentationUtility.Shared.Models
{
    public class DocApiProperty : DocCSharpItem
    {
        public DocApiType Type { get; protected set; }

        public DocApiType ParentType { get; protected set; }

        public DocApiProperty(DocApiType type, PropertyInfo prop, List<string> parents = null) : base(prop)
        {
            ParentType = type;

            ParseXml();

            var p = new List<string>(parents ?? new List<string>()) { type.Name.Replace("[]", "") };

            if (CheckOverflow(p, prop.PropertyType))
            {
                Type = new DocApiType(prop.PropertyType, p);
            } else
            {
                string end = prop.PropertyType.Name.Contains("List") ? "[]" : "";
                Type = new DocApiType(typeof(Object), forceName: prop.PropertyType.Name + end);
            }
        }

        private bool CheckOverflow(List<string> type, Type prop)
        {
            if (type.Contains(prop.Name))
            {
                return false;
            }
            bool noRecursion = true;
            if (prop.Name.Contains("`"))
            {
                var types = prop.GetGenericArguments();
                foreach (var t in types)
                {
                    noRecursion = noRecursion && CheckOverflow(type, t);
                }
            }
            return noRecursion;
        }
    }
}
