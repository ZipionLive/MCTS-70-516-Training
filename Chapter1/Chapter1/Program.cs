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
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

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
            //PartsBusiness partsBiz = new PartsBusiness("PartsBusiness");
            //DataTable sellers = partsBiz.sellers;
            //DataTable parts = partsBiz.parts;

            //StringBuilder sb = new StringBuilder();

            //DataRow seller2 = sellers.Rows[1];

            //partsBiz.DelRow(sellers, seller2);

            //partsBiz.DelRow(parts, parts.Rows[parts.Rows.Count - 2]);

            //foreach (string str in TableReader.DataSetToStrings(partsBiz, 25))
            //{
            //    sb.AppendLine(str);
            //}

            //DataRow seller3 = sellers.Rows[sellers.Rows.Count - 1];

            //DataRow[] seller3Parts = seller3.GetChildRows("Sellers_Parts");

            //sb.AppendLine("Parts for " + seller3["Name"] + " :");
            //foreach (DataRow row in seller3Parts)
            //{
            //    sb.AppendFormat("{0} : {1, 20} | {2} | {3}\n", row["PartCode"], row["PartDescription"], row["Cost"], row["RetailPrice"]);
            //}
            //sb.AppendLine();

            //DataRow pt2seller = parts.Rows[1].GetParentRow("Sellers_Parts");

            //sb.AppendLine("Part " + parts.Rows[1]["PartCode"] + " was ordered by :");
            //sb.AppendFormat("{0} | {1}\n", pt2seller["Name"], pt2seller["Address1"]);
            //sb.AppendLine();

            //using (StreamWriter outfile = new StreamWriter(filePath + @"\TabPrints.txt"))
            //{
            //    try
            //    {
            //        outfile.Write(sb.ToString());
            //        Console.WriteLine("Ecriture réussie !");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            #endregion

            #region Sales
            //Sales sales = new Sales("Sales");
            //DataSet tempSales = sales.Copy();
            //DataTable tempPeople = tempSales.Tables["People"];

            //DataRow cass = tempPeople.Select("Name='Cassidy'").SingleOrDefault();
            //Guid cassID = (Guid)cass["ID"];

            //cass["Name"] = "Proinsias Cassidy";

            //sales.AddOrder(cassID, 100);

            //sales.Merge(tempSales, false, MissingSchemaAction.Error);

            //string salesPrints = ToolBoxUtilities.MergeStringList(TableReader.DataSetToStrings(sales));

            //using (StreamWriter outfile = new StreamWriter(filePath + @"\SalesPrints.txt"))
            //{
            //    try
            //    {
            //        outfile.Write(salesPrints);
            //        Console.WriteLine("Ecriture réussie !");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            #endregion

            #region Serialization
            Garage myGarage = new Garage();

            DataSerializer.DataToXml(myGarage.cars, "cars.xml");

            DataTable xmlGarage;
            string cars = string.Empty;
            try
            {
                xmlGarage = DataWriter.XmlToTable("cars.xml");
                cars = DataReader.TabToString(xmlGarage, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("La lecture du fichier \"cars.xml\" a échoué\n" + ex.Message);
            }

            PartsBusiness partsBiz = new PartsBusiness();

            DataSerializer.DataToXml(partsBiz, "partsBusiness", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            DataSet xmlPartsBiz;
            string partsBizStr = string.Empty;
            try
            {
                xmlPartsBiz = DataWriter.XmlToSet("partsBusiness", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                partsBizStr = ToolBoxUtilities.MergeStringList(DataReader.DataSetToStrings(xmlPartsBiz, true));
            }
            catch (Exception ex)
            {
                Console.WriteLine("La lecture du fichier \"PartsBusiness.xml\" a échoué\n" + ex.Message);
            }

            using (StreamWriter outfile = new StreamWriter(filePath + @"\SalesPrints.txt"))
            {
                try
                {
                    outfile.Write(cars + "\n\n" + partsBizStr);
                    Console.WriteLine("Ecriture réussie !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion

            Console.ReadKey();
        }
    }
}
