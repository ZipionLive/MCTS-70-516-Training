using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace Toolbox
{
    public static class DataSerializer
    {
        /// <summary>
        /// Sérialise une DataTable en XML (le fichier sera enregistré sur le bureau)
        /// </summary>
        /// <param name="tab">La DataTable à sérialiser</param>
        /// <param name="fileName">Le nom du fichier XML</param>
        /// <param name="withSchema">Inclure le schéma ? (vrai par défaut)</param>
        public static void DataToXml(DataTable tab, string fileName, bool withSchema = true)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DataToXml(tab, fileName, filePath, withSchema);
        }

        /// <summary>
        /// Sérialise une DataTable en XML
        /// </summary>
        /// <param name="tab">La DataTable à sérialiser</param>
        /// <param name="fileName">Le nom du fichier XML</param>
        /// <param name="filePath">Le chemin du fichier XML</param>
        /// <param name="withSchema">Inclure le schéma ? (vrai par défaut)</param>
        public static void DataToXml(DataTable tab, string fileName, string filePath, bool withSchema = true)
        {
            if (withSchema)
                tab.WriteXml(ToolBoxUtilities.FullFilePath(fileName, filePath), XmlWriteMode.WriteSchema);
            else
                tab.WriteXml(ToolBoxUtilities.FullFilePath(fileName, filePath));
        }

        /// <summary>
        /// Sérialise un DataSet en XML (le fichier sera enregistré sur le bureau)
        /// </summary>
        /// <param name="tab">Le DataSet à sérialiser</param>
        /// <param name="fileName">Le nom du fichier XML</param>
        /// <param name="withSchema">Inclure le schéma ? (vrai par défaut)</param>
        public static void DataToXml(DataSet set, string fileName, bool withSchema = true)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DataToXml(set, fileName, filePath, withSchema);
        }

        /// <summary>
        /// Sérialise un DataSet en XML
        /// </summary>
        /// <param name="tab">Le DataSet à sérialiser</param>
        /// <param name="fileName">Le nom du fichier XML</param>
        /// <param name="filePath">Le chemin du fichier XML</param>
        /// <param name="withSchema">Inclure le schéma ? (vrai par défaut)</param>
        public static void DataToXml(DataSet set, string fileName, string filePath, bool withSchema = true)
        {
            if (withSchema)
                set.WriteXml(ToolBoxUtilities.FullFilePath(fileName, filePath), XmlWriteMode.WriteSchema);
            else
                set.WriteXml(ToolBoxUtilities.FullFilePath(fileName, filePath));
        }
    }
}
