using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Toolbox
{
    public static class TableReader
    {
        const int defaultColWidth = 15;

        /// <summary>
        /// Convertit une DataTable vers un string
        /// </summary>
        /// <param name="tab">La DataTable à convertir</param>
        /// <param name="colWidth">La taille des colonnes (en nombre de caractères)</param>
        public static string TabToString(DataTable tab, int colWidth = defaultColWidth)
        {
            StringBuilder sb = new StringBuilder();
            string colFormat = "{0," + colWidth + "} ";

            foreach (DataColumn col in tab.Columns)
            {
                if (col.DataType == typeof(Guid))
                    sb.AppendFormat("{0,9}", col.ColumnName);
                else
                    sb.AppendFormat(colFormat, col.ColumnName);
            }

            sb.AppendLine();
            foreach (DataRow row in tab.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    sb.Append("Deleted row");

                else
                {
                    foreach (DataColumn col in tab.Columns)
                    {
                        if (col.DataType == typeof(Guid))
                            sb.AppendFormat("{0,9}", "GUID");
                        else
                            sb.AppendFormat(colFormat, row[col]);
                    }
                }
                sb.AppendLine();
            }
            sb.AppendLine();

            return sb.ToString();
        }

        /// <summary>
        /// Convertit une DataView vers un string
        /// </summary>
        /// <param name="view">La DataView à convertir</param>
        /// <param name="colWidth">La taille des colonnes (en nombre de caractères)</param>
        public static string ViewToString(DataView view, int colWidth = defaultColWidth)
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

        public static List<string> DataSetToStrings(DataSet set, int colwidth = defaultColWidth)
        {
            List<string> tabPrints = new List<string>();

            foreach (DataTable tab in set.Tables)
            {
                tabPrints.Add(TabToString(tab, colwidth));
            }

            return tabPrints;
        }
    }
}
