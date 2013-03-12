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
using System.Xml;
using IO = System.IO;
using System.Xml.Linq;

namespace Lesson2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XDocument xDoc;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            xDoc = CreateXDocument("xDocTest.xml");

            var qResult = xDoc.Descendants("Root").SingleOrDefault()
                .Descendants("Child")
                .Where(c => c.Attribute("ID").Value == "Child 2")
                .SingleOrDefault()
                    .Descendants("GrandChild")
                    .Select(gc => new
                    {
                        ID = gc.Attribute("ID").Value,
                        Attr = gc.Attribute("Attr").Value
                    }).AsQueryable();

            dgQueryResult.ItemsSource = qResult;
        }

        private XDocument CreateXDocument(string fileName)
        {
            string xml = @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
                <Root>
                    <Child ID='Child 1' Attr='Un'>
                        <GrandChild ID='Grand child 1-1' Attr='One' />
                        <GrandChild ID='Grand child 1-2' Attr='Two' />
                        <GrandChild ID='Grand child 1-3' Attr='Three' />
                    </Child>
                    <Child ID='Child 2' Attr='Deux'>
                        <GrandChild ID='Grand child 2-1' Attr='Four' />
                        <GrandChild ID='Grand child 2-2' Attr='Five' />
                        <GrandChild ID='Grand child 2-3' Attr='Six' />
                    </Child>
                    <Child ID ='Child 3' Attr='Trois'>
                        <GrandChild ID='Grand child 3-1' Attr='Seven' />
                        <GrandChild ID='Grand child 3-2' Attr='Eight' />
                        <GrandChild ID='Grand child 3-3' Attr='Nine' />
                    </Child>
                </Root>";

            XDocument xDoc = XDocument.Parse(xml);

            if (!IO.File.Exists(GetFilePath(fileName)))
            {
                xDoc.Save(GetFilePath(fileName));
                MessageBox.Show("Fichier \"" + fileName + "\" créé !");
            }
            else
            {
                XDocument xDocFile = XDocument.Load(GetFilePath(fileName));
                XNode fileRoot = xDocFile.Descendants("Root").SingleOrDefault();
                XNode stringRoot = xDoc.Descendants("Root").SingleOrDefault();

                if (!XNode.DeepEquals(fileRoot, stringRoot))
                {
                    xDoc.Save(GetFilePath(fileName));
                    MessageBox.Show("Fichier \"" + fileName + "\" recréé !");
                }
                else
                {
                    MessageBox.Show("Le fichier \"" + fileName + "\" existe déjà !");
                }
            }

            return xDoc;
        }

        public string GetFilePath(string fileName)
        {
            string filePath = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
            return filePath;
        }

        private void btnXmlToObjects_Click(object sender, RoutedEventArgs e)
        {
            var childAndGrandChildren = xDoc.Descendants("Root").SingleOrDefault()
                .Descendants("Child").Select(c => new Child
                {
                    id = (string)c.Attribute("ID"),
                    attr = (string)c.Attribute("Attr"),
                    children = c.Descendants("GrandChild").Select(gc => new GrandChild
                    {
                        id = (string)gc.Attribute("ID"),
                        attr = (string)gc.Attribute("Attr"),
                        parent = (string)"Child of " + gc.Parent.Attribute("ID").Value
                    }).ToList()
                });

            StringBuilder sb = new StringBuilder();

            foreach (Child c in childAndGrandChildren)
            {
                sb.AppendFormat("{0} : {1}\n", c.id, c.attr);

                foreach (GrandChild gc in c.children)
                {
                    sb.AppendFormat("   {0} : {1}\n        {2}\n", gc.id, gc.attr, gc.parent);
                }
            }

            MessageBox.Show(sb.ToString());
        }

        private void btnXmlToText_Click(object sender, RoutedEventArgs e)
        {
            var childAndGrandChildren = xDoc.Descendants("Child").Select(c => new
            {
                childInfo = string.Format("{0} : {1}",
                    (string)c.Attribute("ID"),
                    (string)c.Attribute("Attr")),
                grandChildInfo = c.Descendants("GrandChild")
                    .Select(gc => string.Format("    {0} : {1}\n",
                        (string)gc.Attribute("ID"),
                        (string)gc.Attribute("Attr")))
            });

            StringBuilder sb = new StringBuilder();

            foreach (var entry in childAndGrandChildren)
            {
                sb.AppendLine(entry.ToString());
            }

            MessageBox.Show(sb.ToString());
        }
    }
}
