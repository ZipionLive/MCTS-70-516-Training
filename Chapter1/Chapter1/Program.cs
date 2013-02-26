using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolbox;
using System.Data;
using System.IO;

namespace Chapter1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Garage
            //Garage myGarage = new Garage();

            //Console.WriteLine("Original Garage :");
            //Console.WriteLine(TableReader.TabToString(myGarage.cars));

            //DataTable garageClone = TableWriter.CloneWithContent(myGarage.cars);

            //garageClone.LoadDataRow(new object[] { null, "STU159", "Lancia", "Stratos", 1973 }, true);
            //garageClone.LoadDataRow(new object[] { null, "VWX357", "Ford", "GT40", 1969 }, true);

            //Console.WriteLine("Cloned garage :");
            //Console.WriteLine(TableReader.TabToString(garageClone));

            //DataView garageView = new DataView(garageClone);
            //garageView.Sort = "Year DESC, Plate ASC";
            //garageView.RowFilter = "Year < 1970";

            //Console.WriteLine("Garage view :");
            //Console.WriteLine(TableReader.ViewToString(garageView));

            //DataTable viewExport = garageView.ToTable("MyViewExport", true, "ID", "Plate", "Manufacturer", "Model", "Year");

            //Console.WriteLine("Table exported from the garage view :");
            //Console.WriteLine(TableReader.TabToString(viewExport));
            #endregion

            #region Parts Business
            PartsBusiness partsBiz = new PartsBusiness("PartsBusiness");
            DataTable sellers = partsBiz.sellers;
            DataTable parts = partsBiz.parts;

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            StringBuilder sb = new StringBuilder();

            foreach (string str in TableReader.DataSetToStrings(partsBiz, 25))
            {
                Console.WriteLine(str);

                sb.AppendLine(str);
            }

            using (StreamWriter outfile = new StreamWriter(filePath + @"\TabPrints.txt"))
            {
                outfile.Write(sb.ToString());
            }

            DataRow seller1 = sellers.Rows[0];
            DataRow Seller2 = sellers.Rows[1];
            DataRow seller3 = sellers.Rows[2];


            #endregion

            Console.ReadKey();
        }
    }
}
