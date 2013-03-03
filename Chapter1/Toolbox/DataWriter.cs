using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Toolbox
{
    public static class DataWriter
    {
        /// <summary>
        /// Crée un clone d'une DataTable et en importe toutes les DataRows
        /// </summary>
        /// <param name="tab">La DataTable à cloner</param>
        /// <returns>La DataTable clonée et remplie</returns>
        public static DataTable CloneWithContent(DataTable tab)
        {
            DataTable clone = tab.Clone();

            foreach (DataRow row in tab.Rows)
                clone.ImportRow(row);

            return clone;
        }

        #region XML
        /// <summary>
        /// Crée une DataTable et y importe des données depuis un fichier XML situé sur le bureau
        /// </summary>
        /// <param name="fileName">Le nom du fichier XML source</param>
        /// <param name="withSchemaFile">Indique s'il faut lire le schéma de données dans un fichier XSD séparé (vrai par défaut)</param>
        /// <returns>La DataTable remplie</returns>
        /// <remarks>
        /// En cas de doute sur le contenu du fichier xml (DataTable ou DataSet), penser à utiliser un try/catch !
        /// L'éventuel fichier XSD doit porter le même nom que le fichier XML
        /// </remarks>
        public static DataTable XmlToTable(string fileName, bool withSchemaFile = true)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return XmlToTable(fileName, filePath, withSchemaFile);
        }

        /// <summary>
        /// Crée une DataTable et y importe des données depuis un fichier XML
        /// </summary>
        /// <param name="fileName">Le nom du fichier XML source</param>
        /// <param name="filePath">Le chemin du fichier XML source</param>
        /// <param name="withSchemaFile">Indique s'il faut lire le schéma de données dans un fichier XSD séparé (vrai par défaut)</param>
        /// <returns>La DataTable remplie</returns>
        /// <remarks>
        /// En cas de doute sur le contenu du fichier xml (DataTable ou DataSet), penser à utiliser un try/catch !
        /// L'éventuel fichier XSD doit porter le même nom que le fichier XML
        /// </remarks>
        public static DataTable XmlToTable(string fileName, string filePath, bool withSchemaFile = true)
        {
            DataTable tab = new DataTable();

            if (withSchemaFile)
                tab.ReadXmlSchema(ToolBoxUtilities.SchemaPath(fileName, filePath));

            tab.ReadXml(ToolBoxUtilities.XmlPath(fileName, filePath));

            return tab;
        }

        /// <summary>
        /// Crée un DataSet et y importe des données depuis un fichier XML situé sur le bureau
        /// </summary>
        /// <param name="fileName">Le nom du fichier XML source</param>
        /// <param name="withSchemaFile">Indique s'il faut lire le schéma de données dans un fichier XSD séparé (vrai par défaut)</param>
        /// <returns>Le DataSet rempli</returns>
        /// <remarks>
        /// En cas de doute sur le contenu du fichier xml (DataTable ou DataSet), penser à utiliser un try/catch !
        /// L'éventuel fichier XSD doit porter le même nom que le fichier XML
        /// </remarks>
        public static DataSet XmlToSet(string fileName, bool withSchemaFile = true)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return XmlToSet(fileName, filePath, withSchemaFile);
        }

        /// <summary>
        /// Crée un DataSet et y importe des données depuis un fichier XML
        /// </summary>
        /// <param name="fileName">Le nom du fichier XML source</param>
        /// <param name="filePath">Le chemin du fichier XML source</param>
        /// <param name="withSchemaFile">Indique s'il faut lire le schéma de données dans un fichier XSD séparé (vrai par défaut)</param>
        /// <returns>Le DataSet rempli</returns>
        /// <remarks>
        /// En cas de doute sur le contenu du fichier xml (DataTable ou DataSet), penser à utiliser un try/catch !
        /// L'éventuel fichier XSD doit porter le même nom que le fichier XML
        /// </remarks>
        public static DataSet XmlToSet(string fileName, string filePath, bool withSchemaFile = true)
        {
            DataSet set = new DataSet();

            if (withSchemaFile)
                set.ReadXmlSchema(ToolBoxUtilities.SchemaPath(fileName, filePath));

            set.ReadXml(ToolBoxUtilities.XmlPath(fileName, filePath));

            return set;
        }
        #endregion

        #region Binary
        /// <summary>
        /// Crée une DataTable et y importe des données depuis un fichier binaire situé sur le bureau
        /// </summary>
        /// <param name="fileName">Le nom du fichier binaire source</param>
        /// <returns>La DataTable remplie</returns>
        public static DataTable BinaryToTable(string fileName)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return BinaryToTable(fileName, filePath);
        }

        /// <summary>
        /// Crée une DataTable et y importe des données depuis un fichier binaire situé sur le bureau
        /// </summary>
        /// <param name="fileName">Le nom du fichier binaire source</param>
        /// <param name="filePath">Le chemin du fichier binaire source</param>
        /// <returns>La DataTable Remplie</returns>
        public static DataTable BinaryToTable(string fileName, string filePath)
        {
            DataTable tab = new DataTable();

            FileStream fs = new FileStream(ToolBoxUtilities.BinaryPath(fileName, filePath), FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            tab = (DataTable)bf.Deserialize(fs);
            fs.Close();

            return tab;
        }

        /// <summary>
        /// Crée un DataSet et y importe des données depuis un fichier binaire situé sur le bureau
        /// </summary>
        /// <param name="fileName">Le nom du fichier binaire source</param>
        /// <returns>Le DataSet rempli</returns>
        public static DataSet BinaryToSet(string fileName)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return BinaryToSet(fileName, filePath);
        }

        /// <summary>
        /// Crée un DataSet et y importe des données depuis un fichier binaire situé sur le bureau
        /// </summary>
        /// <param name="fileName">Le nom du fichier binaire source</param>
        /// <param name="filePath">Le chemin du fichier binaire source</param>
        /// <returns>Le DataSet rempli</returns>
        public static DataSet BinaryToSet(string fileName, string filePath)
        {
            DataSet set = new DataSet();

            FileStream fs = new FileStream(ToolBoxUtilities.BinaryPath(fileName, filePath), FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            set = (DataSet)bf.Deserialize(fs);
            fs.Close();

            return set;
        }
        #endregion
    }
}
