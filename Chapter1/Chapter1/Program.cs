using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolbox;
using System.Data;

namespace Chapter1
{
    class Program
    {
        static void Main(string[] args)
        {
            Garage myGarage = new Garage();

            Console.WriteLine("Original Garage :");
            Console.WriteLine(TableReader.TabToString(myGarage.cars));

            DataTable garageClone = TableWriter.CloneWithContent(myGarage.cars);

            garageClone.LoadDataRow(new object[] { null, "STU159", "Lancia", "Stratos", 1973 }, true);
            garageClone.LoadDataRow(new object[] { null, "VWX357", "Ford", "GT40", 1969 }, true);

            Console.WriteLine("Cloned garage :");
            Console.WriteLine(TableReader.TabToString(garageClone));

            DataView garageView = new DataView(garageClone);
            garageView.Sort = "Year DESC";

            Console.WriteLine("Garage view :");
            Console.WriteLine(TableReader.ViewToString(garageView));

            Console.ReadKey();
        }
    }
}
