using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Toolbox
{
    public static class ToolBoxUtilities
    {
        /// <summary>
        /// Génère un chemin complêt pour un fichier XML à partir d'un emplacement et d'un nom de fichier
        /// </summary>
        /// <param name="fileName">Le nom du fichier</param>
        /// <param name="filePath">L'emplacement du fichier</param>
        /// <returns>Le chemin complêt sous forme de string</returns>
        public static string XmlPath(string fileName, string filePath)
        {
            if (fileName.Substring(fileName.Length - 4).Equals(".xsd"))
                fileName = fileName.Substring(0, fileName.Length - 4);
            if (!fileName.Substring(fileName.Length - 4).Equals(".xml"))
                fileName += ".xml";
            return Path.Combine(filePath, fileName);
        }

        /// <summary>
        /// Génère un chemin complêt pour un fichier XSD à partir d'un emplacement et d'un nom de fichier
        /// </summary>
        /// <param name="fileName">Le nom du fichier</param>
        /// <param name="filePath">L'emplacement du fichier</param>
        /// <returns>Le chemin complêt sous forme de string</returns>
        public static string SchemaPath(string fileName, string filePath)
        {
            if (fileName.Substring(fileName.Length - 4).Equals(".xml"))
                fileName = fileName.Substring(0, fileName.Length - 4);
            if (!fileName.Substring(fileName.Length - 4).Equals(".xsd"))
                fileName += ".xsd";
            return Path.Combine(filePath, fileName);
        }

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
    }
}
