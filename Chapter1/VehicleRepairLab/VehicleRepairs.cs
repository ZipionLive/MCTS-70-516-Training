using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;

namespace VehicleRepairLab
{
    class VehicleRepairs : DataSet
    {
        public string name { get; private set; }
        public DataTable vehicles { get; private set; }
        public DataTable repairs { get; private set; }

        private VehicleRepairs() : base() { }

        public VehicleRepairs(string dataSetName = "VehicleRepairs")
            : base(dataSetName)
        {
            this.name = dataSetName;

            vehicles = this.Tables.Add("Vehicles");
            DataColumn plate = vehicles.Columns.Add("Plate");
            plate.Unique = true;
            vehicles.Columns.Add("Manufacturer");
            vehicles.Columns.Add("Model");
            vehicles.Columns.Add("Year", typeof(int));
            vehicles.PrimaryKey = new DataColumn[] { plate };

            repairs = this.Tables.Add("Repairs");
            DataColumn id = repairs.Columns.Add("ID", typeof(int));
            id.AutoIncrement = true;
            id.AutoIncrementSeed = -1;
            id.AutoIncrementStep = -1;
            DataColumn vPlate = repairs.Columns.Add("VehiclePlate");
            repairs.Columns.Add("Description");
            repairs.Columns.Add("Cost");
            repairs.PrimaryKey = new DataColumn[] { id };

            this.Relations.Add(
                "Vehicles_Repairs",
                plate,
                vPlate);
        }
    }
}
