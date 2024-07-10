using ASC.Api.Interfaces;
using DocumentationUtility.Portals.Models;
using DocumentationUtility.Shared.Markdown;
using DocumentationUtility.Shared.Models;
using DocumentationUtility.Shared.XmlDocs;
using DocumentationUtility.Shared.Statistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class Program
{
    public static void Main()
    {
        var dllPaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "ASC*.dll");

        foreach (var dll in dllPaths)
        {
            try
            {
                Console.WriteLine($"Loading dll {Path.GetFileName(dll)}");
                XmlDocs.LoadXmlDocumentation(Assembly.LoadFile(dll));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load dll {Path.GetFileName(dll)}\n{ex.Message}\n{ex.StackTrace}");
            }
        }

        var allTypes = XmlDocs.LoadedAssemblies
            .SelectMany(a => a.ExportedTypes)
            .ToList();
        var controllers = allTypes
            .Where(t => !t.IsAbstract && typeof(IApiEntryPoint).IsAssignableFrom(t))
            .ToList();

        var result = new List<PortalApiController>();

        foreach (var controller in controllers)
        {
            result.Add(new PortalApiController(controller));
        }

        JsonRenderer.RenderApiControllers(result.Cast<DocApiController>().ToList(), "json_out", "portals.json");

        Statistics.PrintAll(false);

        Console.WriteLine();
    }
}
