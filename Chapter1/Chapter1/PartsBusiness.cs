using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Chapter1
{
    [Serializable]
    public class PartsBusiness : DataSet
    {
        public DataTable sellers { get; private set; }
        public DataTable parts { get; private set; }
        public string name { get; private set; }

        public PartsBusiness(string dataSetName = "PartsBusiness", bool xmlSerializable = true) : base(dataSetName)
        {
            DataTable sellers = this.Tables.Add("Sellers");
            DataColumn sid = sellers.Columns.Add("ID", typeof(Guid));
            DataColumn name = sellers.Columns.Add("Name");
            DataColumn address1 = sellers.Columns.Add("Address1");
            DataColumn address2 = sellers.Columns.Add("Address2");
            DataColumn city = sellers.Columns.Add("City");
            DataColumn postCode = sellers.Columns.Add("PostCode");
            DataColumn country = sellers.Columns.Add("Country");
            sellers.PrimaryKey = new DataColumn[] { sid };

            DataTable parts = this.Tables.Add("Parts");
            DataColumn pid = parts.Columns.Add("ID", typeof(Guid));
            DataColumn sellerId = parts.Columns.Add("SellerID", typeof(Guid));
            DataColumn partCode = parts.Columns.Add("PartCode");
            DataColumn  partDesc = parts.Columns.Add("PartDescription");
            DataColumn cost = parts.Columns.Add("Cost", typeof(decimal));
            DataColumn rPrice = parts.Columns.Add("RetailPrice", typeof(decimal));
            DataColumn codeCost = parts.Columns.Add("CodeCost");
            codeCost.Expression = "PartCode + ' ' + Cost";
            parts.PrimaryKey = new DataColumn[] { pid };

            this.Relations.Add(
                "Sellers_Parts",
                sid,
                sellerId, true);

            ForeignKeyConstraint fk = (ForeignKeyConstraint)parts.Constraints["Sellers_Parts"];
            fk.DeleteRule = Rule.Cascade;

            if (xmlSerializable)
            {
                foreach (DataTable tab in this.Tables)
                {
                    foreach (DataColumn col in tab.Columns)
                    {
                        if (col.DataType == typeof(Guid))
                            col.ColumnMapping = MappingType.Attribute;
                        else if (col.Expression != string.Empty)
                            col.ColumnMapping = MappingType.Hidden;
                    }
                }

                foreach (DataRelation rel in this.Relations)
                {
                    rel.Nested = true;
                }
            }

            this.sellers = sellers;
            this.parts = parts;
            this.name = dataSetName;

            FillTables();
        }

        private void FillTables()
        {
            AddSeller("TailSpin Toys", "42 Avenue de la Couronne", "Bruxelles", "1040", "Belgique");
            AddSeller("Bart Smit Rue Neuve", "25 Rue Neuve", "Bruxelles", "1000", "Belgique");
            AddSeller("Joué Club", "57 Place d'Armes", "Valenciennes", "59300", "France");
            AddPart(0, "PT1", "Part 1 Description", (decimal)1.50, (decimal)2.00);
            AddPart(0, "PT2", "Part 2 Description", (decimal)2.35, (decimal)3.50);
            AddPart(1, "PT3", "Part 3 Description", (decimal)3.50, (decimal)5.00);
            AddPart(2, "PT4", "Part 4 Description", (decimal)1.20, (decimal)1.80);
            AddPart(2, "PT5", "Part 5 Description", (decimal)2.00, (decimal)2.60);
            AddPart(2, "PT6", "Part 6 Description", (decimal)0.55, (decimal)1.00);
        }

        public void AddSeller(string name, string address1, string address2, string city, string postCode, string country)
        {
            DataRow newSeller = this.sellers.NewRow();
            newSeller["ID"] = Guid.NewGuid();
            newSeller["Name"] = name;
            newSeller["Address1"] = address1;
            newSeller["Address2"] = address2;
            newSeller["City"] = city;
            newSeller["PostCode"] = postCode;
            newSeller["Country"] = country;

            sellers.Rows.Add(newSeller);
        }

        public void AddSeller(string name, string address, string city, string postCode, string country)
        {
            AddSeller(name, address, null, city, postCode, country);
        }

        public void AddPart(int sellerIndex, string partCode, string partDesc, decimal cost, decimal retailPrice)
        {
            DataRow newPart = this.parts.NewRow();
            newPart["ID"] = Guid.NewGuid();
            newPart["SellerID"] = sellers.Rows[sellerIndex]["ID"];
            newPart["PartCode"] = partCode;
            newPart["PartDescription"] = partDesc;
            newPart["Cost"] = cost;
            newPart["RetailPrice"] = retailPrice;

            parts.Rows.Add(newPart);
        }

        public void DelRow(DataTable tab, DataRow row)
        {
            try
            {
                if (tab == sellers)
                    Console.WriteLine("Removing row \"" + row["Name"] + "\"");
                else if (tab == parts)
                    Console.WriteLine("Removing row \"" + row["PartCode"] + "\"");
                tab.Rows.Remove(row);
                Console.WriteLine("Row removed !");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
