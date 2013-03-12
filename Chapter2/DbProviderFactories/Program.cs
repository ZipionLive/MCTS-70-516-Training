using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using Toolbox;

namespace DbProviderFactories
{
    class Program
    {
        static void Main(string[] args)
        {
            DbProviderFactory factory = SqlClientFactory.Instance;

            DbConnection connection = GetProviderConnection(factory);
            try
            {
                connection.Open();
                Console.WriteLine("Connection opened !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to open connection\n" + ex.Message);
            }
            try
            {
                connection.Close();
                Console.WriteLine("Connection closed !");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to close connection");
            }
        }

        public static DbConnection GetProviderConnection(DbProviderFactory factory)
        {
            DbConnection connection = factory.CreateConnection();
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

            connection.ConnectionString = cString;

            return connection;
        }
    }
}
