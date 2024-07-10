using DocumentationUtility.Shared.Json;
using DocumentationUtility.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;

namespace DocumentationUtility.Shared.Markdown
{
    public static class JsonRenderer
    {
        private static JsonSerializerSettings options;

        private static JsonSerializerSettings JsonOptions
        {
            get
            {
                if (options != null) return options;

                var opt = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                };

                opt.Converters.Add(new TypeJsonConverter());

                options = opt;

                return options;
            }
        }

        public static void RenderApiControllers(List<DocApiController> apiControllers, string folderName, string fileName)
        {
            var outPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            Directory.CreateDirectory(outPath);
            File.WriteAllText(Path.Combine(outPath, fileName), JsonConvert.SerializeObject(apiControllers, JsonOptions));
        }
    }
}
