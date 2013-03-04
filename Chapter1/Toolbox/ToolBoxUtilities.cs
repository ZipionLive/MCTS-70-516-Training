using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Toolbox
{
    /// <summary>
    /// Contient des méthodes diverses
    /// </summary>
    public static class ToolBoxUtilities
    {
        /// <summary>
        /// Concatène les différents strings contenus dans une liste
        /// </summary>
        /// <param name="strList">Une liste de strings</param>
        public static string MergeStringList(List<string> strList)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string str in strList)
                sb.AppendLine(str + "\n");

            return sb.ToString();
        }

        #region File path generation
        /// <summary>
        /// Génère un chemin complêt pour un fichier XML à partir d'un emplacement et d'un nom de fichier
        /// </summary>
        /// <param name="fileName">Le nom du fichier</param>
        /// <param name="filePath">L'emplacement du fichier</param>
        /// <param name="fileExtension">L'extension du fichier (".xml" par défaut)</param>
        /// <returns>Le chemin complêt sous forme de string</returns>
        public static string XmlPath(string fileName, string filePath, string fileExtension = ".xml")
        {
            if (fileName.Substring(fileName.Length - 4, 1).Equals("."))
                fileName = fileName.Substring(0, fileName.Length - 4);
            fileName += fileExtension;
            return Path.Combine(filePath, fileName);
        }

        /// <summary>
        /// Génère un chemin complêt pour un fichier XSD à partir d'un emplacement et d'un nom de fichier
        /// </summary>
        /// <param name="fileName">Le nom du fichier</param>
        /// <param name="filePath">L'emplacement du fichier</param>
        /// <param name="fileExtension">L'extension du fichier (".xsd" par défaut)</param>
        /// <returns>Le chemin complêt sous forme de string</returns>
        public static string SchemaPath(string fileName, string filePath, string fileExtension = ".xsd")
        {
            if (fileName.Substring(fileName.Length - 4, 1).Equals("."))
                fileName = fileName.Substring(0, fileName.Length - 4);
            fileName += fileExtension;
            return Path.Combine(filePath, fileName);
        }

        /// <summary>
        /// Génère un chemin complêt pour un fichier BIN à partir d'un emplacement et d'un nom de fichier
        /// </summary>
        /// <param name="fileName">Le nom du fichier</param>
        /// <param name="filePath">L'emplacement du fichier</param>
        /// <param name="fileExtension">L'extension du fichier (".bin" par défaut)</param>
        /// <returns>Le chemin complêt sous forme de string</returns>
        public static string BinaryPath(string fileName, string filePath, string fileExtension = ".bin")
        {
            if (fileName.Substring(fileName.Length - 4, 1).Equals("."))
                fileName = fileName.Substring(0, fileName.Length - 4);
            fileName += ".zlb";
            return Path.Combine(filePath, fileName);
        }
        #endregion
    }
}
