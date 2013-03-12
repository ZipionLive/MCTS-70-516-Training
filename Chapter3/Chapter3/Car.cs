using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter3
{
    public class Car
    {
        public string plate { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public int year { get; set; }

        public Car(string plate, string manufacturer, string model, int year)
        {
            this.plate = plate;
            this.manufacturer = manufacturer;
            this.model = model;
            this.year = year;
        }
    }
}
