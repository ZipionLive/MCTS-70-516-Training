using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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

        /// <summary>
        /// Crée une DataTable et y importe des données depuis un fichier XML situé sur le bureau
        /// </summary>
        /// <param name="fileName">Le nom du fichier XML source</param>
        /// <returns>La DataTable remplie</returns>
        /// <remarks>
        /// En cas de doute sur le contenu du fichier xml (DataTable ou DataSet), penser à utiliser un try/catch !
        /// </remarks>
        public static DataTable XmlToTable(string fileName)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return XmlToTable(fileName, filePath);
        }

        /// <summary>
        /// Crée une DataTable et y importe des données depuis un fichier XML
        /// </summary>
        /// <param name="fileName">Le nom du fichier XML source</param>
        /// <param name="filePath">Le chemin du fichier XML source</param>
        /// <returns>La DataTable remplie</returns>
        /// <remarks>
        /// En cas de doute sur le contenu du fichier xml (DataTable ou DataSet), penser à utiliser un try/catch !
        /// </remarks>

        public static DataTable XmlToTable(string fileName, string filePath)
        {
            DataTable tab = new DataTable();
            tab.ReadXml(ToolBoxUtilities.FullFilePath(fileName, filePath));

            return tab;
        }

        /// <summary>
        /// Crée un DataSet et y importe des données depuis un fichier XML situé sur le bureau
        /// </summary>
        /// <param name="fileName">Le nom du fichier XML source</param>
        /// <returns>Le DataSet rempli</returns>
        /// <remarks>
        /// En cas de doute sur le contenu du fichier xml (DataTable ou DataSet), penser à utiliser un try/catch !
        /// </remarks>
        public static DataSet XmlToSet(string fileName)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return XmlToSet(fileName, filePath);
        }

        /// <summary>
        /// Crée un DataSet et y importe des données depuis un fichier XML
        /// </summary>
        /// <param name="fileName">Le nom du fichier XML source</param>
        /// <param name="filePath">Le chemin du fichier XML source</param>
        /// <returns>Le DataSet rempli</returns>
        /// <remarks>
        /// En cas de doute sur le contenu du fichier xml (DataTable ou DataSet), penser à utiliser un try/catch !
        /// </remarks>
        public static DataSet XmlToSet(string fileName, string filePath)
        {
            DataSet set = new DataSet();
            set.ReadXml(ToolBoxUtilities.FullFilePath(fileName, filePath));

            return set;
        }
    }
}
