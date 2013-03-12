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

namespace Lesson2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NorthwindDataContext ndc = new NorthwindDataContext();
        private StringWriter sw = new StringWriter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ndc.Log = sw;

            var customers = ndc.Customers.Where(c => c.CompanyName.Contains("Restaurant"))
                .OrderBy(c => c.PostalCode)
                .Select(c => new
                {
                    c.CompanyName,
                    c.ContactName,
                    c.ContactTitle,
                    c.Country
                }).AsQueryable();

            dgCustomers.ItemsSource = customers;
            MessageBox.Show(sw.GetStringBuilder().ToString());
        }

        private void btnCustWithOrders_Click(object sender, RoutedEventArgs e)
        {
            var custWithOrders = ndc.Customers.Join(
                ndc.Orders,
                c => c.CustomerID,
                o => o.CustomerID,
                (c, o) => new
                {
                    c.CustomerID,
                    c.CompanyName,
                    c.ContactName,
                    o.OrderID,
                    o.OrderDate
                }).OrderBy(r => r.CustomerID)
                .ThenBy(r => r.OrderID);

            dgCustomers.ItemsSource = custWithOrders;
            MessageBox.Show(sw.GetStringBuilder().ToString());
        }

        private void btnGroupJoin_Click(object sender, RoutedEventArgs e)
        {
            var custOuterJoin = ndc.Customers.GroupJoin(
                ndc.Orders,
                c => c.CustomerID,
                o => o.CustomerID,
                (c, o) => new
                {
                    c.CustomerID,
                    c.CompanyName,
                    orders = o
                }).SelectMany(t => t.orders.DefaultIfEmpty().Select(ord => new {
                    t.CompanyName,
                    t.CustomerID,
                    OrderID = (int?)ord.OrderID,
                    OrderDate = (DateTime?)ord.OrderDate
                })).OrderBy(r => r.CustomerID)
                .ThenBy(r => r.OrderID);

            dgCustomers.ItemsSource = custOuterJoin;
            MessageBox.Show(sw.GetStringBuilder().ToString());
        }

        private void btnStoredProc_Click(object sender, RoutedEventArgs e)
        {
            dgCustomers.ItemsSource = ndc.CustOrderHist("ALFKI");
            MessageBox.Show(sw.GetStringBuilder().ToString());
        }
    }
}
