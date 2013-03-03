using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Chapter1
{
    public class Garage
    {
        public DataTable cars { get; private set; }

        #region Constructor
        public Garage(bool xmlSerializable = true)
        {
            this.cars = new DataTable("Cars");

            #region ID
            DataColumn ID = new DataColumn("ID", typeof(int));
            ID.AutoIncrement = true;
            ID.AutoIncrementSeed = 0;
            ID.AutoIncrementStep = 1;
            cars.Columns.Add(ID);
            cars.PrimaryKey = new DataColumn[] { ID };
            #endregion
            #region Plate
            DataColumn plate = new DataColumn("Plate");
            plate.MaxLength = 6;
            plate.Unique = true;
            plate.AllowDBNull = false;
            cars.Columns.Add(plate);
            #endregion
            #region Manufacturer
            DataColumn manufacturer = new DataColumn("Manufacturer");
            manufacturer.MaxLength = 50;
            manufacturer.AllowDBNull = false;
            cars.Columns.Add(manufacturer);
            #endregion
            #region Model
            DataColumn model = new DataColumn("Model");
            model.MaxLength = 50;
            model.AllowDBNull = false;
            cars.Columns.Add(model);
            #endregion
            #region Year
            DataColumn year = new DataColumn("Year", typeof(int));
            year.AllowDBNull = false;
            cars.Columns.Add(year);
            #endregion
            #region YearModel
            DataColumn yearModel = new DataColumn("YearModel");
            yearModel.Expression = "Model + ' ' + Year";
            yearModel.Caption = "Year and Model";
            cars.Columns.Add(yearModel);
            #endregion

            if (xmlSerializable)
            {
                this.cars.TableName = "CarsList";
                ID.ColumnMapping = MappingType.Attribute;
                plate.ColumnMapping = MappingType.Attribute;
                manufacturer.ColumnMapping = MappingType.Attribute;
                model.ColumnMapping = MappingType.Attribute;
                year.ColumnMapping = MappingType.Attribute;
                yearModel.ColumnMapping = MappingType.Hidden;
            }

            FillGarage();
        }
        #endregion

        #region Methods
        private void FillGarage()
        {
            try
            {
                AddCar("ABC123", "Ford", "Gran Torino", 1972);
                AddCar("DEF456", "Dodge", "Charger", 1969);
                AddCar("GHI789", "Pontiac", "Trans Am", 1969);
                AddCar("JKL987", "Chevrolet", "Camaro SS396", 1967);
                AddCar("MNO654", "Cadillac", "Eldorado", 1958);
                AddCar("PQR321", "Delorean", "DMC-12", 1981);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddCar(string plate, string manufacturer, string model, int year)
        {
            if (year >= 1908 && year <= DateTime.Now.Year)
            {
                DataRow newCar = cars.NewRow();
                newCar["Plate"] = plate;
                newCar["Manufacturer"] = manufacturer;
                newCar["Model"] = model;
                newCar["Year"] = year;
                cars.Rows.Add(newCar);
            }
            else
            {
                throw new InvalidOperationException("Invalid value for the \"Year\" column");
            }
        }
        #endregion
    }
}
