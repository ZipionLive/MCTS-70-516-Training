using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Toolbox
{
    /// <summary>
    /// Contient des méthodes permettant de sérialiser des DataTables et DataSets en XML ou binaire
    /// </summary>
    public static class DataSerializer
    {
        #region XML
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
            {
                tab.WriteXmlSchema(ToolBoxUtilities.SchemaPath(fileName, filePath));
                tab.WriteXml(ToolBoxUtilities.XmlPath(fileName, filePath));
            }
            else
                tab.WriteXml(ToolBoxUtilities.XmlPath(fileName, filePath));
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
            {
                set.WriteXmlSchema(ToolBoxUtilities.SchemaPath(fileName, filePath));
                set.WriteXml(ToolBoxUtilities.XmlPath(fileName, filePath));
            }
            else
                set.WriteXml(ToolBoxUtilities.XmlPath(fileName, filePath));
        }
        #endregion

        #region Binary
        /// <summary>
        /// Sérialise une DataTable en binaire
        /// </summary>
        /// <param name="tab">La DataTable à sérialiser</param>
        /// <param name="fileName">Le nom du fichier binaire</param>
        /// <param name="trueBinary">Indique s'il faut créer un véritable fichier binaire (par défaut, des données XML seront incluses)</param>
        public static void DataToBinary(DataTable tab, string fileName, bool trueBinary = false)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DataToBinary(tab, fileName, filePath, trueBinary);
        }

        /// <summary>
        /// Sérialise une DataTable en binaire
        /// </summary>
        /// <param name="tab">La DataTable à sérialiser</param>
        /// <param name="fileName">Le nom du fichier binaire</param>
        /// <param name="filePath">Le chemin du fichier binaire</param>
        /// <param name="trueBinary">Indique s'il faut créer un véritable fichier binaire (par défaut, des données XML seront incluses)</param>
        public static void DataToBinary(DataTable tab, string fileName, string filePath, bool trueBinary = false)
        {
            if (trueBinary)
                tab.RemotingFormat = SerializationFormat.Binary;

            FileStream fs = new FileStream(ToolBoxUtilities.BinaryPath(fileName, filePath), FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, tab);
            fs.Close();
        }

        /// <summary>
        /// Sérialise un DataSet en binaire
        /// </summary>
        /// <param name="tab">Le DataSet à sérialiser</param>
        /// <param name="fileName">Le nom du fichier binaire</param>
        /// <param name="trueBinary">Indique s'il faut créer un véritable fichier binaire (par défaut, des données XML seront incluses)</param>
        public static void DataToBinary(DataSet set, string fileName, bool trueBinary = false)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DataToBinary(set, fileName, filePath, trueBinary);
        }

        /// <summary>
        /// Sérialise un DataSet en binaire
        /// </summary>
        /// <param name="tab">Le DataSet à sérialiser</param>
        /// <param name="fileName">Le nom du fichier binaire</param>
        /// <param name="filePath">Le chemin du fichier binaire</param>
        /// <param name="trueBinary">Indique s'il faut créer un véritable fichier binaire (par défaut, des données XML seront incluses)</param>
        public static void DataToBinary(DataSet set, string fileName, string filePath, bool trueBinary = false)
        {
            if (trueBinary)
                set.RemotingFormat = SerializationFormat.Binary;

            FileStream fs = new FileStream(ToolBoxUtilities.BinaryPath(fileName, filePath), FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, set);
            fs.Close();
        }
        #endregion
    }
}
