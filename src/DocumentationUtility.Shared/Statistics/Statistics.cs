using DocumentationUtility.Shared.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentationUtility.Shared.Statistics
{
    public static class Statistics
    {
        // ToDo: log
        private static Dictionary<string, int> unhandled = new Dictionary<string, int>();
        private static Dictionary<string, HashSet<string>> unhandledItems = new Dictionary<string, HashSet<string>>();
        // ToDo: log
        private static List<string> unfoundXmlDocsKeys = new List<string>();
        private static List<string> usedXmlDocs = new List<string>();
        private static Dictionary<string, int> documentation = new Dictionary<string, int>
        {
            { "Method", 0 },
            { "Property", 0 },
            { "Type", 0 },
            { "Field", 0 },
            { "Event", 0 },
            { "Constructor", 0 }
        };

        public static void CountDocumentation(string member, string key)
        {
            if (member == null)
            {
                if (!unfoundXmlDocsKeys.Contains(key)) unfoundXmlDocsKeys.Add(key);
                return;
            }
            if (!usedXmlDocs.Contains(key))
            {
                usedXmlDocs.Add(key);
                documentation[member]++;
            }
        }

        public static void CountUnhandled(string tag, string item)
        {
            if (unhandled.ContainsKey(tag))
            {
                unhandled[tag]++;
                unhandledItems[tag].Add(item);
            }
            else 
            {
                unhandled.Add(tag, 1);
                unhandledItems.Add(tag, new HashSet<string> { item });
            }
        }

        public static void PrintDocumentation(bool fullInfo)
        {
            int total = 0;
            Console.WriteLine("\nUsed XML documentation");
            Console.WriteLine("+-------------------+-------------------+");
            Console.WriteLine("| member".PadRight(20) + "| used / loaded".PadRight(20) + "|");
            Console.WriteLine("+-------------------+-------------------+");
            foreach (var d in documentation)
            {
                int c = XmlDocs.XmlDocs.GetLoadedXmlCount(d.Key.Substring(0,1).Split(new string[] { "" }, StringSplitOptions.None));
                total += d.Value;
                Console.WriteLine($"| {d.Key}".PadRight(20) + $"| {d.Value} / {c}".PadRight(20) + "|");
            }
            Console.WriteLine("+-------------------+-------------------+");
            Console.WriteLine("| TOTAL".PadRight(20) + $"| {total} / {XmlDocs.XmlDocs.GetLoadedXmlCount(new string[] {})}".PadRight(20) + "|");
            Console.WriteLine("+-------------------+-------------------+");
            if (fullInfo)
            {
                XmlDocs.XmlDocs.GetLoadedXmlCount(documentation.Keys.Select(s => s.Substring(0, 1)).ToArray());
                if (unfoundXmlDocsKeys.Count() > 0)
                {
                    Console.WriteLine($"\nUnfound in loaded xml documentation keys[{unfoundXmlDocsKeys.Count()}]:");
                    unfoundXmlDocsKeys.Sort();
                    foreach (var u in unfoundXmlDocsKeys) Console.WriteLine(u);
                }
            }
        }

        public static void PrintUnhandled(bool fullInfo)
        {
            Console.WriteLine("\nUnhandled elements");
            if (fullInfo)
            {
                Console.WriteLine("+-------------------+---------+---------------------------------------------------------------------+");
                Console.WriteLine("| tag".PadRight(20) + "| count".PadRight(10) + "| from".PadRight(70) + "|");
                Console.WriteLine("+-------------------+---------+---------------------------------------------------------------------+");
                foreach (var e in unhandled)
                {
                    var i = unhandledItems[e.Key].ToArray();
                    Console.WriteLine($"| {e.Key}".PadRight(20) + $"| {e.Value}".PadRight(10) + $"| {i[0]}".PadRight(70) + "|");
                    if (i.Length > 1) for (int j = 1; j<i.Length; j++) Console.WriteLine("| ".PadRight(20) + "| ".PadRight(10) + $"| {i[j]}".PadRight(70) + "|");
                    Console.WriteLine("+-------------------+---------+---------------------------------------------------------------------+");
                }
            } else
            {
                Console.WriteLine("+-------------------+---------+");
                Console.WriteLine("| tag".PadRight(20) + "| count".PadRight(10) + "|");
                Console.WriteLine("+-------------------+---------+");
                foreach (var e in unhandled) Console.WriteLine($"| {e.Key}".PadRight(20) + $"| {e.Value}".PadRight(10) + "|");
                Console.WriteLine("+-------------------+---------+");
            }
        }

        public static void PrintAll(bool fullInfo)
        {
            PrintDocumentation(fullInfo);
            PrintUnhandled(fullInfo);
        }
    }
}
