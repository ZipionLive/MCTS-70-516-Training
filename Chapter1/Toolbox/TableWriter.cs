using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Toolbox
{
    public static class TableWriter
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

        //public static DataTable ViewToTable(DataView view, string tabName = "Export")
        //{
        //    DataTable export = view.ToTable(
        //}
    }
}
