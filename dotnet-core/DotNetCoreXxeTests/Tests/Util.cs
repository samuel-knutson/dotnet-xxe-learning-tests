using System.IO;
using System.Reflection;

namespace DotNetFrameworkXxeTests.Tests
{
    static class Util
    {
        public static string XslFilepath => GetResourceFilePath("test.xsl");

        public static string Payload => GetResourceFileContents("xxe_attack.txt");

        public static string XmlWithPayload
        {
            get
            {
                string xml = GetResourceFileContents("test.xml");
                string payloadFilepath = GetResourceFilePath("xxe_attack.txt");
                return xml.Replace("_ATTACK_FILEPATH_PLACEHOLDER_", payloadFilepath);
            }
        }

        public static string ResourceFolderPath
        {
            get
            {
                string assemblyLocation = Assembly.GetExecutingAssembly().Location;
                string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
                return Path.Combine(assemblyDirectory, "Resources");
            }
        }

        public static string GetResourceFileContents(string fileName)
        {
            string resourceFilepath = GetResourceFilePath(fileName);
            return File.ReadAllText(resourceFilepath);
        }

        public static string GetResourceFilePath(string fileName)
        {
            string resourceFolderPath = ResourceFolderPath;
            return Path.Combine(resourceFolderPath, fileName);
        }

        public static void SaveResourceFile(string fileName, string fileContents)
        {
            string filePath = GetResourceFilePath(fileName);
            File.WriteAllText(filePath, fileContents);
        }

        public static void DeleteResourceFile(string fileName)
        {
            string filePath = GetResourceFilePath(fileName);
            File.Delete(filePath);
        }
    }
}
