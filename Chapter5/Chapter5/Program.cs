using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Lesson1
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDoc;
            if (File.Exists(GetFilePath("xmlTest.xml")))
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(GetFilePath("xmlTest.xml"));
            }
            else
            {
                xmlDoc = CreateXml();
            }
            //ParseXml(xmlDoc);
            //SelectXmlNode(xmlDoc, "2");
            //SelectXmlNodesByTag(xmlDoc, "GrandChild", false);
            //SelectXmlNodesByTag(xmlDoc, "Child", false);
            //SelectXmlNodes(xmlDoc, "Child", true);
            StringBuilder sb = ReadXml(Path.Combine(@"C:\Users\ZipionLive\workspace\", "Northwind Customers.xml"));

            using (StreamWriter sw = new StreamWriter(GetFilePath("XmlReaderTest.txt")))
            {
                try
                {
                    sw.Write(sb.ToString());
                    Console.WriteLine("Ecriture de \"XmlReaderTest.txt\" réussie !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static StringBuilder ReadXml(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            XmlReader reader = new XmlTextReader(filePath);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                    case XmlNodeType.Element:
                    case XmlNodeType.Comment:
                        sb.AppendFormat("{0} : {1} = {2}", reader.NodeType, reader.Name, reader.Value);
                        sb.AppendLine();
                        break;
                    case XmlNodeType.Text:
                        sb.AppendFormat(" - Value : {0}", reader.Value);
                        sb.AppendLine();
                        break;
                }

                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        sb.AppendFormat(" - Attribute : {0} = {1}", reader.Name, reader.Value);
                        sb.AppendLine();
                    }
                }
            }

            reader.Close();
            return sb;
        }

        private static void SelectXmlNodes(XmlDocument xmlDoc, string tag, bool goDeep = true)
        {
            XmlNodeList nodeList = xmlDoc.SelectNodes(string.Format("//{0}", tag));

            StringBuilder sb = new StringBuilder();

            foreach (XmlNode node in nodeList)
                RecurseNodes(node, 0, sb, goDeep);

            Console.WriteLine(sb.ToString());
        }

        private static void SelectXmlNodesByTag(XmlDocument xmlDoc, string tag, bool goDeep = true)
        {
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName(tag);

            StringBuilder sb = new StringBuilder();

            foreach (XmlNode node in nodeList)
                RecurseNodes(node, 0, sb, goDeep);

            Console.WriteLine(sb.ToString());
        }

        private static void SelectXmlNode(XmlDocument xmlDoc, string ID)
        {
            XmlNode node = xmlDoc.SelectSingleNode(string.Format("//Child[@ID='{0}']", ID));
            RecurseNodes(node);
        }

        private static void ParseXml(XmlDocument xmlDoc)
        {
            RecurseNodes(xmlDoc.DocumentElement);
        }

        private static void RecurseNodes(XmlNode node, bool goDeep = true)
        {
            StringBuilder sb = new StringBuilder();

            if (goDeep)
                RecurseNodes(node, 0, sb);

            Console.WriteLine(sb.ToString());
        }

        private static void RecurseNodes(XmlNode node, int level, StringBuilder sb, bool goDeep = true)
        {
            sb.AppendFormat("{0,2} Type : {1,7} Name : {2,10} Attr : ", level, node.NodeType, node.Name);

            foreach (XmlAttribute attr in node.Attributes)
                sb.AppendFormat("{0} = {1}", attr.Name, attr.Value);
            sb.AppendLine();

            if (goDeep)
            {
                foreach (XmlNode n in node.ChildNodes)
                    RecurseNodes(n, level + 1, sb);
            }
        }

        private static XmlDocument CreateXml()
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement elt;
            int childCounter;
            int grandChildCounter;

            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));

            elt = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(elt);

            for (childCounter = 1; childCounter <= 5; childCounter++)
            {
                XmlElement childElt = xmlDoc.CreateElement("Child");
                XmlAttribute childAttr = xmlDoc.CreateAttribute("ID");
                childAttr.Value = childCounter.ToString();
                childElt.Attributes.Append(childAttr);

                elt.AppendChild(childElt);

                for (grandChildCounter = 1; grandChildCounter <= 3; grandChildCounter++)
                {
                    childElt.AppendChild(xmlDoc.CreateElement("GrandChild"));
                }
            }

            xmlDoc.Save(GetFilePath("xmlTest.xml"));

            return xmlDoc;
        }

        private static string GetFilePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);
        }
    }
}
