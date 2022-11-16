using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Nebby.Editor
{
    /// <summary>
    /// General System.IO related utilities.
    /// </summary>
    public static class IOUtils
    {
        /// <summary>
        /// If the directory specified in <paramref name="directoryPath"/> does not exist, it creates it.
        /// </summary>
        /// <param name="directoryPath">The directory path to ensure its existence</param>
        public static void EnsureDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        /// Formats <paramref name="path"/> so its valid for unity's systems.
        /// Replaces \ with / and returns a project relative path
        /// </summary>
        /// <param name="path">The path to modified</param>
        /// <returns>The formatted path</returns>
        public static string FormatPathForUnity(string path)
        {
            return FileUtil.GetProjectRelativePath(path.Replace("\\", "/"));
        }

        /// <summary>
        /// Returns the current directory from the active object.
        /// </summary>
        /// <returns>The active object'sdirectory</returns>
        public static string GetCurrentDirectory()
        {
            var activeObjectPath = Path.GetFullPath(AssetDatabase.GetAssetPath(Selection.activeObject));
            if (File.Exists(activeObjectPath))
            {
                activeObjectPath = Path.GetDirectoryName(activeObjectPath);
            }
            return activeObjectPath;
        }

        public static string GetDirectoryOfObject(Object obj)
        {
            var path = Path.GetFullPath(AssetDatabase.GetAssetPath(obj));
            if(File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            return path;
        }
    }
}
