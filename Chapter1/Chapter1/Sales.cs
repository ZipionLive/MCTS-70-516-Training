using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Chapter1
{
    public class Sales : DataSet
    {
        public DataTable people { get; private set; }
        public DataTable orders { get; private set; }
        public string name { get; private set; }

        /// <summary>
        /// Constructeur vide pour permettre l'appel de la méthode Copy()
        /// La visibilité "private" permet d'appeler le vrai constructeur sans paramètre.
        /// </summary>
        private Sales() : base() { }

        public Sales(string dataSetName = "Sales") : base(dataSetName)
        {
            this.name = dataSetName;

            people = this.Tables.Add("People");
            people.Columns.Add("ID", typeof(Guid));
            people.Columns.Add("Name");
            people.PrimaryKey = new DataColumn[] { people.Columns["ID"] };

            orders = this.Tables.Add("Orders");
            orders.Columns.Add("ID", typeof(Guid));
            orders.Columns.Add("PersonID", typeof(Guid));
            orders.Columns.Add("Amount", typeof(decimal));
            orders.PrimaryKey = new DataColumn[] { orders.Columns["ID"] };

            this.Relations.Add(
                "People_Orders",
                people.Columns["ID"],
                orders.Columns["PersonID"],
                true);

            AddPeople("Jesse Custer");
            AddPeople("Tulip O'Hare");
            AddPeople("Cassidy");
        }

        public void AddPeople(string name)
        {
            try
            {
                people.Rows.Add(Guid.NewGuid(), name);
            }
            catch (NullReferenceException nrex)
            {
                Console.WriteLine(nrex.TargetSite + "\n\n" + nrex.Message + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddOrder(Guid personID, int amount)
        {
            orders.Rows.Add(Guid.NewGuid(), personID, amount);
        }
    }
}
