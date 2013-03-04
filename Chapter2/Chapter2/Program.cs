using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var nw = ConfigurationManager.ConnectionStrings["Northwind"];
            string cName = nw.Name;
            string cProvider = nw.ProviderName;
            string cString = nw.ConnectionString;
            cString=cString.Replace("@user", login);
            cString=cString.Replace("@pwd", pwd );
            using (SqlConnection connect = new SqlConnection(cString))
            {
                try
                {
                    connect.Open();
                    Console.WriteLine("SQL connection opened !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("SQL connection closed !");
            #endregion

            Console.ReadKey();
        }
    }
}
