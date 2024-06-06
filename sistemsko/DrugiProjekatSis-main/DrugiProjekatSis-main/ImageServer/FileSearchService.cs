using System.IO;

namespace ImageServer
{
    public static class FileSearchService
    {
        public static string SearchFileRecursively(string rootPath, string fileName)
        {
            foreach (var file in Directory.GetFiles(rootPath, fileName, SearchOption.AllDirectories))
            {
                return file;
            }
            return null;
        }
    }
}
