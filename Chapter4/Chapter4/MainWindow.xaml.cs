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
using System.IO;

namespace Lesson1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NorthwindDataContext ndc;
        private MemoryStream photo;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ndc = new NorthwindDataContext();
            StringWriter sw = new StringWriter();
            ndc.Log = sw;

            Employee employee = ndc.Employees.Where(emp => emp.LastName.StartsWith("Davolio"))
                .Select(emp => emp).FirstOrDefault();

            MessageBox.Show(sw.GetStringBuilder().ToString());

            photo = new MemoryStream(employee.Photo.ToArray());
            MessageBox.Show(sw.GetStringBuilder().ToString());

            var empListing = ndc.Employees
                .Select(emp => new {
                    name = emp.FirstName + " " + emp.LastName,
                    adress = emp.Address,
                    city = emp.City,
                    postcode = emp.PostalCode,
                    country = emp.Country,
                    phone = emp.HomePhone
                }).AsQueryable();

            dgEmployees.ItemsSource = empListing;
            MessageBox.Show(sw.GetStringBuilder().ToString());
        }
    }
}
