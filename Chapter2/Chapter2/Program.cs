using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Odbc;
using Toolbox;

namespace Chapter2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ODBC
            //using (OdbcConnection connect = new OdbcConnection())
            //{
            //    Console.WriteLine("Please enter login for ODBC connection :");
            //    string login = Console.ReadLine();
            //    Console.WriteLine("Please enter password for ODBC connection :");
            //    string pwd = ConsoleUtilities.GetPassword();

            //    string connectString1 = @"DSN=ODBC_Test; UID=" + login + "; PWD=" + pwd;
            //    string connectString2 = @"DRIVER={SQL Server}; SERVER=ASPIRE-5560G\ZLSQL; DATABASE=Northwind; UID=" + login + "; PWD=" + pwd;

            //    connect.ConnectionString = connectString2;
            //    try
            //    {
            //        connect.Open();
            //        Console.WriteLine("ODBC connection opened !");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}

            //Console.WriteLine("ODBC connection closed !");
            #endregion

            #region SQL Server
            Console.WriteLine("Please enter login for SQL connection :");
            string login = Console.ReadLine();
            Console.WriteLine("Please enter password for SQL connection :");
            string pwd = ConsoleUtilities.GetPassword();

            ConnectionStringSettings nw = ConfigurationManager.ConnectionStrings["NorthwindMars"];
            string cName = nw.Name;
            string cProvider = nw.ProviderName;
            string cString = nw.ConnectionString;
            cString = cString.Replace("@user", login);
            cString = cString.Replace("@pwd", pwd);

            #region SqlConnection
            //using (SqlConnection connect = new SqlConnection(cString))
            //{
            //    SqlCommand cmdNonQuery = connect.CreateCommand();
            //    cmdNonQuery.CommandType = CommandType.Text;
            //    cmdNonQuery.CommandText = "UPDATE Products SET UnitPrice = UnitPrice - 3.1 WHERE ProductID = @pid";
            //    DbParameter param = cmdNonQuery.CreateParameter();
            //    param.ParameterName = "@pid";
            //    param.Value = "10";
            //    cmdNonQuery.Parameters.Add(param);

            //    SqlCommand cmdScalar = connect.CreateCommand();
            //    cmdScalar.CommandType = CommandType.Text;
            //    cmdScalar.CommandText = "SELECT COUNT(*) FROM Products";

            //    SqlCommand cmdReader = connect.CreateCommand();
            //    cmdReader.CommandType = CommandType.Text;
            //    cmdReader.CommandText = "SELECT ProductID, QuantityPerUnit, UnitPrice FROM Products";

            //    try
            //    {
            //        connect.Open();
            //        //cmdNonQuery.ExecuteNonQuery();
            //        Console.WriteLine("SQL connection opened !");
            //        Console.WriteLine("Il y a " + cmdScalar.ExecuteScalar() + " entrées dans la table \"Products\"");

            //        var reader = cmdReader.ExecuteReader();
            //        DataTable tempTab = new DataTable("Products");
            //        tempTab.Load(reader, LoadOption.Upsert);

            //        foreach (DataColumn col in tempTab.Columns)
            //            col.ColumnMapping = MappingType.Attribute;

            //        DataSerializer.DataToXml(tempTab, "products");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}

            //Console.WriteLine("SQL connection closed !");
            #endregion

            #region DataAdapter
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = cString;
            SqlCommand cmd = connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT CustomerID, CompanyName FROM Customers";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet nwCustSet = new DataSet("NorthwindCustomers");
            sda.Fill(nwCustSet, "Customers");
            Console.WriteLine("DataSet Filled");

            foreach (DataColumn col in nwCustSet.Tables["Customers"].Columns)
                col.ColumnMapping = MappingType.Attribute;

            try 
	        {	        
		        DataSerializer.DataToXml(nwCustSet, "nwCustomers");
	        }
	        catch (Exception ex)
	        {
                Console.WriteLine(ex.Message);
	        }       
            #endregion
            #endregion

            Console.ReadKey();
        }
    }
}
