using DocumentationUtility.Shared.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentationUtility.Shared.Markdown
{
    public static class MarkdownRenderer
    {
        public static void RenderApiControllers(List<DocApiController> apiControllers, string folderName)
        {
            var outPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            Directory.CreateDirectory(outPath);

            foreach (var controller in apiControllers)
            {
                var controllerMdPath = Path.Combine(outPath, controller.Name + ".md");
                File.WriteAllText(controllerMdPath, GetContentForController(controller));

                var controllerPath = Path.Combine(outPath, controller.Name);
                Directory.CreateDirectory(controllerPath);

                foreach (var method in controller.ApiMethods)
                {
                    var methodMdPath = Path.Combine(controllerPath, method.Name + ".md");
                    File.WriteAllText(methodMdPath, GetContentForMethod(method));
                }
            }
        }

        private static string GetContentForController(DocApiController controller)
        {
            var sb = new StringBuilder();

            sb
                .AppendHeader(controller.Name)
                .AppendLine(controller.Description)
                .AppendLine();

            foreach (var method in controller.ApiMethods)
            {
                sb
                    .Append("* ")
                    .AppendLink(method.Name, "#")
                    .Append($" - {method.Description}")
                    .AppendLine();
            }

            return sb.ToString();
        }

        private static string GetContentForMethod(DocApiMethod method)
        {
            var sb = new StringBuilder();

            sb
                .AppendHeader(method.Path)
                .AppendLine(method.Description)
                .AppendLine();

            sb
                .AppendHeader("Parameters", false);

            foreach (var param in method.Parameters)
            {
                sb
                    .Append("* ")
                    .AppendLink(param.Name, "#")
                    .Append($" - {param.Description} - {param.Type.Name}")
                    .AppendLine();
            }

            return sb.ToString();
        }

        private static StringBuilder AppendHeader(this StringBuilder sb, string header, bool h1 = true)
        {
            return sb
                .AppendLine(header)
                .AppendLine(new string(h1 ? '=' : '-', header.Length))
                .AppendLine();
        }

        private static StringBuilder AppendLink(this StringBuilder sb, string text, string link)
        {
            return sb
                .Append($"[{text}]({link})");
        }
    }
}
