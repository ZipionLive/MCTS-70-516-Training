using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using io = System.IO;
using System.IO;
using System.Data;

namespace VehicleRepairLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataSet ds;
        private readonly string schemaFile = io.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "vehicleRepair.xsd");
        private readonly string xmlFile = io.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "vehicleRepair.xml");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateDataSet();
                dgVehicles.ItemsSource = ds.Tables["Vehicles"].DefaultView;
                dgRepairs.ItemsSource = ds.Tables["Repairs"].DefaultView;
                lblDataLoaded.Content = "Data has been loaded !";
            }
            catch (Exception ex)
            {
                lblDataLoaded.Content = ex.Message;
            }
        }

        private void PopulateDataSet()
        {
            if (File.Exists(schemaFile) && File.Exists(xmlFile))
            {
                ds = new DataSet();
                ds.ReadXmlSchema(schemaFile);
                ds.ReadXml(xmlFile);
                string t = ds.GetXml();
            }
            else
            {
                CreateSchema();
            }
        }

        private void CreateSchema(string dataSetName = "VehicleRepair")
        {
            ds = new DataSet(dataSetName);

            DataTable vehicles = ds.Tables.Add("Vehicles");
            DataColumn plate = vehicles.Columns.Add("Plate");
            plate.Unique = true;
            vehicles.Columns.Add("Manufacturer");
            vehicles.Columns.Add("Model");
            vehicles.Columns.Add("Year", typeof(int));
            vehicles.PrimaryKey = new DataColumn[] { plate };

            DataTable repairs = ds.Tables.Add("Repairs");
            DataColumn id = repairs.Columns.Add("ID", typeof(int));
            id.AutoIncrement = true;
            id.AutoIncrementSeed = -1;
            id.AutoIncrementStep = -1;
            DataColumn vPlate = repairs.Columns.Add("VehiclePlate");
            repairs.Columns.Add("Description");
            repairs.Columns.Add("Cost");
            repairs.PrimaryKey = new DataColumn[] { id };

            ds.Relations.Add(
                "Vehicles_Repairs",
                plate,
                vPlate);

            ds.WriteXmlSchema(schemaFile);

            vehicles.Rows.Add("ABC123", "Ford", "Gran Torino", 1972);
            vehicles.Rows.Add("DEF456", "Dodge", "Challenger", 1969);
            repairs.Rows.Add(null, "ABC123", "Oil replaced", (decimal)10.00);
            repairs.Rows.Add(null, "ABC123", "Front lights replaced", (decimal)25.00);
            repairs.Rows.Add(null, "DEF456", "Engine cleaned", (decimal)30.00);

            ds.WriteXml(xmlFile);
        }
    }
}
