using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Toolbox
{
    public static class TableReader
    {
        /// <summary>
        /// Convertit une DataTable vers un string
        /// </summary>
        /// <param name="tab">La DataTable à convertir</param>
        /// <param name="colWidth">La taille des colonnes (en nombre de caractères)</param>
        public static string TabToString(DataTable tab, int colWidth = 15)
        {
            StringBuilder sb = new StringBuilder();
            string colFormat = "{0," + colWidth + "} ";

            foreach (DataColumn col in tab.Columns)
                sb.AppendFormat(colFormat, col.ColumnName);

            sb.AppendLine();
            foreach (DataRow row in tab.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    sb.Append("Deleted row");

                else
                {
                    foreach (DataColumn col in tab.Columns)
                        sb.AppendFormat(colFormat, row[col]);
                }
            }
            sb.AppendLine();

            return sb.ToString();
        }

        /// <summary>
        /// Convertit une DataView vers un string
        /// </summary>
        /// <param name="view">La DataView à convertir</param>
        /// <param name="colWidth">La taille des colonnes (en nombre de caractères)</param>
        public static string ViewToString(DataView view, int colWidth = 15)
        {
            StringBuilder sb = new StringBuilder();
            string colFormat = "{0," + colWidth + "} ";

            foreach (DataColumn col in view.Table.Columns)
                sb.AppendFormat(colFormat, col.ColumnName);

            sb.AppendLine();
            foreach (DataRowView drv in view)
            {
                foreach (DataColumn col in view.Table.Columns)
                    sb.AppendFormat(colFormat, drv.Row[col]);
            }
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
