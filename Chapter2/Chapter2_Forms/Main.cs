using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Toolbox;
using System.Data.SqlClient;

namespace Chapter2_Forms
{
    public partial class Main : Form
    {
        private ConnectionStringSettings nw = ConfigurationManager.ConnectionStrings["NorthwindMars"];
        private bool connected;
        private SqlConnection connect;
        private string cName;
        private string cProvider;
        private string cString;
        private SqlDataAdapter sda;
        private DataSet nwSet;
        private DataTable customers;
        private List<DataRow> deletedRows = new List<DataRow>();
        //private List<DataRow> RowSelection = new List<DataRow>();

        public Main()
        {
            InitializeComponent();
        }

        private void FillDataGrid(DataSet set)
        {
            dgDataViewer.DataSource = set;
            dgDataViewer.DataMember = "Customers";
        }

        private void Disconnect()
        {
            cString = nw.ConnectionString;
            connected = false;
            dgDataViewer.DataSource = null;
            btnConnect.Text = "Connect !";
            pnlControl.BackColor = Color.DarkRed;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                string login = tbLogin.Text;
                string pwd = tbPassword.Text;

                tbLogin.Text = string.Empty;
                tbPassword.Text = string.Empty;

                cString = cString.Replace("@user", login);
                cString = cString.Replace("@pwd", pwd);

                try
                {
                    connect.ConnectionString = cString;
                    SqlCommand cmd = connect.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Country FROM Customers";
                    sda = new SqlDataAdapter(cmd);
                    SqlCommandBuilder cmdBuild = new SqlCommandBuilder(sda);
                    nwSet = new DataSet("Northwind");
                    sda.Fill(nwSet, "Customers");
                    customers = nwSet.Tables["Customers"];

                    FillDataGrid(nwSet);

                    pnlControl.BackColor = Color.DarkGreen;
                    MessageBox.Show("You are now connected to the Northwind database !", "Success");
                    connected = true;
                    btnConnect.Text = "Disconnect";
                }
                catch (SqlException)
                {
                    MessageBox.Show("Please check your login and password.", "Connection Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                Disconnect();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            connected = false;
            connect = new SqlConnection();
            cName = nw.Name;
            cProvider = nw.ProviderName;
            cString = nw.ConnectionString;

            foreach (DataGridViewColumn dgCol in dgDataViewer.Columns)
                dgCol.DefaultCellStyle.Font = new Font("Ubuntu", 9.0F, GraphicsUnit.Pixel); 
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void btnWriteXml_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                foreach (DataTable tab in nwSet.Tables)
                {
                    foreach (DataColumn col in tab.Columns)
                    {
                        col.ColumnMapping = MappingType.Attribute;
                    }
                }

                try
                {
                    DataSerializer.DataToXml(nwSet, "Northwind Customers", @"C:\Users\ZipionLive\workspace\");
                    MessageBox.Show("XML successfully written", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                sda.UpdateBatchSize = 0;
                sda.Update(nwSet, "Customers");
                nwSet.AcceptChanges();
                deletedRows.Clear();
                FillDataGrid(nwSet);

                MessageBox.Show("Changes successfully saved", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgDataViewer.SelectedRows)
                {
                    string custID = row.Cells[0].Value.ToString();
                    DataRow delRow = customers.Select("CustomerID='" + custID + "'").SingleOrDefault<DataRow>();
                    deletedRows.Add(delRow);
                    customers.Rows.Remove(delRow);
                    //dgDataViewer.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in deletedRows)
                customers.Rows.Add(row);
        }
    }
}
