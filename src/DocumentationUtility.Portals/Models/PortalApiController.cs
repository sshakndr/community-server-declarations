using ASC.Api.Attributes;
using DocumentationUtility.Shared.Models;
using DocumentationUtility.Shared.Statistics;
using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace DocumentationUtility.Portals.Models
{
    public class PortalApiController : DocApiController
    {
        public PortalApiController(Type type) : base(type) { }

        protected override void ParseMethods()
        {
            foreach (var method in type.GetMethods().Where(m => m.GetCustomAttributes<ApiAttribute>().Any()))
            {
                var m = new PortalApiMethod(this, method);
                if (m.IsVisible) ApiMethods.Add(m);
            }
        }

        protected override void ParseReflection()
        {
            Path = Name;
        }

        protected override bool HandleElement(XElement element)
        {
            return base.HandleElement(element);
        }
    }
}
